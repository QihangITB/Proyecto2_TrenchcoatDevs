using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChickenBrewPassive", menuName = "Passives/ChickenBrewPassive")]
public class ChickenBrewPassive : APassive
{
    public AAttack ChickenBrew;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(ChickenBrew))
        {
            player.knownAttacks.Add(ChickenBrew);
        }
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
