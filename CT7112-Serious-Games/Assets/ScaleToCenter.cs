using UnityEngine;
using System.Collections;

public class ScaleFromEdge : MonoBehaviour
{
    public float shrinkSpeed = 0.5f;
    void Update()
    {
       transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        
        if (transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject);
        }

    }


}
