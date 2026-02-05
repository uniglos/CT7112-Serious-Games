using UnityEngine;

public class RadiusOverTIme : MonoBehaviour
{
    private ParticleSystem ps;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var shape = ps.shape;
        shape.radius = Mathf.PingPong(Time.time, 10f);
    }
}
