using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireFountainPassive", menuName = "Passives/FireFountainPassive")]
public class FireFountainPassive : APassive
{
    public AAttack FireFountain;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(FireFountain))
        {
            player.knownAttacks.Add(FireFountain);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
