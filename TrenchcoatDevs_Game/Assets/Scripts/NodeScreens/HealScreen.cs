using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealScreen : MonoBehaviour
{
    public List<CharacterOutOfBattle> characters;
    public TMP_Text successMsg;
    public float waitTime = 1.5f;

    private void OnEnable()
    {
        successMsg.gameObject.SetActive(false);
    }
    public void HealAllCharacters()
    {
        foreach (CharacterOutOfBattle character in characters)
        {
            if (character.character != null)
            {
                character.characterHP = character.character.maxHealth + character.level * 2;
            }
        }

        StartCoroutine(ShowSuccessMessage(waitTime));
    }

    private IEnumerator ShowSuccessMessage(float waitTime)
    {
        successMsg.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        successMsg.gameObject.SetActive(false);

        NodeAccess nodeAccess = FindObjectOfType<NodeAccess>();
        nodeAccess.OnExitButtonClick();
    }

}
