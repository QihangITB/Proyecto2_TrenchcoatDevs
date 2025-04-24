using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortFuse : APassive
{
    public override void ActivatePassive(CharacterHolder player)
    {
        player.speed += 4;
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
}
