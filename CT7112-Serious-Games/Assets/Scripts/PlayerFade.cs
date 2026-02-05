using UnityEngine;
using System.Collections;

public class PlayerFade : MonoBehaviour
{
    public SpriteRenderer playerSR;
    public float fadeDuration = 0.15f;

    public IEnumerator FadeToSprite(Sprite newSprite)
    {
        float t = 0f;
        Color originalColor = playerSR.color;

        // 1. Fade OUT
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / fadeDuration;

            playerSR.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - lerp);

            yield return null;
        }

        // Swap sprite at the midpoint
        playerSR.sprite = newSprite;

        // 2. Fade IN
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / fadeDuration;

            playerSR.color = new Color(originalColor.r, originalColor.g, originalColor.b, lerp);

            yield return null;
        }

        // Snap to full opacity
        playerSR.color = originalColor;
    }
}
