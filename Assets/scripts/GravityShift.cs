using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityShift : MonoBehaviour
{
    public bool mIsGravityUp = false; // Публичная переменная для доступа из других скриптов
    private float mGravity = 9.81f * 2.0f; // Ускорение гравитации
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Проверяем, активна ли сцена, где должен работать скрипт
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "GravityScene" && sceneName != "TimeScene" && sceneName != "SampleScene")
        {
            Debug.LogWarning("GravityShift отключен на сцене: " + sceneName);
            enabled = false;
            return;
        }

        // Получаем компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден на объекте: " + gameObject.name);
            enabled = false;
            return;
        }

        // Устанавливаем начальную гравитацию
        SetGravity();
    }

    void Update()
    {
        if (!enabled) return;

        // Проверяем, была ли нажата клавиша для переключения гравитации
        if (IsToggleGravity())
        {
            ToggleGravity();
            SetGravity();
        }
    }

    // Проверка нажатия клавиши для переключения гравитации
    protected bool IsToggleGravity()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    // Переключение направления гравитации
    protected void ToggleGravity()
    {
        mIsGravityUp = !mIsGravityUp;
        Debug.Log("Гравитация переключена: " + (mIsGravityUp ? "Вверх" : "Вниз"));
    }

    // Установка гравитации в зависимости от состояния
    protected void SetGravity()
    {
        if (mIsGravityUp)
        {
            Physics2D.gravity = Vector2.up * mGravity;
            spriteRenderer.flipY = true;
        }
        else
        {
            Physics2D.gravity = Vector2.down * mGravity;
            spriteRenderer.flipY = false;
        }
    }
}