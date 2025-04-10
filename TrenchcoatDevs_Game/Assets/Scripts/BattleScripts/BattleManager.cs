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
    public GameObject enemyTeamButton;
    public GameObject basicAttackButton;
    public GenericAttack attack;
    public GenericAreaAttack areaAttack;
    public AAttack enemyAttack;
    public CharacterHolder user;
    public AEnemy enemyUser;
    public List<CharacterHolder> targets = new List<CharacterHolder>();
    public bool fightIsFinished = false;
    public int poisonDamageDivisor = 10;
    public bool canMove = true;

    public void CharacterAllocation(List<APlayer> listOfPlayers, List<AEnemy> listOfenemies)
    {
        for (int i = 0; i < listOfPlayers.Count; i++)
        {
            Debug.Log("Player " + i + " is " + players[i].character);
            players[i].character = listOfPlayers[i];
            players[i].SelectCharacter();
        }
        for (int i = 0; i < listOfenemies.Count; i++)
        {
            Debug.Log("Enemy " + i + " is " + enemies[i].character);
            enemies[i].character = listOfenemies[i];
            enemies[i].SelectCharacter();
        }
        StartRound();

    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        PlayerManager.instance.AllocateCharacters();
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
                if (!characters.Contains(character) && character.HP > 0 && character.character != null)
                {
                    characters.Add(character);
                }
            }
            foreach (CharacterHolder character in players)
            {
                if (!characters.Contains(character) && character.HP > 0 && character.character != null)
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
            canMove = true;
            CheckConditions(character);
            //vacia targets
            targets.Clear();
            if (character.HP <= 0 || !canMove)
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
                    StartCoroutine(WaitForTurn());
                }
                else
                {
                    //haz que basicAttackButton cambie la funcion de ataque al 
                    basicAttackButton.GetComponent<SelectAttack>().attack = character.character.basicAttack;
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
    public IEnumerator WaitForTurn()
    {
        yield return new WaitForSeconds(1.5f);
        FinishTurn();

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
        if (character.isPoisoned)
        {
            Debug.Log(character.character + " is poisoned");
            character.TakeDamage(character.maxHP / poisonDamageDivisor);
        }
        if (character.isDisgusted)
        {
            int random = Random.Range(0, 10);
            if (random < 3)
            {
                canMove = false;
                Debug.Log(character.character + " is disgusted and can't attack");
            }
        }
        if (character.isRegenerating)
        {
            character.Heal(character.maxHP / 20, false);
            Debug.Log(character.character + " is regenerating and healed " + character.maxHP / 10 + " HP");
        }
        if (character.isRested)
        {
            character.Rest(character.maxStamina/20);
            Debug.Log(character.character + " is rested and recovered " + character.maxStamina / 20 + " stamina");
        }
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
        DeActivateTargetButtons();
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
        enemyTeamButton.GetComponent<Image>().enabled = false;
    }
}
