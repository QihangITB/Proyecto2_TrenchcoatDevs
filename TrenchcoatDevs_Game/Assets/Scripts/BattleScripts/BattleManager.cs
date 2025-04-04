using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<CharacterHolder> enemies = new List<CharacterHolder>();
    public List<CharacterHolder> players = new List<CharacterHolder>();
    public List<CharacterHolder> characters = new List<CharacterHolder>();
    public List<CharacterHolder> CharOrderInTurn = new List<CharacterHolder>();
    public List<GameObject> playerButtons = new List<GameObject>();
    public List<GameObject> enemyButtons = new List<GameObject>();
    public GenericAttack attack;
    public GenericAreaAttack areaAttack;
    public AAttack enemyAttack;
    public CharacterHolder user;
    public AEnemy enemyUser;
    public List<CharacterHolder> targets = new List<CharacterHolder>();
    public bool fightIsFinished = false;

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
        StartRound();
        playerButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerButton"));
        enemyButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyButton"));
    }
    public void StartRound()
    {
        if (fightIsFinished)
        {
            StartTurn(null);
        }
        else
        {
            CharOrderInTurn.Clear();
            foreach (CharacterHolder character in enemies)
            {
                if (!characters.Contains(character) && character.HP > 0)
                {
                    characters.Add(character);
                }
            }
            foreach (CharacterHolder character in players)
            {
                if (!characters.Contains(character) && character.HP > 0)
                {
                    characters.Add(character);
                }
            }
            foreach (CharacterHolder character in characters)
            {
                CharOrderInTurn.Add(character);
            }
            OrderCharacters();
        }
        
    }
    public void StartTurn(CharacterHolder character)
    {
        if (!fightIsFinished)
        {
            user = character;
            CheckConditions(character);
            if (character.HP <= 0)
            {
                FinishTurn();
            }
            else
            {
                if (enemies.Contains(character))
                {
                    Debug.Log(character.character);
                    enemyUser = user.character as AEnemy;
                    enemyUser.SelectAttack();
                    FinishTurn();
                }
                else
                {
                    Debug.Log(character + " elige un ataque");
                }
            }
        }
        else
        {
            if (enemies.Count == 0)
            {
                Debug.Log("You win");
            }
            else
            {
                Debug.Log("You lose");
            }
        }
    }
    public void FinishTurn()
    {
        user = null;
        CharOrderInTurn.RemoveAt(0);
        if (CharOrderInTurn.Count > 0)
        {
            StartTurn(CharOrderInTurn[0]);
        }
        else
        {
            StartRound();
        }
    }
    private void CheckConditions(CharacterHolder character)
    {
        //Ahora no hace nada
    }
    //ordena characters por speed
    public void OrderCharacters()
    {
        CharOrderInTurn.Sort((x, y) => y.speed.CompareTo(x.speed));
        Debug.Log(CharOrderInTurn[0].character.name + " is first in turn");
        StartTurn(CharOrderInTurn[0]);
    }

    public void UseAttack()
    {
 
        attack.Effect(targets, user);
        DeActivateTargetButtons();
    }
    public void UseAreaAttack()
    {
        areaAttack.Effect(targets, user);
    }
    public void DeActivateTargetButtons()
    {
        foreach (GameObject button in playerButtons)
        {
            button.GetComponent<Image>().enabled = false;
        }
        foreach (GameObject button in enemyButtons)
        {
            button.GetComponent<Image>().enabled = false;
        }
    }
}
