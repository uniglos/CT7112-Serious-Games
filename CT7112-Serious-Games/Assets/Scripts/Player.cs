using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer sr;
    public string currentColour;
    

    public Color[] colours;
    public string[] colourNames;
    int colourIndex = 0;
    public SpriteColourCombo[] spriteColours;

    public int playerHealth = 3;
    public int playerScore = 0;

    public TMP_Text scoreText;
    
    int combo = 0;
    int comboMultiplier = 1;
    public TMP_Text comboText;
    public RectTransform canvasRect;
    bool comboAnimating;

    public HeartEffects heartEffects;
    public Image[] hearts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetRandomColour();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetNextColour();
        }
    }

    void SetRandomColour()
    {
        colourIndex = Random.Range(0, spriteColours.Length); 
        sr.sprite = spriteColours[colourIndex].sprite;
        currentColour = spriteColours[colourIndex].ColourName;

        sr.color = Color.white;
    }

    void SetNextColour()
    {
        sr.sprite = spriteColours[colourIndex].sprite;
        currentColour = spriteColours[colourIndex].ColourName;

        colourIndex++;

        if (colourIndex >= spriteColours.Length)
        {
            colourIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!Application.isPlaying) return;
        Debug.Log("Collision with: " + col.name);

        if (col.CompareTag(currentColour))
        {
            playerScore++;
            ScaleFromEdge.GlobalDifficulty += 0.1f;
            scoreText.text = "Score: " + playerScore;
            Debug.Log("Correct Score = " + playerScore);

            combo++;
            comboMultiplier = 1 + combo / 5;
            playerScore += comboMultiplier;
            comboText.text = "X" + comboMultiplier;
            if (!comboAnimating)
                StartCoroutine(ComboPopupRoutine());
            

        }
        else
        {
            playerHealth--;

            if (playerHealth >= 0)
            {
                StartCoroutine(heartEffects.ShakeAndFade(hearts[playerHealth]));
            }
            
            Debug.Log("Wrong. Health =" + playerHealth);

            combo = 0;
            comboMultiplier = 1;
            
        }
    }

    IEnumerator ComboPopupRoutine()
    {
        comboAnimating = true;

        float x = UnityEngine.Random.Range(0.1f, 0.9f) * canvasRect.rect.width;
        float y = UnityEngine.Random.Range(0.1f, 0.9f) * canvasRect.rect.height;

        comboText.rectTransform.anchoredPosition = new Vector2(x, y);

        comboText.transform.localScale = Vector3.one * 0.5f;
        Color c = comboText.color;
        c.a = 0f; 
        comboText.color = c;

        float t = 0f;
        while (t <1f)
        {
            t += Time.deltaTime * 4f;

            c.a = Mathf.Lerp(0f, 1f, t);
            comboText.color = c;

            comboText.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one * 1.2f, t);

            yield return null;
        }

        t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime * 2f;

            c.a = Mathf.Lerp(1f, 0f, t);
            comboText.color = c;

            yield return null;
        }

        comboAnimating = false;
    }



}
