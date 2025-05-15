using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameResult : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    private void Awake()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Start()
    {
        bool youLose = PlayerPrefs.GetInt("gameresult") == 0;

        if (youLose)
        {
            loseScreen.SetActive(true);
        }
        else
        {
            winScreen.SetActive(true);
        }

        PlayerPrefs.DeleteKey("gameresult");
    }
}
