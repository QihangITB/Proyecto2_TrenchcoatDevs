using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeMapGeneration : MonoBehaviour
{
    [Header("Node Prefabs")]
    public GameObject BattleP;
    public GameObject HealthP;
    public GameObject ShopP;
    public GameObject CharacterP;
    public GameObject TreasureP;

    [Header("Configuration")]
    public int HealthNodeNum;
    public int ShopNodeNum;
    public int CharacterNodeNum;
    public int TreasureNodeNum;

    [Header("Node Levels")]
    public GameObject TutorialNode;
    public GameObject StartNode;
    public GameObject BossLevel;
    public List<GameObject> FirstLevel;
    public List<GameObject> SecondLevel;
    public List<GameObject> ThirdLevel;
    public List<GameObject> FourthLevel;
    public List<GameObject> FifthLevel;

    [Header("Path Prefab")]
    public GameObject Path;

    private List<List<GameObject>> allLevels;

    void Start()
    {
        allLevels = new List<List<GameObject>> { FirstLevel, SecondLevel, ThirdLevel, FourthLevel, FifthLevel };

        // Static nodes
        InitializeStaticNodes();

        // Random nodes
        GenerateSpecialNodes();
    }
    private void InitializeStaticNodes()
    {
        SetNode(TutorialNode, BattleP);
        SetNode(StartNode, CharacterP);
        SetNode(BossLevel, BattleP);

        SetPath(TutorialNode, StartNode, Path);
        SetPath(StartNode, FirstLevel[0], Path);

    }

    private void GenerateSpecialNodes()
    {
        GenerateNodeRandomly(HealthP, HealthNodeNum);
        GenerateNodeRandomly(ShopP, ShopNodeNum);
        GenerateNodeRandomly(CharacterP, CharacterNodeNum);
        GenerateNodeRandomly(TreasureP, TreasureNodeNum);
    }

    private void GenerateNodeRandomly(GameObject nodePrefab, int count)
    {
        while (count > 0)
        {
            int levelNum = Random.Range(0, allLevels.Count);
            int maxNodes = (levelNum == 0 || levelNum == allLevels.Count - 1) ? 3 : allLevels[levelNum].Count;
            int nodeNum = Random.Range(0, maxNodes);

            if (IsEmptyNode(levelNum, nodeNum))
            {
                SetNode(allLevels[levelNum][nodeNum], nodePrefab);
                count--;
            }
        }
    }

    private bool IsEmptyNode(int levelNum, int nodeNum)
    {
        return allLevels[levelNum][nodeNum].transform.childCount == 0;
    }

    private void SetNode(GameObject node, GameObject nodePrefab)
    {
        Instantiate(nodePrefab, node.transform.position, node.transform.rotation, node.transform);
    }

    private void SetPath(GameObject origin, GameObject destination, GameObject pathPrefab)
    {
        // coger la direccion del origen al destino
        Vector3 direction = destination.transform.position - origin.transform.position;

        Instantiate(pathPrefab, origin.transform.position, Quaternion.LookRotation(direction), origin.transform);
    }
}
