using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<ACharacter> enemies = new List<ACharacter>();
    public List<APlayer> players = new List<APlayer>();
    public List<ACharacter> characters = new List<ACharacter>();
    public List<ACharacter> CharOrderInTurn = new List<ACharacter>();
    public GenericAttack attack;
    public GenericAreaAttack areaAttack;
    public CharacterHolder target;
    public CharacterHolder user;
    public List<CharacterHolder> targets = new List<CharacterHolder>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //ordena characters por speed
    public void OrderCharacters()
    {
        CharOrderInTurn.Sort((x, y) => y.speed.CompareTo(x.speed));
    }

    public void UseAttack()
    {
        attack.Effect(target, user);
        DeActivateTargetButtons();
    }
    public void UseAreaAttack()
    {
        areaAttack.Effect(targets, user);
    }
    public void DeActivateTargetButtons()
    {
        foreach (GameObject button in attack.targetButtons)
        {
            button.GetComponent<Image>().enabled = false;
        }
    }
}
