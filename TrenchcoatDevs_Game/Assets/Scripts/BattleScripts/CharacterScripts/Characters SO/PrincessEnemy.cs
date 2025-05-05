using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrincessEnemy", menuName = "Enemies/PrincessEnemy")]
public class PrincessEnemy : AEnemy
{
    public override void SelectAttack()
    {
        if (BattleManager.instance.currentRound%2==1)
        {
            BattleManager.instance.enemyAttack = BattleManager.instance.user.character.attacks[0];
        }
        else
        {
            BattleManager.instance.enemyAttack = BattleManager.instance.user.character.attacks[1];
        }
        BattleManager.instance.enemyAttack.Effect(BattleManager.instance.players, BattleManager.instance.user);
    }
}
