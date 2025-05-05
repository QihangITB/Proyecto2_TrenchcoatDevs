using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MolotovCocktail", menuName = "Attacks/MolotovCocktail")]
public class MolotovCocktail : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        foreach (GameObject button in BattleManager.instance.enemyButtons)
        {
            if (button.GetComponent<CharacterHolder>().character != null)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                target.TakeDamage(user.attack * 2);
                target.GetBurnt();
            }
            BattleManager.instance.FinishTurn();
        }

    }
}
