using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecruitScreen : MonoBehaviour
{
    public static RecruitScreen Instance;

    [Header("First character")]
    public Image imageOne;
    public TMP_Text nameOne;
    public TMP_Text descriptionOne;

    [Header("Second character")]
    public Image imageTwo;
    public TMP_Text nameTwo;
    public TMP_Text descriptionTwo;

    public List<APlayer> allCharacters;
    private APlayer selectedCharacter;

    [Header("Canvas")]
    public GameObject randomSelectionObject;
    public GameObject teamSelectionObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        ShowRandomCharacter();
    }

    private void ShowRandomCharacter()
    {
        int randomOne = Random.Range(0, allCharacters.Count);
        int randomTwo = Random.Range(0, allCharacters.Count);

        while(randomOne == randomTwo)
        {
            randomTwo = Random.Range(0, allCharacters.Count);
        }

        nameOne.text = allCharacters[randomOne].characterName;
        descriptionOne.text = allCharacters[randomOne].description;
        imageOne.sprite = allCharacters[randomOne].sprite;

        nameTwo.text = allCharacters[randomTwo].characterName;
        descriptionTwo.text = allCharacters[randomTwo].description;
        imageTwo.sprite = allCharacters[randomTwo].sprite;

    }

    public void SelectCharacter(TMP_Text message)
    {
        selectedCharacter = allCharacters.Find(x => x.characterName == message.text);
        ShowTeamSelection(true);
    }

    private void ShowTeamSelection(bool isTeamSelectionActive)
    {
        if(isTeamSelectionActive)
        {
            teamSelectionObject.SetActive(true);
            randomSelectionObject.SetActive(false);
        }
        else
        {
            teamSelectionObject.SetActive(false);
            randomSelectionObject.SetActive(true);
        }
    }

    public void EquipCharacter(ShowTeamData team)
    {
        team.data.character = selectedCharacter;
        team.data.characterHP = selectedCharacter.maxHealth;
        team.data.knownPassives = new List<APassive>(selectedCharacter.passives);
        team.data.knownAttacks = new List<AAttack>(selectedCharacter.attacks);
        team.data.basicAttack = selectedCharacter.basicAttack;
        team.data.fightsToLevelUp = 2;
        team.data.timesToLevelUp = team.data.level-1;
        team.data.level = 1;
        allCharacters.Remove(selectedCharacter);

        ShowTeamSelection(false);

        NodeAccess nodeAccess = FindObjectOfType<NodeAccess>();
        nodeAccess.OnExitButtonClick();
    }
}
