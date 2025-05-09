using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CharacterOutOfBattle : MonoBehaviour
{
    public APlayer character;
    public int characterHP;
    public int characterPoisonModifier;
    public List<APassive> knownPassives = new List<APassive>();
    public List<AAttack> knownAttacks = new List<AAttack>();
    public GenericAttack basicAttack;
    public int level;
    public int timesToLevelUp;
    public int fightsToLevelUp;

    public void UpdateCharacter()
    {
        characterPoisonModifier = 1;
        //añade todas las passivas y ataques que el personaje conoce a la lista de conocidos si no lo tiene
        foreach (APassive passive in character.passives)
        {
            if (!knownPassives.Contains(passive))
            {
                knownPassives.Add(passive);
            }
        }
        foreach (AAttack attack in character.attacks)
        {
            if (!knownAttacks.Contains(attack))
            {
                knownAttacks.Add(attack);
            }
        }
    }

    public void LevelUp()
    {
        level++;
        characterHP += 2;
        fightsToLevelUp = 2;
    }

    public CharacterJson ConvertToJsonClass()
    {
        CharacterJson data = new CharacterJson
        {
            character = character != null ? character.name : null,
            characterHP = characterHP,
            characterPoisonModifier = characterPoisonModifier,
            knownPassives = knownPassives.Select(p => p != null ? p.name : null).ToList(),
            knownAttacks = knownAttacks.Select(a => a != null ? a.name : null).ToList(),
            basicAttack = basicAttack != null ? basicAttack.name : null,
            level = level,
            timesToLevelUp = timesToLevelUp,
            fightsToLevelUp = fightsToLevelUp
        };
        return data;
    }
}
