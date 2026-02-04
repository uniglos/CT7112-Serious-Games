using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        ScaleFromEdge.GlobalDifficulty = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

}
