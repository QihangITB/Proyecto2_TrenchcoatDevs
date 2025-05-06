using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSpriteInBattle : MonoBehaviour
{
    public CharacterHolder character;
    // Update is called once per frame
    private void OnEnable()
    {
        StartCoroutine(SelectSprite());
    }
    void Update()
    {
        if (character.HP <= 0)
        {
            GetComponent<RawImage>().texture = BattleManager.instance.cameras[BattleManager.instance.cameras.Count - 1].targetTexture;
        }
    }
    public IEnumerator SelectSprite()
    {
        yield return new WaitForSeconds(0.00001f);

        if (character.character != null)
        {
            switch (character.character)
            {
                case GrandmaCharacter grandma:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[0].targetTexture;
                    break;
                case AddictCharacter addicted:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[1].targetTexture;
                    break;
                case PiromaniacCharacter pyro:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[2].targetTexture;
                    break;
                case InternCharacter intern:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[3].targetTexture;
                    break;
                case GraffitiPainterCharacter streetartist:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[4].targetTexture;
                    break;
                case CulturistCharacter bodybuilder:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[5].targetTexture;
                    break;
                case PrincessEnemy princess:
                    GetComponent<RawImage>().texture = BattleManager.instance.cameras[6].targetTexture;
                    break;
            }
            if (character.character.GetType().Name == "PrincessEnemy")
            {
                GetComponent<RawImage>().transform.localScale = Vector3.one * 2;
            }
            else
            {
                GetComponent<RawImage>().transform.localScale = Vector3.one;
            }
        }
        else
        {
            GetComponent<RawImage>().texture = BattleManager.instance.cameras[BattleManager.instance.cameras.Count - 1].targetTexture;
        }
    }
}
