using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSpriteInBattle : MonoBehaviour
{
    public CharacterHolder character;
    public GameObject spriteReference;
    // Update is called once per frame
    private void OnEnable()
    {
        StartCoroutine(SelectSprite());
    }
    void Update()
    {
        if (character.HP <= 0)
        {
            if (OnlineBattleManager.instance != null)
            {
                GetComponent<RawImage>().texture = OnlineBattleManager.instance.cameras[OnlineBattleManager.instance.cameras.Count - 1].targetTexture;
            }
            else
            {
                GetComponent<RawImage>().texture = BattleManager.instance.cameras[BattleManager.instance.cameras.Count - 1].targetTexture;
            }
            
        }
    }
    public void UpdateSprite()
    {
        StartCoroutine(SelectSprite());
    }
    public IEnumerator SelectSprite()
    {
        yield return new WaitForSeconds(0.00001f);
        GetComponent<RawImage>().enabled = true;
        Camera thisCam = null;

        if (character.character != null) 
        {
            if (OnlineBattleManager.instance != null)
            {
                switch (character.character)
                {
                    case GrandmaCharacter grandma:
                        thisCam = OnlineBattleManager.instance.cameras[0];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case AddictCharacter addicted:
                        thisCam = OnlineBattleManager.instance.cameras[1];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case PiromaniacCharacter pyro:
                        thisCam = OnlineBattleManager.instance.cameras[2];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case InternCharacter intern:
                        thisCam = OnlineBattleManager.instance.cameras[3];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case GraffitiPainterCharacter streetartist:
                        thisCam = OnlineBattleManager.instance.cameras[4];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case CulturistCharacter bodybuilder:
                        thisCam = OnlineBattleManager.instance.cameras[5];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case PrincessEnemy princess:
                        thisCam = OnlineBattleManager.instance.cameras[6];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    default:
                        GetComponent<RawImage>().texture = OnlineBattleManager.instance.cameras[OnlineBattleManager.instance.cameras.Count - 1].targetTexture;
                        break;
                }
            }
            else
            {
                switch (character.character)
                {
                    case GrandmaCharacter grandma:
                        thisCam = BattleManager.instance.cameras[0];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case AddictCharacter addicted:
                        thisCam = BattleManager.instance.cameras[1];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case PiromaniacCharacter pyro:
                        thisCam = BattleManager.instance.cameras[2];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case InternCharacter intern:
                        thisCam = BattleManager.instance.cameras[3];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case GraffitiPainterCharacter streetartist:
                        thisCam = BattleManager.instance.cameras[4];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case CulturistCharacter bodybuilder:
                        thisCam = BattleManager.instance.cameras[5];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case PrincessEnemy princess:
                        thisCam = BattleManager.instance.cameras[6];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        break;
                    case OnionEnemy onion:
                        thisCam = BattleManager.instance.onionCams[0];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        BattleManager.instance.onionCams.Remove(thisCam);
                        BattleManager.instance.onionCams.Add(thisCam);
                        break;
                    case BroColiEnemy brocoli:
                        thisCam = BattleManager.instance.brocoliCams[0];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        BattleManager.instance.brocoliCams.Remove(thisCam);
                        BattleManager.instance.brocoliCams.Add(thisCam);
                        break;
                    case PGeon pgeon:
                        thisCam = BattleManager.instance.pgeonCams[0];
                        GetComponent<RawImage>().texture = thisCam.targetTexture;
                        spriteReference = thisCam.GetComponentInChildren<Animator>().gameObject;
                        BattleManager.instance.pgeonCams.Remove(thisCam);
                        BattleManager.instance.pgeonCams.Add(thisCam);
                        break;
                    default:
                        GetComponent<RawImage>().texture = BattleManager.instance.cameras[BattleManager.instance.cameras.Count - 1].targetTexture;
                        break;
                }
            }
            
            if (character.character.GetType().Name == "PrincessEnemy")
            {
                GetComponent<RawImage>().transform.localScale = Vector3.one * 2;
            }
            else
            {
                GetComponent<RawImage>().transform.localScale = Vector3.one * 1.2f;
            }
        }
        else
        {
            if (OnlineBattleManager.instance != null)
            {
                GetComponent<RawImage>().texture = OnlineBattleManager.instance.cameras[OnlineBattleManager.instance.cameras.Count - 1].targetTexture;
            }
            else
            {
                GetComponent<RawImage>().texture = BattleManager.instance.cameras[BattleManager.instance.cameras.Count - 1].targetTexture;
            }
            
        }
    }
}
