using UnityEngine;
using UnityEngine.SceneManagement; // Не забудьте подключить пространство имен для управления сценами

public class ChangeSceneOnEsc : MonoBehaviour
{
    // Название сцены, на которую вы хотите переключиться
    [SerializeField] private string targetSceneName;

    void Update()
    {
        // Проверяем, нажата ли клавиша Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Загружаем указанную сцену
            SceneManager.LoadScene(targetSceneName);
        }
    }
}