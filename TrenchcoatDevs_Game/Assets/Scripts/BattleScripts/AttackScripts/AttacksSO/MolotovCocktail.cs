using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MolotovCocktail", menuName = "Attacks/MolotovCocktail")]
public class MolotovCocktail : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        targetButtons = BattleManager.instance.enemyButtons;
        foreach (GameObject button in targetButtons)
        {
            if (button.GetComponent<CharacterHolder>().character != null)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        foreach (CharacterHolder target in targets)
        {
            target.TakeDamage(user.attack * 2);
            target.GetBurnt();
        }
        BattleManager.instance.FinishTurn();
    }
}
