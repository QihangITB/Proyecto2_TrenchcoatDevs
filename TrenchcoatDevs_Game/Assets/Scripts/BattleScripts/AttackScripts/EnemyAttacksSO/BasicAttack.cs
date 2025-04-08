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
        //selecciona un objetivo aleatorio de la lista de players de battlemanager
        int randomIndex = Random.Range(0, BattleManager.instance.players.Count);
        iterationLimit = 20;
        while (targets[randomIndex].character.health <= 0 && iterationLimit>0)
        {
            randomIndex = Random.Range(0, BattleManager.instance.players.Count);
            iterationLimit--;
        }
        BattleManager.instance.targets.Add(targets[randomIndex]);
        // Apply damage to the target
        BattleManager.instance.targets[0].TakeDamage(user.attack);
    }
}
