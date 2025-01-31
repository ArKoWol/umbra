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

        if (other.CompareTag("win"))
        {
            SceneManager.LoadScene(winSceneName);
        }

        if (other.CompareTag("next"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // Этот метод вызывается, когда персонаж сталкивается с объектом типа "enemy" (коллайдер)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что объект, с которым произошло столкновение, имеет тег "enemy"
        if (collision.collider.CompareTag("enemy"))
        {
            // Телепортируем персонажа на стартовую позицию
            SceneManager.LoadScene(deadSceneName);
        }
    }
}
