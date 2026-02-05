using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutlineAnimatorUI : MonoBehaviour
{
    public Image outlineImage;            // UI Image for the outline
    public float pulseScale = 1.2f;
    public float pulseDuration = 0.15f;

    private RectTransform rect;
    private Vector3 originalScale;
    private Color originalColor;

    void Start()
    {
        rect = outlineImage.GetComponent<RectTransform>();
        originalScale = rect.localScale;
        originalColor = outlineImage.color;
    }

    public IEnumerator Pulse()
    {
        float t = 0f;

        Vector3 startScale = originalScale;
        Vector3 endScale = originalScale * pulseScale;

        Color startColor = originalColor;
        Color endColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.4f);

        while (t < pulseDuration)
        {
            t += Time.deltaTime;
            float lerp = t / pulseDuration;

            rect.localScale = Vector3.Lerp(startScale, endScale, lerp);
            outlineImage.color = Color.Lerp(startColor, endColor, lerp);

            yield return null;
        }

        // Snap back
        rect.localScale = originalScale;
        outlineImage.color = originalColor;
    }
}


