using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCollision : MonoBehaviour
{
    // Начальная позиция для телепортации

    [SerializeField] private string winSceneName;
    
    [SerializeField] private string deadSceneName;
    
    [SerializeField] private string nextSceneName;

    // Этот метод вызывается, когда персонаж сталкивается с триггером
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект, с которым произошло столкновение, имеет тег "chest"
        if (other.CompareTag("chest"))
        {
            // Удаляем объект, с которым произошло столкновение
            Destroy(other.gameObject);
        }

        // Проверяем, что объект, с которым произошло столкновение, имеет тег "enemy"
        if (other.CompareTag("enemy"))
        {
            // Сохраняем имя сцены, в которой произошла смерть
            SceneData.LastSceneName = SceneManager.GetActiveScene().name;
            // Переходим на сцену смерти
            SceneManager.LoadScene(deadSceneName);
        }
        
        if (other.CompareTag("win"))
        {
            SceneManager.LoadScene(winSceneName);
        }
        
        if (other.CompareTag("next"))
        {
            SaveLevel(nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
    private void SaveLevel(string sceneName)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int currentLevel = GetLevelByName(sceneName);

        if (currentLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel);
        }
    }

    protected int GetLevelByName(string sceneName)
    {
        switch (sceneName)
        {
            case "JumpScene":
                return 1;
            case "GravityScene":
                return 2;
            case "TimeScene":
                return 3;
            case "SampleScene":
                return 4;
            default:
                return 1;
        }
    }
}
