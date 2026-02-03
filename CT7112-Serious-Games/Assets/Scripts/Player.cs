using UnityEngine;


public class Player : MonoBehaviour
{
    public SpriteRenderer sr;
    public string currentColour;
    

    public Color[] colours;
    public string[] colourNames;
    int colourIndex = 0;

    public int playerHealth = 3;
    public int playerScore = 0;
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
        colourIndex = Random.Range(0, colours.Length); 
        sr.color = colours[colourIndex];
        currentColour = colourNames [colourIndex];
    }

    void SetNextColour()
    {
        sr.color = colours[colourIndex];
        currentColour = colourNames[colourIndex];

        colourIndex++;

        if (colourIndex >= colours.Length)
        {
            colourIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ColourObj obj = col.GetComponent<ColourObj>();

        if (obj == null) return;

        if (obj.colourName == currentColour)
        {
            playerScore++;
            Debug.Log("Correct Score = " + playerScore);
        }
        else
        {
            playerHealth--;
            Debug.Log("Wrong. Health =" + playerHealth);
        }
    }



}
