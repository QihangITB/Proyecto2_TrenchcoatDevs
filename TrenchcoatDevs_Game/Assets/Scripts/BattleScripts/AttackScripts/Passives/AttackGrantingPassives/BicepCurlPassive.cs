using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BicepCurlPassive", menuName = "Passives/BicepCurlPassive")]
public class BicepCurlPassive : APassive
{
    public AAttack BicepCurl;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(BicepCurl))
        {
            player.knownAttacks.Add(BicepCurl);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
