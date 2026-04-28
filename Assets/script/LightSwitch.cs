using System;
using System.Collections;
using UnityEngine;

public class LightSwitch : MonoBehaviour,IToggle 
{
    [SerializeField] private Light targetLight;  // Перетащи свет в инспектор
    [SerializeField] private float fadeDuration = 0.5f;

    private bool isOn = false;

    public bool IsOn => isOn;

    private void Start()
    {
        if (targetLight == null) targetLight = GetComponent<Light>();
        targetLight.enabled = false;  // Свет выключен по умолчанию
    }

    public void Toggle()
    {
        StartCoroutine(FadeLight(!isOn));
    }

    private IEnumerator FadeLight(bool targetState)
    {
        float startIntensity = targetLight.intensity;
        float targetIntensity = targetState ? 1f : 0f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / fadeDuration);
            targetLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            yield return null;
        }

        targetLight.intensity = targetIntensity;
        targetLight.enabled = targetState;
        isOn = targetState;
    }
}