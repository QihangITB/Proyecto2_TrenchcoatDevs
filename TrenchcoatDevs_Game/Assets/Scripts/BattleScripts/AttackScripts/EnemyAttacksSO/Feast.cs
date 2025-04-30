using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feast", menuName = "EnemyAttacks/Feast")]
public class Feast : GenericAttack
{
    int iterationLimit;
    public override void ActivateTargetButtons()
    {
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
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
            foreach (CharacterHolder onion in BattleManager.instance.enemies)
            {
                if (onion != null)
                {
                    //comprueba si el enemigo es BroColi
                    if (onion.character.characterName == "Onion")
                    {
                        BattleManager.instance.targets.Add(onion);
                        user.Heal(5, false);
                    }
                }
            }
            foreach (CharacterHolder target in BattleManager.instance.targets)
            {
                if (target.character.characterName == "Onion")
                {
                    target.TakeDamage(20);
                }
                else
                {
                    target.TakeDamage(user.attack);
                }
            }
        }

    }
}
