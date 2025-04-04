using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AEnemy : ACharacter
{
    public void SelectAttack()
    {
        int randomIndex = Random.Range(0, BattleManager.instance.user.character.attacks.Count);
        BattleManager.instance.enemyAttack = BattleManager.instance.user.character.attacks[randomIndex];
        BattleManager.instance.enemyAttack.Effect(BattleManager.instance.players, BattleManager.instance.user);
    }
}