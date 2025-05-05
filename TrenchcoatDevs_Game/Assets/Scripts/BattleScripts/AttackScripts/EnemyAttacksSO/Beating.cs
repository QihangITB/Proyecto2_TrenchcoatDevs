using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatingEnemy", menuName = "EnemyAttacks/BeatingEnemy")]
public class Beating : GenericAttack
{
    int iterationLimit;
    int broColiCount = 0;
    public override void ActivateTargetButtons()
    {
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        broColiCount = 0;
        int randomIndex = Random.Range(0, BattleManager.instance.players.Count);
        iterationLimit = 500; //si no se pone unity lo cuenta como bucle infinito
        while (BattleManager.instance.players[randomIndex] == null || targets[randomIndex].character.health <= 0 && iterationLimit > 0)
        {
            targets.RemoveAt(randomIndex);
            randomIndex = Random.Range(0, targets.Count);
            iterationLimit--;
        }
        BattleManager.instance.targets.Add(targets[randomIndex]);
        foreach (CharacterHolder target in targets)
        {
            if (target.isTaunting)
            {
                BattleManager.instance.targets[0] = target;
            }
        }

        int randomPrecision = Random.Range(1, 11);
        if (randomPrecision > user.precisionModifier)
        {
            Debug.Log("Attack missed");
        }
        else
        {
            foreach (CharacterHolder broColi in BattleManager.instance.enemies)
            {
                if (broColi != null)
                {
                    //comprueba si el enemigo es BroColi
                    if (broColi.character.characterName == "BroColi")
                    {
                        broColiCount++;
                    }
                }
                
            }
            BattleManager.instance.targets[0].TakeDamage(user.attack*broColiCount);
        }

    }
}
