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
        GenerateStaticNodes();

        // Random nodes
        GenerateSpecialNodes();

        // Fill empty nodes
        FillEmptyNodes(BattleP);

        // NodePaths
        GeneratePath(Path);
    }

    // NODES
    private void GenerateStaticNodes()
    {
        SetNode(TutorialNode, BattleP);
        SetNode(BossLevel, BattleP);

        SetNode(StartNode, CharacterP);
        SetNode(ThirdLevel[1], CharacterP);
        SetNode(ThirdLevel[3], CharacterP);
    }

    private void GenerateSpecialNodes()
    {
        GenerateNodeRandomly(HealthP, HealthNodeNum);
        GenerateNodeRandomly(ShopP, ShopNodeNum);
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

    private void FillEmptyNodes(GameObject nodePrefab)
    {
        foreach (List<GameObject> node in allLevels)
        {
            foreach (GameObject nodeObj in node)
            {
                if (IsEmptyNode(allLevels.IndexOf(node), node.IndexOf(nodeObj)))
                {
                    SetNode(nodeObj, nodePrefab);
                }
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


    // PATH
    private void GeneratePath(GameObject pathPrefab)
    {
        SetPath(TutorialNode, StartNode, pathPrefab);

        foreach (GameObject node in FirstLevel)
        {
            SetPath(StartNode, node, pathPrefab);
        }

        foreach (GameObject node in FifthLevel)
        {
            SetPath(node, BossLevel, pathPrefab);
        }

        for (int i = 0; i < allLevels.Count - 1; i++)
        {
            ConnectNodesWithPath(allLevels[i], allLevels[i + 1], pathPrefab);
        }
    }

    private void ConnectNodesWithPath(List<GameObject> currentLevel, List<GameObject> nextLevel, GameObject pathPrefab)
    {
        for (int i = 0; i < currentLevel.Count; i++)
        {
            if (currentLevel.Count < nextLevel.Count)
            {
                SetPath(currentLevel[i], nextLevel[i], pathPrefab);
                if (i + 1 < nextLevel.Count)
                    SetPath(currentLevel[i], nextLevel[i + 1], pathPrefab);
            }
            else
            {
                if (i < nextLevel.Count)
                    SetPath(currentLevel[i], nextLevel[i], pathPrefab);
                if (i - 1 >= 0)
                    SetPath(currentLevel[i], nextLevel[i - 1], pathPrefab);
            }
        }
    }

    private void SetPath(GameObject origin, GameObject destination, GameObject pathPrefab)
    {
        Vector3 direction = destination.transform.position - origin.transform.position;
        Instantiate(pathPrefab, origin.transform.position, Quaternion.LookRotation(direction), origin.transform);
    }
}
