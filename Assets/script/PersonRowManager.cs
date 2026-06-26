using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PersonRowManager : MonoBehaviour
{
    public enum AlignmentAxis
    {
        WorldX,
        WorldY,
        WorldZ,
        StartForward,
        StartRight,
        StartUp
    }

    public Transform LaneStartPoint;

    public float Spacing = 5f;

    public float Speed = 3f;

    private bool isMovementLocked = false;

    public List<Transform> Perons = new List<Transform>();

    public AlignmentAxis Alignment;

    private Vector3 GetAlignmentDirection()
    {
        return Alignment switch
        {
           AlignmentAxis.WorldX => Vector3.right,
           AlignmentAxis.WorldY => Vector3.up,
           AlignmentAxis.WorldZ => Vector3.forward,
           AlignmentAxis.StartForward => LaneStartPoint != null ? LaneStartPoint.forward : Vector3.forward,
           AlignmentAxis.StartRight => LaneStartPoint != null ? LaneStartPoint.right : Vector3.right,
           AlignmentAxis.StartUp => LaneStartPoint != null ? LaneStartPoint.up : Vector3.up,
           _ => Vector3.right
        };
    }

    public bool IsfiIsFirstInQueue(GameObject Object)
    {
        return Perons.Count > 0 && Perons[0] != null && Perons[0].gameObject == Object;
        
    }

    public void Update()
    {
        if(isMovementLocked) return; 

        if(LaneStartPoint == null)
        {
            Debug.LogError("не назначена точка" + gameObject.name); 
            return;
        }

        Perons.RemoveAll(row => row == null);

        if (Perons.Count == 0) return;

        Vector3 direction = GetAlignmentDirection();

        Transform FirstPerson = Perons[0];
        Vector3 TargetFirst = LaneStartPoint.position;
        FirstPerson.position = Vector3.MoveTowards(FirstPerson.position, TargetFirst, Speed * Time.deltaTime);

        for (int i = 1; i < Perons.Count; i++)
        {
            Transform CurrentPerson = Perons[i];
            Transform PersonAhead = Perons[i - 1];

            Vector3 Targetpos = PersonAhead.position - direction * Spacing;

            CurrentPerson.position = Vector3.MoveTowards(CurrentPerson.position, Targetpos, Speed * Time.deltaTime);
        }

    }

    private void OnDrawGizmos()
    {
        if (LaneStartPoint == null) return;

        Vector3 queueDir = GetAlignmentDirection();

        // Точка старта
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(LaneStartPoint.position, 0.3f);
        // Направление очереди (куда смотрит первая машина)
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(LaneStartPoint.position, queueDir * 2f);

        // Цели для машин
        for (int i = 0; i < Perons.Count; i++)
        {
            if (Perons[i] == null) continue;

            Vector3 target;
            if (i == 0)
                target = LaneStartPoint.position;
            else
                target = Perons[i - 1].position - queueDir * Spacing;

            bool reached = Vector3.Distance(Perons[i].position, target) < 0.05f;
            Gizmos.color = reached ? Color.green : Color.yellow;
            Gizmos.DrawSphere(target, 0.5f);
            Gizmos.DrawLine(Perons[i].position, target);
        }
    }
}
