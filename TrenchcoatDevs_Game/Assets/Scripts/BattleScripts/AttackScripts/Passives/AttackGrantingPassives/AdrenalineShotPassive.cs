using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdrenalineDosePassive", menuName = "Passives/AdrenalineDosePassive")]
public class AdrenalineShotPassive : APassive
{
    public AAttack AdrenalineDose;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(AdrenalineDose))
        {
            player.knownAttacks.Add(AdrenalineDose);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
