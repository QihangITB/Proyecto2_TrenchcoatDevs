using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<APlayer> players = new List<APlayer>();
    public List<AEnemy> enemies = new List<AEnemy>();
    public void AllocateCharacters()
    {
        BattleManager.instance.CharacterAllocation(players, enemies);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
