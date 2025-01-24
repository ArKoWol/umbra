using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void PlayTutorial()
    {
        SceneManager.LoadScene("tutor");
    }
    
    public void PlayMeny()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}