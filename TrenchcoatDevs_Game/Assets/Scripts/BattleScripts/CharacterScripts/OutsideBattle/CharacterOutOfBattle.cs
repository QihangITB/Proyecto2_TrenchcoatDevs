using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOutOfBattle : MonoBehaviour
{
    public APlayer character;
    public int characterHP;
    public int characterPoisonModifier;
    public List<APassive> knownPassives = new List<APassive>();
    public List<AAttack> knownAttacks = new List<AAttack>();
    public GenericAttack basicAttack;

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
}
