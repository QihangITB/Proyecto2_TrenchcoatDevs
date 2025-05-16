using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatMorePassive", menuName = "Passives/EatMorePassive")]

public class EatMorePassive : APassive
{
    public AAttack EatMore;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(EatMore))
        {
            player.knownAttacks.Add(EatMore);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
