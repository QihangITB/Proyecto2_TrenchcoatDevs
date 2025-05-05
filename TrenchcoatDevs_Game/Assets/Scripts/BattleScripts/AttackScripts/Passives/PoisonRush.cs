using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonRush", menuName = "Passives/PoisonRush")]
public class PoisonRush : APassive
{
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
        //Solo necesita existir para un check
    }
}
