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
            if (ChestCounter.Instance != null)
            {
                ChestCounter.Instance.IncrementChestCount();
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("enemy"))
        {
            SceneData.LastSceneName = SceneManager.GetActiveScene().name;
            if (ChestCounter.Instance != null)
            {
                ChestCounter.Instance.ResetChestCount();
            }
            SceneManager.LoadScene(deadSceneName);
        }

        if (other.CompareTag("win"))
        {
            if (ChestCounter.Instance != null)
            {
                ChestCounter.Instance.ResetChestCount();
            }
            SceneManager.LoadScene(winSceneName);
        }

        if (other.CompareTag("next"))
        {
            if (ChestCounter.Instance != null)
            {
                ChestCounter.Instance.ResetChestCount();
            }
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not set.");
            }
        }
    }
}