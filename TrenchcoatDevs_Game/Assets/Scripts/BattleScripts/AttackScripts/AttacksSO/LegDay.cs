using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LegDay", menuName = "Attacks/LegDay")]
public class LegDay : GenericAttack
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
            user.speed += 2;
            PerformFinishTurn();
        }

    }
}
