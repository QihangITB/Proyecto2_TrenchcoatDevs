using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PoisonCloud", menuName = "Attacks/PoisonCloud")]
public class PoisonCloud : GenericAreaAttack
{
    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        Debug.Log("Entra");
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            return;
        }
        else
        {
            Debug.Log("A");
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                if (target != null)
                {
                    target.GetPoisoned();
                }
            }
            BattleManager.instance.FinishTurn();
        }
    }
    public override void ActivateTargetButtons()
    {
        Debug.Log("Ola");
        targetButtons[0] = BattleManager.instance.allCharactersButton;
        targetButtons[0].GetComponent<Image>().enabled = true;
    }
}
