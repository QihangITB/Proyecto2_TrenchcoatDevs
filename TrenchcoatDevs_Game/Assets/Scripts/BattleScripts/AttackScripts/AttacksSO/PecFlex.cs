using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PecFlex", menuName = "Attacks/PecFlex")]
public class PecFlex : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle())
        {
            targetButtons.Clear();
            //pon el boton de user de battlemanager como boton en buttontargetlist
            targetButtons = new List<GameObject>
            {
                OnlineBattleManager.instance.user.gameObject
            };
            foreach (GameObject button in targetButtons)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            targetButtons.Clear();
            //pon el boton de user de battlemanager como boton en buttontargetlist
            targetButtons = new List<GameObject>
            {
                BattleManager.instance.user.gameObject
            };
            foreach (GameObject button in targetButtons)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
        
    }

    public override void Effect(List<CharacterHolder> target, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            PerformFinishTurn();
        }
        else
        {
            user.UseStamina(cost);
            if (user.defense>user.character.defense+2)
            {
                Debug.Log("Defense already increased");
            }
            else
            {
                user.defense += 2;
            }
            user.Taunt();
            PerformFinishTurn();
        }

    }
}
