using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttackEnemy", menuName = "EnemyAttacks/BasicAttackEnemy")]
public class BasicAttack : GenericAttack
{
    int iterationLimit;
    public override void ActivateTargetButtons()
    {
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        int randomIndex = Random.Range(0, BattleManager.instance.players.Count);
        iterationLimit = 500; //si no se pone unity lo cuenta como bucle infinito
        while (BattleManager.instance.players[randomIndex].character == null || targets[randomIndex].character.health <= 0 && iterationLimit > 0)
        {
            randomIndex = Random.Range(0, targets.Count);
            iterationLimit--;
        }
        BattleManager.instance.targets.Add(targets[randomIndex]);
        foreach (CharacterHolder target in targets)
        {
            if (target != null)
            {

                if (target.isTaunting)
                {
                    BattleManager.instance.targets[0] = target;
                }
            }
        }

        int randomPrecision = Random.Range(1, 11);
        if (randomPrecision > user.precisionModifier) {
            Debug.Log("Attack missed");
        }
        else
        {
            BattleManager.instance.targets[0].TakeDamage(user.attack);
        }

    }
}
