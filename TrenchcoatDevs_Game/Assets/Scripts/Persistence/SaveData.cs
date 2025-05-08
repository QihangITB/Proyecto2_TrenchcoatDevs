using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static string nodeMapFileName = "node_map_data";
    public static string characterFileName = "characters_data";
    public void SaveNodeMap(NodeMapGeneration nodeMap)
    {
        JsonDataManager.SaveJsonToJson(nodeMap.ToString(), nodeMapFileName);
    }

    public void SaveCharacters()
    {
        CharacterSaveData data = new CharacterSaveData();

        List<CharacterOutOfBattle> onPartyCharacters = Resources
            .FindObjectsOfTypeAll<CharacterOutOfBattle>()
            .Where(c => c.gameObject.scene.IsValid()) // Solo en escena (no assets)
            .ToList();
        Debug.Log($"Characters in party: {onPartyCharacters.Count}");

        data.charcter1 = onPartyCharacters[0].ConvertToCharacterJson();
        data.charcter2 = onPartyCharacters[1].ConvertToCharacterJson();
        data.charcter3 = onPartyCharacters[2].ConvertToCharacterJson();
        data.recruitment = new List<string>();

        Debug.Log($"Character 1: {data.charcter1.character}");

        JsonDataManager.SaveDataToJson(data, characterFileName);
    }
}
