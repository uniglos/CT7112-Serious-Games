using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class SceneLightController : MonoBehaviour
{
    public Light2D globalLight;
    public float fadeDuration = 0.25f;

    private Coroutine runningFade;

    public void FadeToColor(Color newColor)
    {
        // Stop previous fade if it's still running
        if (runningFade != null)
            StopCoroutine(runningFade);

        // Start a new fade
        runningFade = StartCoroutine(FadeRoutine(newColor));
    }

    private IEnumerator FadeRoutine(Color newColor)
    {
        float t = 0f;
        Color startColor = globalLight.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / fadeDuration;

            globalLight.color = Color.Lerp(startColor, newColor, lerp);

            yield return null;
        }

        globalLight.color = newColor;
        runningFade = null;
    }
}


