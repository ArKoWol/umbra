using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCollision : MonoBehaviour
{
    [SerializeField] private string winSceneName;
    [SerializeField] private string deadSceneName;
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("chest"))
        {
            ChestCounter.Instance.IncrementChestCount();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("enemy"))
        {
            SceneData.LastSceneName = SceneManager.GetActiveScene().name;
            ChestCounter.Instance.ResetChestCount();
            SceneManager.LoadScene(deadSceneName);
        }

        if (other.CompareTag("win"))
        {
            ChestCounter.Instance.ResetChestCount();
            SceneManager.LoadScene(winSceneName);
        }

        if (other.CompareTag("next"))
        {
            ChestCounter.Instance.ResetChestCount();
            SceneManager.LoadScene(nextSceneName);
        }
    }
}