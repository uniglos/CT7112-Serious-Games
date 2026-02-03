using UnityEngine;
using System.Collections;
using System;
using Unity.Mathematics;

public class ScaleFromEdge : MonoBehaviour
{
    public float shrinkSpeed = 0.5f;

    [SerializeField] GameObject prefabToSpawn;
    public Sprite[] possibleSprites;

    Vector3 originalScale;
    bool hasSpawned;

    void Start()
    {
        originalScale = transform.localScale;
        enabled = true;
    }
    void Update()
    {
       transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        
        if (transform.localScale.x <= 0.1f)
        {
            hasSpawned = true;
  
            SpawnReplacement();
            Destroy(gameObject);
        }

        

    }

    void SpawnReplacement()
    {
        GameObject clone = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        clone.transform.localScale = originalScale;
        

        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();

        if (sr != null && possibleSprites.Length > 0)
        {
          int index = UnityEngine.Random.Range(0, possibleSprites.Length);

            while (possibleSprites[index] == sr.sprite)
            {
                index = UnityEngine.Random.Range(0, possibleSprites.Length);
            }

            sr.sprite = possibleSprites[index];
        }
                
        
    }

}
