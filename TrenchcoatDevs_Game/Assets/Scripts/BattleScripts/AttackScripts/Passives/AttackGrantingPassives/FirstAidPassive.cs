using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstAidPassive", menuName = "Passives/FirstAidPassive")]
public class FirstAidPassive : APassive
{
    public AAttack FirstAid;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(FirstAid))
        {
            player.knownAttacks.Add(FirstAid);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
