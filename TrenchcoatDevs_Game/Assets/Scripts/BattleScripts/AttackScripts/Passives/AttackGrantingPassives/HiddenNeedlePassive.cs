using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HiddenNeedlePassive", menuName = "Passives/HiddenNeedlePassive")]
public class HiddenNeedlePassive : APassive
{
    public AAttack HiddenNeedle;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(HiddenNeedle))
        {
            player.knownAttacks.Add(HiddenNeedle);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
