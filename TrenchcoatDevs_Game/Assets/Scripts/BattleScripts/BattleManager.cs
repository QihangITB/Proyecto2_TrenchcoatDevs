using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
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

    public List<Camera> onionCams;
    public List<Camera> brocoliCams;
    public List<Camera> pgeonCams;

    public GameObject player;

    public void CharacterAllocation(List<APlayer> listOfPlayers, List<AEnemy> listOfenemies, List<CharacterOutOfBattle> listOfOutOfBattle)
    {
        //desactiva la descripcion de la habilidad
        basicAttackButton.GetComponent<SelectTypeOfAttack>().description.transform.parent.gameObject.SetActive(false);
        win = false;
        fightIsFinished = false;
        for (int i = 0; i < players.Count; i++)
        {
            if (i < listOfPlayers.Count)
            {
                players[i].character = listOfPlayers[i];
                Debug.Log("Player " + i + " is " + players[i].character);
                players[i].HpBar.GetComponent<Slider>().gameObject.SetActive(true);
                players[i].StaminaBar.GetComponent<Slider>().gameObject.SetActive(true);
                /*for (int j = 0; i < listOfOutOfBattle.Count; j++)
                {
                    if (players[i].characterOutOfBattle != null && players[0].characterOutOfBattle!= listOfOutOfBattle[j] && players[1].characterOutOfBattle != listOfOutOfBattle[j])
                    {
                        players[i].SelectCharacter(listOfOutOfBattle[j]);
                        j = 3;
                    }
                    else
                    {
                        players[i].SelectCharacter(null);
                    }
                }*/
                for (int j = 2; j>= 0; j--)
                {
                    Debug.Log(j);
                    if (listOfOutOfBattle[j] != null)
                    {
                        if (listOfOutOfBattle[j].character != null)
                        {
                            if (players[0].characterOutOfBattle != listOfOutOfBattle[j] && players[1].characterOutOfBattle != listOfOutOfBattle[j])
                            {
                                players[i].SelectCharacter(listOfOutOfBattle[j]);
                            }
                        }
                    }
                    else
                    {
                        players[i].SelectCharacter(null);
                    }
                }
                
            }
            else
            {
                //desactiva el componente imagen del objeto padre
                /*foreach (Image image in players[i].gameObject.GetComponentsInParent<Image>())
                {
                    image.enabled = false;
                }*/
                //desactiva el slider 
                players[i].HpBar.GetComponent<Slider>().gameObject.SetActive(false);
                players[i].StaminaBar.GetComponent<Slider>().gameObject.SetActive(false);
                players[i].character = null;
                if (players[i].characterOutOfBattle != null)
                {
                    players[i].characterOutOfBattle.character = null;

                }
            }
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if (i < listOfenemies.Count && listOfenemies[i]!=null)
            {
                enemies[i].character = listOfenemies[i];
                Debug.Log("enemy chosen is " + listOfenemies[i].characterName);
                Debug.Log("Enemy " + i + " is " + enemies[i].character);
                enemies[i].SelectCharacter(null);

                //aqui hay que tocar cosas

                /*for (int j = 0; j < enemies[i].gameObject.GetComponentsInParent<Image>().Count(); j++)
                {
                    if (j == 1)
                    {
                        enemies[i].gameObject.GetComponentsInParent<Image>()[j].enabled = true;
                    }
                }*/

                enemies[i].gameObject.transform.parent.GetComponentInChildren<RawImage>().enabled = true;

                //desactiva el slider 
                enemies[i].HpBar.GetComponent<Slider>().gameObject.SetActive(true);
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
                enemySelectors.Add(enemies[i]);
                enemies[i].character=null;
            }
        }
        //por cada pasiva de los personajes de la lista de jugadores, esta se activa
        foreach (CharacterHolder character in players)
        {
            if (character != null)
            {
                //busca las pasivas del personaj de list out of battle que tenga el mismo character
                foreach (CharacterOutOfBattle characterOutOfBattle in listOfOutOfBattle)
                {
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
        player.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        StartRound();
    }

    private void Awake()
    {
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
        if (fightIsFinished)
        {
            GetComponent<AudioSource>().Stop();
            player.GetComponent<AudioSource>().Play();
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
                if (character.character != null)
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
        StartCoroutine(LittlePause(character));
    }

    IEnumerator LittlePause(CharacterHolder character)
    {
        yield return new WaitForSeconds(1.5f);
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
                    Debug.Log(character.character);
                    enemyUser = user.character as AEnemy;
                    enemyUser.SelectAttack();
                    AttackAnimation(user);
                    StartCoroutine(WaitForTurn(0));
                }
                else
                {
                    staminaBar.SetActive(true);
                    hpBar.SetActive(true);
                    hpBar.GetComponentInChildren<TextMeshProUGUI>().text = character.HP.ToString() + "HP / " + character.maxHP.ToString() + "HP";
                    staminaBar.GetComponentInChildren<TextMeshProUGUI>().text = character.stamina.ToString() + "St / " + character.maxStamina.ToString() + "St";
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
            basicAttackButton.GetComponent<Image>().enabled = false;
            basicAttackButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            restButton.GetComponent<Image>().enabled = false;
            restButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            foreach (GameObject button in abilityButtons)
            {
                button.GetComponent<Image>().enabled = false;
                button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
            if (win)
            {
                Debug.Log("You win");
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].character != null)
                    {
                        if (players[i].HP > players[i].maxHP)
                        {
                            players[i].HP = players[i].maxHP;
                        }
                        PlayerManager.instance.charactersOutOfBattle[i].characterHP = players[i].HP;
                        players[i].characterOutOfBattle.fightsToLevelUp--;
                        if (players[i].characterOutOfBattle.fightsToLevelUp == 0)
                        {
                            players[i].characterOutOfBattle.timesToLevelUp++;
                        }
                    }
                }
                foreach (CharacterHolder characterHolder in players)
                {
                    characterHolder.burnIcon.SetActive(false);
                    characterHolder.disgustIcon.SetActive(false);
                    characterHolder.poisonIcon.SetActive(false);
                    characterHolder.tauntIcon.SetActive(false);
                    characterHolder.regenerateIcon.SetActive(false);
                    characterHolder.restIcon.SetActive(false);
                    characterHolder.characterTurnIndicator.SetActive(false);
                    characterHolder.characterOutOfBattle = null;
                    characterHolder.character = null;
                }
                nodeAccess.OnExitButtonClick();
            }
            else
            {
                Debug.Log("You lose");
                PlayerPrefs.SetInt("gameresult", 0);
                //aqui va la derrota
            }
        }
    }
    public void CheckWin()
    {
        int charHPCount = 0;
        int enemyHPCount = 0;
        foreach (CharacterHolder character in players)
        {
            if (character.character != null)
            {
                if (character.HP > 0)
                {
                    charHPCount += character.HP;
                }
            }
        }
        foreach (CharacterHolder character in enemies)
        {
            if (character.character != null)
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
            character.Heal(character.maxHP / 8, false);
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
        CharOrderInTurn.Sort((x, y) => y.speed.CompareTo(x.speed));
        Debug.Log(CharOrderInTurn[0].character.name + " is first in turn");
        StartTurn(CharOrderInTurn[0]);
    }

    public void UseAttack()
    {
 
        DeActivateTargetButtons();
        AttackAnimation(user);
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
        attack.Effect(targets, user);

    }
    public void UseAreaAttack()
    {
        DeActivateTargetButtons();
        AttackAnimation(user);
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
        areaAttack.Effect(targets, user);
    }

    void AttackAnimation(CharacterHolder user)
    {
        Debug.Log(user.character.characterName + " animaci�n");
        GameObject activeAttacker = user.transform.parent.GetComponentInChildren<RawImage>().gameObject.GetComponent<SelectSpriteInBattle>().spriteReference;
        activeAttacker.GetComponent<Animator>().SetTrigger("Attack");
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

    public void OnEnable()
    {
        cameras.Add(GameObject.Find("GrandmaCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("AddictedCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("PyroCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("InternCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("StreetArtistCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("BodybuilderCam").GetComponent<Camera>());

        cameras.Add(GameObject.Find("PrincessCam").GetComponent<Camera>());

        cameras.Add(GameObject.Find("NullCam").GetComponent<Camera>());

        onionCams.Add(GameObject.Find("OnionCam").GetComponent<Camera>());
        onionCams.Add(GameObject.Find("OnionCam2").GetComponent<Camera>());
        onionCams.Add(GameObject.Find("OnionCam").GetComponent<Camera>());

        brocoliCams.Add(GameObject.Find("BrocoliCam").GetComponent<Camera>());
        brocoliCams.Add(GameObject.Find("BrocoliCam2").GetComponent<Camera>());
        brocoliCams.Add(GameObject.Find("BrocoliCam3").GetComponent<Camera>());

        pgeonCams.Add(GameObject.Find("PigeonCam").GetComponent<Camera>());
        pgeonCams.Add(GameObject.Find("PigeonCam2").GetComponent<Camera>());
        pgeonCams.Add(GameObject.Find("PigeonCam3").GetComponent<Camera>());
    }
}
