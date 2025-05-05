using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroupStretchPassive", menuName = "Passives/GroupStretchPassive")]
public class GroupStretchPassive : APassive
{
    public AAttack GroupStretch;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(GroupStretch))
        {
            player.knownAttacks.Add(GroupStretch);
        }
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
