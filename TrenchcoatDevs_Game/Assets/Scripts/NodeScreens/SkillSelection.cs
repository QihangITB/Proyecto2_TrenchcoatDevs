using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    public static SkillSelection Instance;

    [Header("First ability")]
    public TMP_Text nameOne;
    public TMP_Text descriptionOne;

    [Header("Second ability")]
    public TMP_Text nameTwo;
    public TMP_Text descriptionTwo;

    [Header("Third ability")]
    public TMP_Text nameThree;
    public TMP_Text descriptionThree;

    public NodeAccess nodeAccess;
    public CharacterOutOfBattle characterOutOfBattle;
    public List<APassive> Passive = new List<APassive>();
    public List<GameObject> PassiveObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void showAbilities()
    {
        int iterationLimit = 0;
        Passive.Clear();
        foreach (APassive passive in characterOutOfBattle.character.knowablePassives)
        {
            Passive.Add(passive);
        }
        //selecciona una habilidad aleatoria para cada uno de los slots
        int randomOne = Random.Range(0, Passive.Count);
        int randomTwo = Random.Range(0, Passive.Count);
        int randomThree = Random.Range(0, Passive.Count);

        while ((characterOutOfBattle.knownPassives.Contains(Passive[randomOne])) && iterationLimit <100)
        {
            randomOne = Random.Range(0, Passive.Count);
        }

        while ((randomOne == randomTwo || characterOutOfBattle.knownPassives.Contains(Passive[randomTwo])) && iterationLimit < 100)
        {
            randomTwo = Random.Range(0, Passive.Count);
        }
        while ((randomOne == randomThree || randomTwo == randomThree || characterOutOfBattle.knownPassives.Contains(Passive[randomThree])) && iterationLimit < 100)
        {
            randomThree = Random.Range(0, Passive.Count);
        }

        nameOne.text = Passive[randomOne].passiveName;
        descriptionOne.text = Passive[randomOne].passiveDescription;
        PassiveObjects[0].GetComponent<PassiveHolder>().passive = Passive[randomOne];
        nameTwo.text = Passive[randomTwo].passiveName;
        descriptionTwo.text = Passive[randomTwo].passiveDescription;
        PassiveObjects[1].GetComponent<PassiveHolder>().passive = Passive[randomTwo];
        nameThree.text = Passive[randomThree].passiveName;
        descriptionThree.text = Passive[randomThree].passiveDescription;
        PassiveObjects[2].GetComponent<PassiveHolder>().passive = Passive[randomThree];
    }
}
