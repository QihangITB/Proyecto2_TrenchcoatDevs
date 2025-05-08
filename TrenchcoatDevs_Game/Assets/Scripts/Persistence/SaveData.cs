using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void SaveNodeMap(NodeMapGeneration nodeMap)
    {
        JsonDataManager.SaveJsonToJson(nodeMap.ToString(), "gamedata");
    }
}
