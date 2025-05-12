using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowTeamData : MonoBehaviour
{
    public CharacterOutOfBattle data;
    public Image image;
    public TMP_Text name;

    private void OnEnable()
    {
        if (data.character != null)
        {
            name.text = data.character.characterName;
            image.sprite = data.character.sprite;
        }
    }
}
