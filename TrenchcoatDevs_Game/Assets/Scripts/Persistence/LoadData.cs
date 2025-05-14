using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadData : MonoBehaviour
{
    public static bool hasToLoad = false;
    public TMP_Text error;

    private void Awake()
    {
        error.gameObject.SetActive(false);
    }

    public void Load()
    {
        if(!JsonDataManager.FileExists(SaveData.nodeMapFileName) && !JsonDataManager.FileExists(SaveData.teamFileName))
        {
            error.gameObject.SetActive(true);
            hasToLoad = false;
        }
        else
        {
            hasToLoad = true;
            SceneController.Instance.LoadScene("Singleplay");
        }
    }
}
