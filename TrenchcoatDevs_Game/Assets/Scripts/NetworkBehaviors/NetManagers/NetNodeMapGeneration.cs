using Unity.Netcode;
using UnityEngine;

public class NetNodeMapGeneration : NodeMapGeneration
{
    [SerializeField]
    PlayerNetGhost _playerNetGhostPrefab;
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
    }
}

