using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class SceneLightController : MonoBehaviour
{
    public Light2D globalLight;
    public float fadeDuration = 0.25f;

    public IEnumerator FadeToColour(Color newColor)
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
    }
}

