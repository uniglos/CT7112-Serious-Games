using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public SpriteRenderer sr;
    public string currentColour;
    

    public Color[] colours;
    public string[] colourNames;
    int colourIndex = 0;
    public SpriteColourCombo[] spriteColours;
    public List<SpriteColourCombo> colourOrder;
    private int currentIndex;

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

    public GameObject gameOverPanel;

    public TMP_Text lastScoreText;
    public TMP_Text highScoreText;

    private int highScore = 0;

    
    public Image nextColourIcon;
   
    public ParticleSystem RippleGood;
    public ParticleSystem RippleBad;

    public ScreenShake screenShake;

    public AudioSource audioSource;
    public AudioClip scoreSound;
    public AudioClip missSound;

    public CarouselSlide carouselSlide;
    public OutlineAnimatorUI outlineAnimator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        SetRandomColour();

        currentIndex = colourOrder.FindIndex(c => c.ColourName == currentColour);

        UpdateCarousel();
 
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetNextColour();
            UpdateCarousel();
            carouselSlide.PlayAnimation();
            StartCoroutine(outlineAnimator.Pulse());

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
        
        currentIndex++;
        if (currentIndex >= colourOrder.Count)
            currentIndex = 0;

        
        sr.sprite = colourOrder[currentIndex].sprite;
        currentColour = colourOrder[currentIndex].ColourName;
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

            audioSource.PlayOneShot(scoreSound, 1);


            RippleGood.Play();

            StartCoroutine(screenShake.Shake(0.05f, 0.1f));

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

            audioSource.PlayOneShot(missSound, 1);



            if (playerHealth >= 0)
            {
                RippleBad.Play();
                StartCoroutine(heartEffects.ShakeAndFade(hearts[playerHealth]));
                
            }
            
            if (playerHealth <= 0)
            {
                GameOver();
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

    void GameOver()
    {
        PlayerPrefs.SetInt("LastScore", playerScore);
        PlayerPrefs.Save();

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        int finalScore = PlayerPrefs.GetInt("LastScore", 0);
        StartCoroutine(CountScore(finalScore));

        // high score logic
        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        highScoreText.text = "High Score: " + highScore;
    }


    public IEnumerator CountScore(int finalScore)
    {
        int displayed = 0;

        while (displayed < finalScore)
        {
            displayed += Mathf.CeilToInt(finalScore * 5f); // speed curve
            displayed = Mathf.Min(displayed, finalScore);

            lastScoreText.text = "Last Score: " + displayed;

            yield return null;
        }
    }

    void UpdateCarousel()
    {
        nextColourIcon.sprite = colourOrder[currentIndex].sprite;

        int nextIndex = (currentIndex + 1) % colourOrder.Count;
        nextColourIcon.sprite = colourOrder[nextIndex].sprite;
    }









}
