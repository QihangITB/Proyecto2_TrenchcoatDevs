using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

[System.Serializable]
public class NodeMapGeneration : MonoBehaviour
{
    public const string PlayerTag = "Player";
    public const string PathName = "Path";

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

    private List<List<GameObject>> _allLevels;
    private List<List<GameObject>> _actionLevels;
    private GameObject _player;

    protected GameObject Player
    {
        get { return _player; }
    }
    protected virtual void Start()
    {
        _allLevels = new List<List<GameObject>> {
            new List<GameObject> { TutorialNode },
            new List<GameObject> { StartNode },
            FirstLevel,
            SecondLevel,
            ThirdLevel,
            FourthLevel,
            FifthLevel,
            new List<GameObject> { BossLevel }
        };
        _actionLevels = new List<List<GameObject>> {
            FirstLevel,
            SecondLevel,
            ThirdLevel,
            FourthLevel,
            FifthLevel
        };
        _player = GameObject.FindWithTag(PlayerTag);

        // Static nodes
        GenerateStaticNodes();

        // Random nodes
        GenerateSpecialNodes();

        // Fill empty nodes
        FillEmptyNodes(BattleP);

        // NodePaths
        GeneratePath(Path);

        // Hide nodes
        HideNodes();

        NodeInteraction.OnPlayerArrivesToNode += ShowNodesBelowPlayerLevel;
        NodeAccess.OnReturnToNodeMap += ShowNextNodes;
    }

    private void OnDestroy()
    {
        NodeInteraction.OnPlayerArrivesToNode -= ShowNodesBelowPlayerLevel;
        NodeAccess.OnReturnToNodeMap -= ShowNextNodes;
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
            int levelNum = Random.Range(0, _actionLevels.Count);
            int maxNodes = (levelNum == 0 || levelNum == _actionLevels.Count - 1) ? 3 : _actionLevels[levelNum].Count;
            int nodeNum = Random.Range(0, maxNodes);

            if (IsEmptyNode(levelNum, nodeNum))
            {
                SetNode(_actionLevels[levelNum][nodeNum], nodePrefab);
                count--;
            }
        }
    }

    private void FillEmptyNodes(GameObject nodePrefab)
    {
        foreach (List<GameObject> node in _actionLevels)
        {
            foreach (GameObject nodeObj in node)
            {
                if (IsEmptyNode(_actionLevels.IndexOf(node), node.IndexOf(nodeObj)))
                {
                    SetNode(nodeObj, nodePrefab);
                }
            }
        }
    }

    private bool IsEmptyNode(int levelNum, int nodeNum)
    {
        return _actionLevels[levelNum][nodeNum].transform.childCount == 0;
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

        for (int i = 0; i < _actionLevels.Count - 1; i++)
        {
            ConnectNodesWithPath(_actionLevels[i], _actionLevels[i + 1], pathPrefab);
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
        Vector3 direction = origin.transform.position - destination.transform.position;
        Instantiate(pathPrefab, destination.transform.position, Quaternion.LookRotation(direction), destination.transform);
    }

    private List<Transform> GetPathsToPreviousNode(GameObject node)
    {
        List<Transform> paths = new List<Transform>();
        foreach (Transform child in node.transform)
        {
            if (child.name.Contains(PathName))
            {
                paths.Add(child);
            }
        }
        return paths;
    }

    // PLAYER LEVEL
    private void HideNodes()
    {
        // Ocultamos todos los nodos
        foreach (List<GameObject> level in _allLevels)
        {
            foreach (GameObject node in level)
            {
                node.SetActive(false);
            }
        }

        // Excepto el primer nodo
        TutorialNode.SetActive(true);
    }

    private int GetPlayerLevel()
    {
        for (int i = 0; i < _allLevels.Count; i++)
        {
            foreach (GameObject node in _allLevels[i])
            {
                if (Vector3.Distance(_player.transform.position, node.transform.position) < 2f)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private void ShowNodesBelowPlayerLevel(GameObject n)
    {
        ShowNodesBelowPlayerLevel();
    }

    private void ShowNodesBelowPlayerLevel()
    {
        int playerLevel = GetPlayerLevel();

        if (playerLevel >= 0)
        {
            for (int i = 0; i <= playerLevel; i++)
            {
                foreach (GameObject node in _allLevels[i])
                {
                    node.SetActive(true);

                    if (i == playerLevel)
                    {
                        DisableNodeInteraction(node);
                    }
                }
            }
        }
    }

    private void ShowNextNodes()
    {
        int nextLevel = GetPlayerLevel() + 1;

        foreach (GameObject node in _allLevels[nextLevel])
        {
            node.SetActive(true);

            bool pathFinded = false;
            int j = 0;
            List<Transform> paths = GetPathsToPreviousNode(node);
            while (j < paths.Count && !pathFinded)
            {
                Vector3 dir = paths[j].position + paths[j].forward * 10f;
                Vector3 pathDirection = dir - _player.transform.position;
                Vector3 nodeDirection = node.transform.position - _player.transform.position;

                Debug.DrawLine(_player.transform.position, dir, Color.red, 20f);
                Debug.DrawLine(_player.transform.position, node.transform.position, Color.blue, 20f);

                float angle = Vector3.Angle(pathDirection, nodeDirection);
                NodeInteraction interaction = node.GetComponentInChildren<NodeInteraction>();

                if (angle < 2f)
                {
                    interaction.IsInteractable = true;
                    pathFinded = true;
                }
                else
                {
                    interaction.IsInteractable = false;
                    j++;
                }
            }
        }
    }

    private void DisableNodeInteraction(GameObject node)
    {
        SphereCollider sc = node.GetComponentInChildren<SphereCollider>();
        if (sc != null)
        {
            sc.enabled = false;
        }
    }



















    public override string ToString()
    {
        LevelData levelData = new LevelData();

        levelData.tutorial = TutorialNode.transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.start = StartNode.transform.GetChild(0).name.Replace("(Clone)", "");

        levelData.level1 = new Level();
        levelData.level1.node1 = FirstLevel[0].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level1.node2 = FirstLevel[1].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level1.node3 = FirstLevel[2].transform.GetChild(0).name.Replace("(Clone)", "");

        levelData.level2 = new Level();
        levelData.level2.node1 = SecondLevel[0].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level2.node2 = SecondLevel[1].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level2.node3 = SecondLevel[2].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level2.node4 = SecondLevel[3].transform.GetChild(0).name.Replace("(Clone)", "");

        levelData.level3 = new Level();
        levelData.level3.node1 = ThirdLevel[0].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level3.node2 = ThirdLevel[1].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level3.node3 = ThirdLevel[2].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level3.node4 = ThirdLevel[3].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level3.node5 = ThirdLevel[4].transform.GetChild(0).name.Replace("(Clone)", "");

        levelData.level4 = new Level();
        levelData.level4.node1 = FourthLevel[0].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level4.node2 = FourthLevel[1].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level4.node3 = FourthLevel[2].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level4.node4 = FourthLevel[3].transform.GetChild(0).name.Replace("(Clone)", "");

        levelData.level5 = new Level();
        levelData.level5.node1 = FifthLevel[0].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level5.node2 = FifthLevel[1].transform.GetChild(0).name.Replace("(Clone)", "");
        levelData.level5.node3 = FifthLevel[2].transform.GetChild(0).name.Replace("(Clone)", "");

        levelData.boss = BossLevel.transform.GetChild(0).name.Replace("(Clone)", "");

        return JsonUtility.ToJson(levelData, true);
    }
}
