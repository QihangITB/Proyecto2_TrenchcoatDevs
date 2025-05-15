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
        bool isWin = PlayerPrefs.GetInt("gameresult") == 1;

        if (isWin)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }

        PlayerPrefs.DeleteKey("gameresult");
    }
}
