using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light it up", menuName = "Passives/Light it up", order = 1)]

public class Lightitup : APassive
{
    public GenericAttack attack;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
        player.basicAttack = attack;
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        //esta no hace nada dentro del combate como tal
    }
}
