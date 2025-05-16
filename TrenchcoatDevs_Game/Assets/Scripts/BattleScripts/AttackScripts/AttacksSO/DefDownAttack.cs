using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DefDownAttack", menuName = "Attacks/DefDownAttack")]
public class DefDownAttack : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle())
        {
            foreach (GameObject button in OnlineBattleManager.instance.enemyButtons)
            {
                if (button.GetComponent<CharacterHolder>().character != null)
                {
                    button.GetComponent<Image>().enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject button in BattleManager.instance.enemyButtons)
            {
                if (button.GetComponent<CharacterHolder>().character != null)
                {
                    button.GetComponent<Image>().enabled = true;
                }
            }
        }
        
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        foreach (CharacterHolder target in targets)
        {
            if (target.defense - 1 >= 0)
            {
                target.defense -= 1;
                Debug.Log(target.character.characterName + " had its defense lowered");
            }
            else
            {
                target.defense = 0;
                Debug.Log(target.character.characterName + " can't get its defense lower");
            }
            target.TakeDamage(user.attack);
        }
        PerformFinishTurn();
    }
}
