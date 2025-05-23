using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : ACharacter
{
    public virtual void SelectAttack()
    {
        int randomIndex = Random.Range(0, BattleManager.instance.user.character.attacks.Count);
        BattleManager.instance.enemyAttack = BattleManager.instance.user.character.attacks[randomIndex];
        BattleManager.instance.enemyAttack.Effect(BattleManager.instance.players, BattleManager.instance.user);
    }
}