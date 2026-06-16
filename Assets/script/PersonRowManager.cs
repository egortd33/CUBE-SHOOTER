using System.Collections.Generic;
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
}
