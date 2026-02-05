using UnityEngine;
using System.Collections;

public class CarouselWheel : MonoBehaviour
{
    public float rotateDuration = 0.25f;
    public float rotateAngle = 120f; // 3 icons = 360/3

    public IEnumerator RotateWheel()
    {
        float t = 0f;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, rotateAngle);

        while (t < rotateDuration)
        {
            t += Time.deltaTime;
            float lerp = t / rotateDuration;

            transform.rotation = Quaternion.Lerp(startRot, endRot, lerp);

            yield return null;
        }

        transform.rotation = endRot;
    }
}

