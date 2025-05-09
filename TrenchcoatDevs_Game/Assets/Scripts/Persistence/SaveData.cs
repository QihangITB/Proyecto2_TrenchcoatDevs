using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static string nodeMapFileName = "nodemapdata";
    public static string teamFileName = "teamdata";
    public void SaveNodeMap()
    {
        NodeMapGeneration nmg = FindObjectOfType<NodeMapGeneration>();
        JsonDataManager.SaveJsonToJson(nmg.ToString(), nodeMapFileName);
    }

    public void SaveTeam()
    {
        CharacterSaveData data = new CharacterSaveData();

        List<CharacterOutOfBattle> onTeamCharacters = Resources
            .FindObjectsOfTypeAll<CharacterOutOfBattle>()
            .Where(c => c.gameObject.scene.IsValid()) // Solo en escena (no assets)
            .ToList();

        data.characters = new List<CharacterJson>
        {
            onTeamCharacters[0].ConvertToJsonClass(),
            onTeamCharacters[1].ConvertToJsonClass(),
            onTeamCharacters[2].ConvertToJsonClass()
        };

        data.recruitment = new List<string>();

        foreach (APlayer character in RecruitScreen.Instance.allCharacters)
        {
            data.recruitment.Add(character.characterName);
        }

        JsonDataManager.SaveDataToJson(data, teamFileName);
    }
}
