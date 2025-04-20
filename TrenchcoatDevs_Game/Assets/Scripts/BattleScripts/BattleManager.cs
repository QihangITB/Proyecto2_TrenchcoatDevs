using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject restButton;
    public List<GameObject> abilityButtons = new List<GameObject>();
    public GenericAttack attack;
    public GenericAreaAttack areaAttack;
    public AAttack enemyAttack;
    public CharacterHolder user;
    public AEnemy enemyUser;
    public List<CharacterHolder> targets = new List<CharacterHolder>();
    public bool fightIsFinished = false;
    public bool win = false;
    public int poisonDamageDivisor = 10;
    public bool canMove = true;

    public void CharacterAllocation(List<APlayer> listOfPlayers, List<AEnemy> listOfenemies)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i < listOfPlayers.Count)
            {
                players[i].character = listOfPlayers[i];
                Debug.Log("Player " + i + " is " + players[i].character);
                players[i].SelectCharacter();
            }
            else
            {
                players.RemoveAt(i);
            }
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if (i < listOfenemies.Count)
            {
                enemies[i].character = listOfenemies[i];
                Debug.Log("Enemy " + i + " is " + enemies[i].character);
                enemies[i].SelectCharacter();
            }
            else
            {
                //desactiva el componente imagen del objeto padre
                foreach (Image image in enemies[i].gameObject.GetComponentsInParent<Image>())
                {
                    image.enabled = false;
                }
                //desactiva el slider 
                enemies[i].HpBar.GetComponent<Slider>().gameObject.SetActive(false);
                enemies[i]=null;
            }
        }

        /*for (int i = 0; i < listOfPlayers.Count; i++)
        {
            players[i].character = listOfPlayers[i];
            Debug.Log("Player " + i + " is " + players[i].character);
            players[i].SelectCharacter();
        }
        for (int i = 0; i < listOfenemies.Count; i++)
        {
            enemies[i].character = listOfenemies[i];
            Debug.Log("Enemy " + i + " is " + enemies[i].character);
            enemies[i].SelectCharacter();
        }*/
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
                if (character != null)
                {
                    if (!characters.Contains(character) && character.HP > 0 && character.character != null)
                    {
                        characters.Add(character);
                    }
                }
                
            }
            foreach (CharacterHolder character in players)
            {
                if (character != null)
                {
                    if (!characters.Contains(character) && character.HP > 0 && character.character != null)
                    {
                        characters.Add(character);
                    }
                }
            }
            foreach (CharacterHolder character in characters)
            {
                if (character != null)
                {
                    CharOrderInTurn.Add(character);
                }
            }
            OrderCharacters();
        }
        
    }
    public void StartTurn(CharacterHolder character)
    {
        WaitForTurn(0.5f);
        if (!fightIsFinished)
        {
            user = character;
            canMove = true;
            CheckConditions(character);
            targets.Clear();
            if (character.HP <= 0 || !canMove)
            {
                FinishTurn();
            }
            else
            {
                basicAttackButton.GetComponent<Image>().enabled = false;
                basicAttackButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                restButton.GetComponent<Image>().enabled = false;
                restButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                foreach (GameObject button in abilityButtons)
                {
                    button.GetComponent<Image>().enabled = false;
                    button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                }
                if (enemies.Contains(character))
                {
                    Debug.Log(character.character);
                    enemyUser = user.character as AEnemy;
                    enemyUser.SelectAttack();
                    StartCoroutine(WaitForTurn(1.5f));
                }
                else
                {
                    basicAttackButton.GetComponent<Image>().enabled = true;
                    basicAttackButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                    restButton.GetComponent<Image>().enabled = true;
                    restButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                    for (int i = 0; i < character.character.attacks.Count; i++)
                    {
                        abilityButtons[i].GetComponent<Image>().enabled = true;
                        abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                        abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = character.character.attacks[i].attackName;
                        AssignAbilityToButton(abilityButtons[i], character.character.attacks[i]);
                    }
                    basicAttackButton.GetComponent<SelectAttack>().attack = character.character.basicAttack;
                    Debug.Log(character.character + " elige un ataque");
                }
            }
        }
        else
        {
            if (win)
            {
                Debug.Log("You win");
            }
            else
            {
                Debug.Log("You lose");
            }
        }
    }
    public void CheckWin()
    {
        int charHPCount = 0;
        int enemyHPCount = 0;
        foreach (CharacterHolder character in players)
        {
            if (character != null)
            {
                if (character.HP > 0)
                {
                    charHPCount += character.HP;
                }
            }
        }
        foreach (CharacterHolder character in enemies)
        {
            if (character != null)
            {
                if (character.HP > 0)
                {
                    enemyHPCount += character.HP;
                }
            }
        }
        if (charHPCount <= 0)
        {
            fightIsFinished = true;
            win = false;
        }
        else if (enemyHPCount <= 0)
        {
            fightIsFinished = true;
            win = true;
        }
    }
    public void AssignAbilityToButton(GameObject button, AAttack ability)
    {
        //comprueba si el ataque es un ataque de area o un ataque normal
        if (ability is GenericAttack)
        {
            button.GetComponent<SelectAttack>().attack = ability as GenericAttack;
            button.GetComponent<SelectTypeOfAttack>().isAreaAttack = false;
        }
        else if (ability is GenericAreaAttack)
        {
            button.GetComponent<SelectAreaAttack>().attack = ability as GenericAreaAttack;
            button.GetComponent<SelectTypeOfAttack>().isAreaAttack = true;
        }
    }
    public IEnumerator WaitForTurn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
