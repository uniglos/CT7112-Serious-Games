using UnityEngine;
using System.Collections;
using System;
using Unity.Mathematics;

public class ScaleFromEdge : MonoBehaviour
{
    public float shrinkSpeed = 0.5f;
    public float difficultyRamp = 0.1f;
    public static float GlobalDifficulty = 1f;

    [SerializeField] GameObject prefabToSpawn;
    public Sprite[] possibleSprites;

    Vector3 originalScale;

    public SpriteColourCombo[] spriteColours;

    IEnumerator Start()
    {
        yield return null;
        originalScale = transform.localScale;
        enabled = true;
    }
    void Update()
    {
        //float difficulty = 1 + Time.timeSinceLevelLoad * difficultyRamp;
       float speed = shrinkSpeed * GlobalDifficulty;
       transform.localScale -= Vector3.one * speed * Time.deltaTime;
        
        if (transform.localScale.x <= 0.1f)
        {
            
  
            SpawnReplacement();
            Destroy(gameObject);
        }

        

    }

    void SpawnReplacement()
    {
        
        GameObject clone = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        clone.transform.localScale = originalScale;

        Debug.Log("colourObj = " + clone.GetComponent<ColourObj>());

        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
        ColourObj colourObj = clone.GetComponent<ColourObj>();

        if (sr != null && possibleSprites.Length > 0)
        {
          int index = UnityEngine.Random.Range(0, possibleSprites.Length);

            while (possibleSprites[index] == sr.sprite)
            {
                index = UnityEngine.Random.Range(0, possibleSprites.Length);
            }

            Sprite chosen = possibleSprites[index];
            sr.sprite = chosen;

            foreach (var pair in spriteColours)
            {
                if (pair.sprite == chosen)
                { 
                    colourObj.colourName = pair.ColourName;
                    clone.tag = pair.ColourName;
                    break;
                }
            }
        }
                
        
    }

}
