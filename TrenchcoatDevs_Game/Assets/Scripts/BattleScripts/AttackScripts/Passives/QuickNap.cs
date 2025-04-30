using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuickNap", menuName = "Passives/QuickNap")]
public class QuickNap : APassive
{
    public override void ActivatePassive(CharacterHolder player)
    {
        player.GetRegenerating();
        player.GetRested();
    }

    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
}
