using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image staticImage;
    public RawImage animImage;
    public Animator anim;
    private void Awake()
    {
        staticImage.enabled = true;
        animImage.enabled = false;
        anim.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        staticImage.enabled = false;
        animImage.enabled = true;
        anim.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.Play(anim.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
        anim.Update(0);
        staticImage.enabled = true;
        animImage.enabled = false;
        anim.enabled = false;
    }
}
