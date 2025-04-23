using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<CharacterOutOfBattle> charactersOutOfBattle = new List<CharacterOutOfBattle>();
    public List<APlayer> players = new List<APlayer>();
    public List<AEnemy> enemies = new List<AEnemy>();
    public void AllocateCharacters()
    {
        players.Clear();
        foreach (CharacterOutOfBattle character in charactersOutOfBattle)
        {
            if (character.character != null)
            {
                players.Add(character.character);
                character.UpdateCharacter();
            }
        }
        BattleManager.instance.CharacterAllocation(players, enemies, charactersOutOfBattle);
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
