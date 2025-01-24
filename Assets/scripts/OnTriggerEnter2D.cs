using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCollision : MonoBehaviour
{
    // Начальная позиция для телепортации

    [SerializeField] private string winSceneName;
    
    [SerializeField] private string deadSceneName;

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
            // Телепортируем персонажа на стартовую позицию
            SceneManager.LoadScene(deadSceneName);
        }
        
        if (other.CompareTag("win"))
        {
            SceneManager.LoadScene(winSceneName);
        }
    }
}
