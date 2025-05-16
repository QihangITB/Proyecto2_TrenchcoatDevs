using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetNodeMapGeneration : NodeMapGeneration
{
    [SerializeField]
    PlayerNetGhost _playerNetGhostPrefab;
    [SerializeField]
    GameObject _onlineFightP;
    [SerializeField]
    GameObject _onlineFightNode;
    private void Awake()
    {
        SeedSync seedSyncer = SeedSyncManager.SeedSyncer;
        Random.InitState(seedSyncer.GetSyncedSeed());
    }
    protected override void Start()
    {
        base.Start();
        if (NetworkManager.Singleton.IsServer)
        {
            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClients.Values)
            {
                NetworkObject ghostNetObject = _playerNetGhostPrefab.GetComponent<NetworkObject>();
                NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(ghostNetObject, client.ClientId,true,default,default,Player.transform.position,Player.transform.rotation);
            }
        }
        AllLevels.Add(new List<GameObject>() { _onlineFightNode});
        //Because the base.Start creates a new List and then hides the element, any new element created in the list would be replaced by a new list
        //so HideNodes has to be called twice so the multiplayer implementation does not alter any modificaction to singleplayer and multiplayer can
        //reflect any change made on the singleplayer.
        HideNodes();
    }
    protected override void GenerateStaticNodes()
    {
        base.GenerateStaticNodes();
        SetNode(_onlineFightNode, _onlineFightP);
    }
    protected override void GeneratePath(GameObject pathPrefab)
    {
        base.GeneratePath(pathPrefab);
        SetPath(BossLevel,_onlineFightNode,Path);
    }
}

