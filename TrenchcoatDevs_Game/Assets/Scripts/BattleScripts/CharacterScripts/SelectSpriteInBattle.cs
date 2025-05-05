using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSpriteInBattle : MonoBehaviour
{
    public CharacterHolder character;
    public List<Camera> cameras;
    // Update is called once per frame
    private void Awake()
    {
        cameras.Add(GameObject.Find("GrandmaCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("AddictedCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("PyroCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("InternCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("StreetArtistCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("BodybuilderCam").GetComponent<Camera>());
        cameras.Add(GameObject.Find("NullCam").GetComponent<Camera>());

        StartCoroutine(SelectSprite());
    }
    void Update()
    {
        if (character.HP <= 0)
        {
            GetComponent<RawImage>().texture = cameras[6].targetTexture;
        }
    }
    IEnumerator SelectSprite()
    {
        yield return new WaitForSeconds(0.001f);

        switch (character.character)
        {
            case GrandmaCharacter g:
                Debug.Log("Abuela");
                GetComponent<RawImage>().texture = cameras[0].targetTexture;
                break;
            case AddictCharacter ad:
                Debug.Log("Drogo");
                GetComponent<RawImage>().texture = cameras[1].targetTexture;
                break;
            case PiromaniacCharacter py:
                Debug.Log("Piro");
                GetComponent<RawImage>().texture = cameras[2].targetTexture;
                break;
            case InternCharacter i:
                Debug.Log("Becario");
                GetComponent<RawImage>().texture = cameras[3].targetTexture;
                break;
            case GraffitiPainterCharacter sa:
                Debug.Log("Raphinha");
                GetComponent<RawImage>().texture = cameras[4].targetTexture;
                break;
            case CulturistCharacter c:
                Debug.Log("Mazao");
                GetComponent<RawImage>().texture = cameras[5].targetTexture;
                break;
        }
    }
}
