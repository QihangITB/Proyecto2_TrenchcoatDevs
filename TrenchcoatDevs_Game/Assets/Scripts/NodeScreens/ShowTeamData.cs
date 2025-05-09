using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowTeamData : MonoBehaviour
{
    public CharacterOutOfBattle data;
    public RawImage image;
    public TMP_Text name;

    private void OnEnable()
    {
        if (data.character != null)
        {
            name.text = data.character.characterName;
        }
    }
}
