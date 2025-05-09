using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static string nodeMapFileName = "node_map_data";
    public static string teamFileName = "team_data";
    public void SaveNodeMap()
    {
        NodeMapGeneration nmg = FindObjectOfType<NodeMapGeneration>();
        JsonDataManager.SaveJsonToJson(nmg.ToString(), nodeMapFileName);
    }

    public void SaveTeam()
    {
        CharacterSaveData data = new CharacterSaveData();

        List<CharacterOutOfBattle> onPartyCharacters = Resources
            .FindObjectsOfTypeAll<CharacterOutOfBattle>()
            .Where(c => c.gameObject.scene.IsValid()) // Solo en escena (no assets)
            .ToList();

        data.charcter1 = onPartyCharacters[0].ConvertToJsonClass();
        data.charcter2 = onPartyCharacters[1].ConvertToJsonClass();
        data.charcter3 = onPartyCharacters[2].ConvertToJsonClass();
        data.recruitment = new List<string>();

        foreach (APlayer character in RecruitScreen.Instance.allCharacters)
        {
            data.recruitment.Add(character.characterName);
        }

        JsonDataManager.SaveDataToJson(data, teamFileName);
    }
}
