using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameResult : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject gameOverScreen;

    private void Awake()
    {
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    private void Start()
    {
        bool youLose = PlayerPrefs.GetInt("gameresult") == 0;

        if (youLose)
        {
            gameOverScreen.SetActive(true);
        }
        else
        {
            winScreen.SetActive(true);
        }

        PlayerPrefs.DeleteKey("gameresult");
    }
}
