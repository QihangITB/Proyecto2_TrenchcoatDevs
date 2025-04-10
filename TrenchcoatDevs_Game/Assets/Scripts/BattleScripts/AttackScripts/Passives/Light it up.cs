using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light it up", menuName = "Passives/Light it up", order = 1)]

public class Lightitup : APassive
{
    public GenericAttack attack;
    public override void ActivatePassive(ACharacter player)
    {
        player.basicAttack = attack;
    }
}
