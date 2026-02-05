using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarouselSlide : MonoBehaviour
{
    public RectTransform nextIcon;

    public float slideDuration = 0.2f;
    public float slideDistance = 200f;

    public CanvasGroup group;

    private Coroutine runningAnim;

    private Vector2 originalNextPos;

    void Start()
    {
        originalNextPos = nextIcon.anchoredPosition;
    }

    public void PlayAnimation()
    {
        // Stop previous animation if still running
        if (runningAnim != null)
            StopCoroutine(runningAnim);

        // Reset to original position BEFORE starting
        nextIcon.anchoredPosition = originalNextPos;
        group.alpha = 1f;

        runningAnim = StartCoroutine(AnimateCarousel());
    }

    private IEnumerator AnimateCarousel()
    {
        // 1. Fade + slide UP (out)
        yield return StartCoroutine(SlideAndFade(slideDistance, 0f));

        // 2. Instantly move BELOW original position (off-screen)
        nextIcon.anchoredPosition = originalNextPos - new Vector2(0, slideDistance);

        // 3. Fade + slide UP (in)
        yield return StartCoroutine(SlideAndFade(slideDistance, 1f));

        // 4. Snap back to perfect original position
        nextIcon.anchoredPosition = originalNextPos;
    }

    private IEnumerator SlideAndFade(float moveAmount, float targetAlpha)
    {
        float t = 0f;

        Vector2 startNext = nextIcon.anchoredPosition;
        Vector2 endNext = startNext + new Vector2(0, moveAmount);

        float startAlpha = group.alpha;

        while (t < slideDuration)
        {
            t += Time.deltaTime;
            float lerp = t / slideDuration;

            nextIcon.anchoredPosition = Vector2.Lerp(startNext, endNext, lerp);
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, lerp);

            yield return null;
        }

        nextIcon.anchoredPosition = endNext;
        group.alpha = targetAlpha;
    }
}

