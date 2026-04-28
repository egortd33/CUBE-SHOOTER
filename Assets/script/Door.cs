using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Door : MonoBehaviour, IToggle
{
    [SerializeField] private Vector3 openRotationEuler = new Vector3(0.0f, 90f, 0.0f);

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpen = false;

    [SerializeField] private float rotationDuration = 0.5f;

    public bool IsOpen => isOpen;

    private bool isAnimation = false;

    private void Start()
    {


        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(openRotationEuler);
    }

   

    public IEnumerator RotateTo(Quaternion targetRotation)
    {
        isAnimation = true;

        Quaternion startRotation = transform.rotation;

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.SmoothStep(0f, 1f, elapsed / rotationDuration);

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = (targetRotation == openRotation);
        isAnimation = false;
    }

    public void Toggle()
    {
               if (isAnimation) return;

        if (isOpen)
        {
            StartCoroutine(RotateTo(closedRotation));
        }
        else
        {
            StartCoroutine(RotateTo(openRotation));
        }
    }
}