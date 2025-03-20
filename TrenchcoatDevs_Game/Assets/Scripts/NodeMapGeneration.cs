using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMapGeneration : MonoBehaviour
{
    [Header("Node Prefabs")]
    public GameObject Battle;
    public GameObject Health;
    public GameObject Shop;
    public GameObject Character;
    public GameObject Treasure;

    [Header("Node Levels")]
    public GameObject StartNode;
    public List<GameObject> FirstLevel;
    public List<GameObject> SecondLevel;
    public List<GameObject> ThirdLevel;
    public List<GameObject> FourthLevel;
    public List<GameObject> FifthLevel;
    public GameObject BossLevel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
