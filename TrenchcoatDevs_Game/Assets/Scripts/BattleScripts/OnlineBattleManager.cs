using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OnlineBattleManager : MonoBehaviour
{
    public static OnlineBattleManager instance;
    public int currentRound = 0;
    public List<CharacterHolder> enemies = new List<CharacterHolder>();
    public List<CharacterHolder> players = new List<CharacterHolder>();
    public List<CharacterHolder> characters = new List<CharacterHolder>();
    public List<CharacterHolder> CharOrderInTurn = new List<CharacterHolder>();
    public List<CharacterHolder> enemySelectors = new List<CharacterHolder>();
    public List<GameObject> playerButtons = new List<GameObject>();
    public List<GameObject> enemyButtons = new List<GameObject>();
    public GameObject enemyTeamButton;
    public GameObject playerTeamButton;
    public GameObject allCharactersButton;
    public GameObject basicAttackButton;
    public GameObject restButton;
    public List<GameObject> abilityButtons = new List<GameObject>();
    public GameObject hpBar;
    public GameObject staminaBar;
    public GenericAttack attack;
    public GenericAreaAttack areaAttack;
    public AAttack enemyAttack;
    public CharacterHolder user;
    public AEnemy enemyUser;
    public List<CharacterHolder> targets = new List<CharacterHolder>();
    public bool fightIsFinished = false;
    public bool win = false;
    public int poisonDamageDivisor = 5;
    public bool canMove = true;
    public NodeAccess nodeAccess;

    public List<Camera> cameras = new List<Camera>();

    public GameObject player;

    private void SetLocalPlayer(List<APlayer> listOfPlayers, List<CharacterOutOfBattle> listOfOutOfBattle)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i < listOfPlayers.Count)
            {
                players[i].character = listOfPlayers[i];
                Debug.Log("Player " + i + " is " + players[i].character);
                if (listOfOutOfBattle[i] != null)
                {
                    players[i].SelectCharacter(listOfOutOfBattle[i]);
                }
                else
                {
                    players[i].SelectCharacter(null);
                }
            }
            else
            {
                //desactiva el componente imagen del objeto padre
                foreach (Image image in players[i].gameObject.GetComponentsInParent<Image>())
                {
                    image.enabled = false;
                }
                //desactiva el slider 
                players[i].HpBar.GetComponent<Slider>().gameObject.SetActive(false);
                players[i].StaminaBar.GetComponent<Slider>().gameObject.SetActive(false);
                players[i] = null;
            }
        }
    }
    private void SetEnemies(List<APlayer> listOfPlayers, List<CharacterOutOfBattle> listOfOutOfBattle)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i < listOfPlayers.Count)
            {
                enemies[i].character = listOfPlayers[i];
                Debug.Log("Player " + i + " is " + players[i].character);
                if (listOfOutOfBattle[i] != null)
                {
                    enemies[i].SelectCharacter(listOfOutOfBattle[i]);
                }
                else
                {
                    enemies[i].SelectCharacter(null);
                }
            }
            else
            {
                //desactiva el componente imagen del objeto padre
                foreach (Image image in players[i].gameObject.GetComponentsInParent<Image>())
                {
                    image.enabled = false;
                }
                //desactiva el slider 
                enemies[i].HpBar.GetComponent<Slider>().gameObject.SetActive(false);
                enemies[i].StaminaBar.GetComponent<Slider>().gameObject.SetActive(false);
                enemies[i] = null;
            }
        }
    }
    public void CharacterAllocation(List<APlayer> listOfPlayers, List<APlayer> listOfenemies, List<CharacterOutOfBattle> listOfOutOfBattle,List<CharacterOutOfBattle> enemieOutOfBattle)
    {
        List<CharacterHolder> characterHolders;
        if (NetworkManager.Singleton.IsHost) 
        {
            SetLocalPlayer(listOfPlayers, listOfOutOfBattle);
            SetEnemies(listOfenemies, enemieOutOfBattle);
            characterHolders = new List<CharacterHolder>();
            characterHolders.AddRange(players);
            characterHolders.AddRange(enemies);
        }
        else
        {
            SetEnemies(listOfenemies,enemieOutOfBattle);
            SetLocalPlayer(listOfPlayers, listOfOutOfBattle);
            characterHolders = new List<CharacterHolder>();
            characterHolders.AddRange(enemies);
            characterHolders.AddRange(players);
        }
        BattleManagerActionSyncer.Instance.CharacterHolders = characterHolders;
        characters = characterHolders;
        characters.RemoveAll(obj => obj.character == null);
        //por cada pasiva de los personajes de la lista de jugadores, esta se activa
        foreach (CharacterHolder character in players)
        {
            if (character != null)
            {
                //busca las pasivas del personaj de list out of battle que tenga el mismo character
                foreach (CharacterOutOfBattle characterOutOfBattle in listOfOutOfBattle)
                {
                    //David said it was to solve a problem he does not remember.
                    if (character.character == characterOutOfBattle.character)
                    {
                        foreach (APassive passive in characterOutOfBattle.knownPassives)
                        {
                            passive.ActivatePassive(character);
                        }
                    }
                }
            }
        }
        foreach (CharacterHolder character in enemies)
        {
            if (character != null)
            {
                //busca las pasivas del personaj de list out of battle que tenga el mismo character
                foreach (CharacterOutOfBattle characterOutOfBattle in enemieOutOfBattle)
                {
                    //David said it was to solve a problem he does not remember.
                    if (character.character == characterOutOfBattle.character)
                    {
                        foreach (APassive passive in characterOutOfBattle.knownPassives)
                        {
                            passive.ActivatePassive(character);
                        }
                    }
                }
            }
        }
        /*foreach(CharacterHolder character in enemies)
        {
            if (character != null)
            {
                //busca las pasivas del personaj de list out of battle que tenga el mismo character
                foreach (CharacterOutOfBattle characterOutOfBattle in listOfOutOfBattle)
                {
                    foreach (APassive passive in characterOutOfBattle.knownPassives)
                    {
                        passive.ActivatePassive(character);
                    }
                }
            }
        }*/
        StartRound();
    }
    private void Start()
    {
        UnityEngine.Random.InitState(SeedSyncManager.SeedSyncer.GetSyncedSeed());
        currentRound = 0;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        playerButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerButton"));
        enemyButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyButton"));
    }
    public void StartRound()
    {
        currentRound++;
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
            foreach (CharacterHolder characterInTurn in characters)
            {
                if (characterInTurn != null)
                {
                    characterInTurn.characterTurnIndicator.SetActive(false);
                }
            }
            character.characterTurnIndicator.SetActive(true);
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
                hpBar.SetActive(false);
                staminaBar.SetActive(false);
                foreach (GameObject button in abilityButtons)
                {
                    button.GetComponent<Image>().enabled = false;
                    button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                }
                if (enemies.Contains(character))
                {

                }
                else
                {
                    staminaBar.SetActive(true);
                    hpBar.SetActive(true);
                    hpBar.GetComponentInChildren<TextMeshProUGUI>().text = character.HP.ToString() + "HP / " + character.maxHP.ToString()+"HP";
                    staminaBar.GetComponentInChildren<TextMeshProUGUI>().text = character.stamina.ToString() + "St / " + character.maxStamina.ToString()+"St";
                    hpBar.GetComponentInChildren<Slider>().value = (float)character.HP / (float)character.maxHP;
                    staminaBar.GetComponentInChildren<Slider>().value = (float)character.stamina / (float)character.maxStamina;
                    basicAttackButton.GetComponent<Image>().enabled = true;
                    basicAttackButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                    restButton.GetComponent<Image>().enabled = true;
                    restButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                    for (int i = 0; i < character.characterOutOfBattle.knownAttacks.Count; i++)
                    {
                        abilityButtons[i].GetComponent<Image>().enabled = true;
                        abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                        abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = character.characterOutOfBattle.knownAttacks[i].attackName;
                        AssignAbilityToButton(abilityButtons[i], character.characterOutOfBattle.knownAttacks[i]);
                    }
                    basicAttackButton.GetComponent<SelectAttack>().attack = character.characterOutOfBattle.basicAttack;
                    Debug.Log(character.character + " elige un ataque");
                }
            }
        }
        else
        {
            if (win)
            {
                Debug.Log("You win");
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i] != null)
                    {
                        if (players[i].HP > players[i].maxHP)
                        {
                            players[i].HP = players[i].maxHP;
                        }
                        PlayerManager.instance.charactersOutOfBattle[i].characterHP = players[i].HP;
                    }
                }
            }
            else
            {
                Debug.Log("You lose");
            }
            basicAttackButton.GetComponent<Image>().enabled = false;
            basicAttackButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            restButton.GetComponent<Image>().enabled = false;
            restButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            foreach (GameObject button in abilityButtons)
            {
                button.GetComponent<Image>().enabled = false;
                button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
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
            BattleManagerActionSyncer.Instance.StartRoundOf(CharOrderInTurn[0]);
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
            if (character.characterOutOfBattle != null)
            {
                if (character.characterOutOfBattle.characterPoisonModifier == 0)
                {
                    character.Heal(character.maxHP / poisonDamageDivisor, false);
                }
                else
                {
                    character.TakeDamage(character.maxHP / (poisonDamageDivisor - character.characterOutOfBattle.characterPoisonModifier));
                }
            }
            else
            {
                character.TakeDamage(character.maxHP / poisonDamageDivisor);
            }
        }
        if (character.isDisgusted)
        {
            int random = UnityEngine.Random.Range(0, 10);
            if (random < 3)
            {
                canMove = false;
                Debug.Log(character.character + " is disgusted and can't attack");
            }
        }
        if (character.isRegenerating)
        {
            character.Heal(character.maxHP / 5, false);
            Debug.Log(character.character + " is regenerating");
        }
        if (character.isRested)
        {
            character.Rest(character.maxStamina/20);
            Debug.Log(character.character + " is rested");
        }
        if (character.isTaunting)
        {
            character.isTaunting = false;
            character.tauntIcon.SetActive(false);
        }
    }
    
    //ordena characters por speed
    public void OrderCharacters()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            CharOrderInTurn.Sort((x, y) => y.speed.CompareTo(x.speed));
            Debug.Log(CharOrderInTurn[0].character.name + " is first in turn");
            BattleManagerActionSyncer.Instance.StartRoundOf(CharOrderInTurn[0]);
        }
       
    }

    public void UseAttack()
    {
        BattleManagerActionSyncer.Instance.UseSingleAttack(attack,targets,user);
    }
    public void PerformAttack()
    {
        DeActivateTargetButtons();
        attack.Effect(targets, user);
    }
    public void UseAreaAttack()
    {
        BattleManagerActionSyncer.Instance.UseAreaAttack(areaAttack,targets,user);
    }
    public void PerformAreaAttack()
    {
        DeActivateTargetButtons();
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
        enemyTeamButton.GetComponent<Image>().enabled = false;
        playerTeamButton.GetComponent<Image>().enabled = false;
        allCharactersButton.GetComponent<Image>().enabled = false;
    }
}
