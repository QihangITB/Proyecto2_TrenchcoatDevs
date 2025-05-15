using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DohaBatteryPassive", menuName = "Passives/DohaBatteryPassive")]
public class DohaBatteryPassive : APassive
{
    public AAttack DohaBattery;
    public override void ObtainPassive(CharacterOutOfBattle player)
    {
        if (!player.knownAttacks.Contains(DohaBattery))
        {
            player.knownAttacks.Add(DohaBattery);
        }
        player.knownPassives.Add(this);
    }
    public override void ActivatePassive(CharacterHolder player)
    {
    }
}
