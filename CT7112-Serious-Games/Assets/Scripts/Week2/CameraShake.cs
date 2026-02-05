using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

