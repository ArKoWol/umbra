using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                buttons[i].interactable = true;
            }
            else 
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(GetLevelName(level));
    }

    protected string GetLevelName(int level)
    {
        switch (level)
        {
            case 1:
                return "JumpScene";
            case 2:
                return "GravityScene";
            case 3:
                return "TimeScene";
            case 4:
                return "SampleScene";
            default:
                return "JumpScene";
        }
    }

}
