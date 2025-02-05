using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("JumpScene");
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

	public void RestartLevel()
    {
        if (!string.IsNullOrEmpty(SceneData.LastSceneName))
        {
            SceneManager.LoadScene(SceneData.LastSceneName);
        }
        else
        {
            Debug.LogError("Имя последней сцены не сохранено!");
        }
    }

	public void OnResumeButtonPressed()
    {
        // Найдём объект PauseManager в основной сцене и вызовем ResumeGame()
        PauseManager pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager != null)
        {
            pauseManager.ResumeGame();
        }
        else
        {
            Debug.LogError("PauseManager не найден в сцене!");
        }
    }
}