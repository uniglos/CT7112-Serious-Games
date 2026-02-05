using UnityEngine;

public class ZigZagScroll : MonoBehaviour
{
    public float scrollSpeedX = 0.2f;
    public float scrollSpeedY = 0f;

    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;

        mat.mainTextureOffset = offset;
    }
}

