using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Название сцены с меню паузы (убедитесь, что сцена добавлена в Build Settings)
    [SerializeField] private string pauseSceneName = "PauseMenu";
    
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // Метод для постановки игры на паузу
    private void PauseGame()
    {
        // Приостанавливаем игровое время
        Time.timeScale = 0f;
        // Загружаем сцену меню паузы аддитивно, чтобы исходная сцена не выгружалась
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
        isPaused = true;
    }

    // Метод для возобновления игры
    public void ResumeGame()
    {
        // Восстанавливаем игровое время
        Time.timeScale = 1f;
        // Выгружаем сцену меню паузы
        SceneManager.UnloadSceneAsync(pauseSceneName);
        isPaused = false;
    }
}