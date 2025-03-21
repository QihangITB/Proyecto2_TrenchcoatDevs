using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeMapGeneration : MonoBehaviour
{
    [Header("Node Prefabs")]
    public GameObject Battle;
    public GameObject Health;
    public GameObject Shop;
    public GameObject Character;
    public GameObject Treasure;

    [Header("Configuration")]
    public int HealthNodeNum;
    public int ShopNodeNum;
    public int CharacterNodeNum;
    public int TreasureNodeNum;

    [Header("Node Levels")]
    public GameObject TutorialNode;
    public GameObject StartNode;
    public List<GameObject> FirstLevel;
    public List<GameObject> SecondLevel;
    public List<GameObject> ThirdLevel;
    public List<GameObject> FourthLevel;
    public List<GameObject> FifthLevel;
    public GameObject BossLevel;

    void Start()
    {
        // Static nodes
        SetNode(TutorialNode, Battle);
        SetNode(StartNode, Character);
        SetNode(BossLevel, Battle);

        // Random nodes
        GenerateSpecialNode(Health, HealthNodeNum);
        GenerateSpecialNode(Shop, ShopNodeNum);
        GenerateSpecialNode(Character, CharacterNodeNum);
        GenerateSpecialNode(Treasure, TreasureNodeNum);

    }

    void Update()
    {
        
    }

    private void GenerateSpecialNode(GameObject node, int generationNum)
    {
        int levelNum, nodeNum;
        levelNum = Random.Range(0, 5);
        nodeNum = (levelNum == 4 || levelNum == 0) ? Random.Range(1, 4) : Random.Range(0, 5);

        while (generationNum > 0)
        {
            if(IsEmptyPosition(levelNum, nodeNum))
            {
                switch (levelNum)
                {
                    case 0:
                        SetNode(FirstLevel[nodeNum], node);
                        break;
                    case 1:
                        SetNode(SecondLevel[nodeNum], node);
                        break;
                    case 2:
                        SetNode(ThirdLevel[nodeNum], node);
                        break;
                    case 3:
                        SetNode(FourthLevel[nodeNum], node);
                        break;
                    case 4:
                        SetNode(FifthLevel[nodeNum], node);
                        break;
                    default:
                        Debug.LogError("Invalid value");
                        break;
                }

                generationNum--;
            }
        }
    }

    private bool IsEmptyPosition(int LevelNum, int NodeNum)
    {
        switch (LevelNum)
        {
            case 0:
                return FirstLevel[NodeNum].transform.childCount == 0;
            case 1:
                return SecondLevel[NodeNum].transform.childCount == 0;
            case 2:
                return ThirdLevel[NodeNum].transform.childCount == 0;
            case 3:
                return FourthLevel[NodeNum].transform.childCount == 0;
            case 4:
                return FifthLevel[NodeNum].transform.childCount == 0;
            default:
                Debug.LogError("Invalid value");
                return false;
        }

    }

    private void SetNode(GameObject node, GameObject prefab)
    {
        Instantiate(prefab, node.transform.position, node.transform.rotation, node.transform);
    }
}
