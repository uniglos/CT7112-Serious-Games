using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartEffects : MonoBehaviour
{
    public IEnumerator ShakeAndFade(Image heart)
    {
        RectTransform rt = heart.rectTransform;
        Color startColor = heart.color;

        float shakeIntensity = 10f;
        float duration = 0.3f;

        float t = 0f;

        // Shake + fade
        while (t < duration)
        {
            t += Time.deltaTime;

            // Shake
            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);
            rt.anchoredPosition += new Vector2(offsetX, offsetY);

            // Fade
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            heart.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        // Reset + hide
        heart.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        heart.gameObject.SetActive(false);
    }
}

