using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

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

    [Header("Canvas")]
    public GameObject randomSelectionObject;
    public GameObject teamSelectionObject;

    [Header("Team Data")]
    public List<APassive> allPassives;
    public List<AAttack> allAttacks;
    public List<APlayer> allCharacters;
    private List<APlayer> availableCharacters;

    private APlayer selectedCharacter;

    public List<APlayer> AvailableCharacters { get { return availableCharacters; } }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            if (LoadData.hasToLoad)
            {
                CharacterSaveData data = JsonDataManager.LoadFromJson<CharacterSaveData>(SaveData.teamFileName);
                LoadTeamCharactersData(data.characters);
                availableCharacters = LoadRecruitmentData(data.recruitment);
            }
            else
            {
                AssignFirstCharacter(SelectFirstChar.startCharacter);
                availableCharacters = allCharacters;
                availableCharacters.Remove(SelectFirstChar.startCharacter);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
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
        imageOne.sprite = availableCharacters[randomOne].sprite;

        nameTwo.text = availableCharacters[randomTwo].characterName;
        descriptionTwo.text = availableCharacters[randomTwo].description;
        imageTwo.sprite = availableCharacters[randomTwo].sprite;
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

    private void AssignFirstCharacter(APlayer data)
    {
        List<CharacterOutOfBattle> onTeamCharacters = Resources
        .FindObjectsOfTypeAll<CharacterOutOfBattle>()
        .Where(c => c.gameObject.scene.IsValid()) // Solo en escena (no assets)
        .ToList();

        for (int i = 0; i < onTeamCharacters.Count; i++)
        {
            if (i==0)
            {
                onTeamCharacters[i].character = data;
                onTeamCharacters[i].characterHP = data.maxHealth;
                onTeamCharacters[i].knownPassives = new List<APassive>(data.passives);
                onTeamCharacters[i].knownAttacks = new List<AAttack>(data.attacks);
                onTeamCharacters[i].basicAttack = data.basicAttack;
                onTeamCharacters[i].fightsToLevelUp = 2;
                onTeamCharacters[i].timesToLevelUp = 0;
                onTeamCharacters[i].level = 1;
                onTeamCharacters[i].characterPoisonModifier = 1;
            }
            else
            {
                onTeamCharacters[i].character = null;
                onTeamCharacters[i].characterHP = 0;
                onTeamCharacters[i].knownPassives = new List<APassive>();
                onTeamCharacters[i].knownAttacks = new List<AAttack>();
                onTeamCharacters[i].basicAttack = null;
                onTeamCharacters[i].fightsToLevelUp = 2;
                onTeamCharacters[i].timesToLevelUp = 0;
                onTeamCharacters[i].level = 1;
                onTeamCharacters[i].characterPoisonModifier = 1;
            }
        }
    }

    // LOAD PERSISTENCE DATA
    private void LoadTeamCharactersData(List<CharacterJson> characters)
    {
        List<CharacterOutOfBattle> onTeamCharacters = Resources
        .FindObjectsOfTypeAll<CharacterOutOfBattle>()
        .Where(c => c.gameObject.scene.IsValid()) // Solo en escena (no assets)
        .ToList();

        for (int i = 0; i < onTeamCharacters.Count; i++)
        {
            // INT VALUES
            onTeamCharacters[i].characterHP = characters[i].characterHP;
            onTeamCharacters[i].characterPoisonModifier = characters[i].characterPoisonModifier;
            onTeamCharacters[i].fightsToLevelUp = characters[i].fightsToLevelUp;
            onTeamCharacters[i].timesToLevelUp = characters[i].timesToLevelUp;
            onTeamCharacters[i].level = characters[i].level;

            // PREFABS VALUES
            onTeamCharacters[i].character = allCharacters.Find(x => characters[i].character.Contains(x.characterName));
            onTeamCharacters[i].basicAttack = allCharacters.Find(x => characters[i].character.Contains(x.characterName)).basicAttack;
            onTeamCharacters[i].knownPassives = new List<APassive>();
            for (int j = 0; j < characters[i].knownPassives.Count; j++)
            {
                if (allPassives.Find(x => x.name.Contains(characters[i].knownPassives[j])) != null)
                {
                    onTeamCharacters[i].knownPassives.Add(
                        allPassives.Find(
                            x => x.name.Contains(characters[i].knownPassives[j])
                            )
                        );
                }
            }
            onTeamCharacters[i].knownAttacks = new List<AAttack>();
            for (int j = 0; j < characters[i].knownAttacks.Count; j++)
            {
                if (allAttacks.Find(x => x.name.Contains(characters[i].knownAttacks[j])) != null)
                {
                    onTeamCharacters[i].knownAttacks.Add(
                        allAttacks.Find(
                            x => x.name.Contains(characters[i].knownAttacks[j])
                            )
                        );
                }
            }
        }
    }

    private List<APlayer> LoadRecruitmentData(List<string> recruitment)
    {
        List<APlayer> availables = new List<APlayer>();
        for (int i = 0; i < allCharacters.Count; i++)
        {
            if (recruitment.Contains(allCharacters[i].characterName))
            {
                availables.Add(allCharacters[i]);
            }
        }
        return availables;
    }
}
