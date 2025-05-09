using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class RecruitScreen : MonoBehaviour
{
    public static RecruitScreen Instance;

    [Header("First character")]
    public RawImage imageOne;
    public TMP_Text nameOne;
    public TMP_Text descriptionOne;

    [Header("Second character")]
    public RawImage imageTwo;
    public TMP_Text nameTwo;
    public TMP_Text descriptionTwo;

    [Header("Canvas")]
    public GameObject randomSelectionObject;
    public GameObject teamSelectionObject;

    [Header("Team Data")]
    public List<APlayer> allCharacters;
    public List<APlayer> availableCharacters;
    public List<APassive> allPassives;
    public List<AAttack> allAttacks;

    private APlayer selectedCharacter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (JsonDataManager.FileExists(SaveData.teamFileName))
            {
                CharacterSaveData data = JsonDataManager.LoadFromJson<CharacterSaveData>(SaveData.teamFileName);
                LoadRecruitmentData(data);
            }
            else
            {
                availableCharacters = allCharacters;
            }
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
        int randomOne = Random.Range(0, availableCharacters.Count);
        int randomTwo = Random.Range(0, availableCharacters.Count);

        while(randomOne == randomTwo)
        {
            randomTwo = Random.Range(0, availableCharacters.Count);
        }

        nameOne.text = availableCharacters[randomOne].characterName;
        descriptionOne.text = availableCharacters[randomOne].description;

        nameTwo.text = availableCharacters[randomTwo].characterName;
        descriptionTwo.text = availableCharacters[randomTwo].description;
    }

    public void SelectCharacter(TMP_Text message)
    {
        selectedCharacter = availableCharacters.Find(x => x.characterName == message.text);
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
        team.data.characterPoisonModifier = 1;
        availableCharacters.Remove(selectedCharacter);

        ShowTeamSelection(false);

        NodeAccess nodeAccess = FindObjectOfType<NodeAccess>();
        nodeAccess.OnExitButtonClick();
    }

    private void LoadTeamCharactersData(CharacterSaveData data)
    {
        List<CharacterOutOfBattle> onTeamCharacters = Resources
        .FindObjectsOfTypeAll<CharacterOutOfBattle>()
        .Where(c => c.gameObject.scene.IsValid()) // Solo en escena (no assets)
        .ToList();

        for (int i = 0; i < onTeamCharacters.Count; i++)
        {
            // INT VALUES
            onTeamCharacters[i].characterHP = data.characters[i].characterHP;
            onTeamCharacters[i].characterPoisonModifier = data.characters[i].characterPoisonModifier;
            onTeamCharacters[i].fightsToLevelUp = data.characters[i].fightsToLevelUp;
            onTeamCharacters[i].timesToLevelUp = data.characters[i].timesToLevelUp;
            onTeamCharacters[i].level = data.characters[i].level;

            // PREFABS VALUES
            onTeamCharacters[i].character = allCharacters.Find(x => x.characterName == data.characters[i].character);
            onTeamCharacters[i].basicAttack = allCharacters.Find(x => x.characterName == data.characters[i].character).basicAttack;
            onTeamCharacters[i].knownPassives = new List<APassive>();
            //for (int j = 0; j < data.characters[i].knownPassives.Count; j++)
            //{
            //    if (allCharAbilities.Find(x => x.name.Contains(data.characters[i].knownPassives[j])) != null)
            //    {
            //        onTeamCharacters[i].knownPassives.Add(allCharAbilities.Find(x => x.name.Contains(data.characters[i].knownPassives[j])));
            //    }
            //}
            //onTeamCharacters[i].knownAttacks = new List<AAttack>();
            //for (int j = 0; j < data.characters[i].knownAttacks.Count; j++)
            //{
            //    if (allCharAbilities.Find(x => x.name.Contains(data.characters[i].knownAttacks[j])) != null)
            //    {
            //        onTeamCharacters[i].knownAttacks.Add(allCharAbilities.Find(x => x.name.Contains(data.characters[i].knownAttacks[j])));
            //    }
            //}
        }
    }

    private void LoadRecruitmentData(CharacterSaveData data)
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            if (data.recruitment.Contains(allCharacters[i].characterName))
            {
                availableCharacters.Add(allCharacters[i]);
            }
        }
    }
}
