using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MEGAThunderPassive", menuName = "Passives/MEGAThunderPassive")]

public class MEGAThunderPassive : APassive
{
    public AAttack MEGAThunder;
    public override void ActivatePassive(CharacterHolder player)
    {
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(MEGAThunder))
        {
            player.knownAttacks.Add(MEGAThunder);
        }
    }
}
