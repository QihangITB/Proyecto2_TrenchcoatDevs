using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteraction : MonoBehaviour
{
    const string PlayerTag = "Player";

    public GameObject SelectionObject;
    public GameObject NonSelectionObject;
    public float PlayerSpeed = 10f;
    public float PlayerDistance = 0.5f;
    public bool IsInteractable = true;

    public static event Action OnPlayerArrivesToNode;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag(PlayerTag);
    }

    void OnMouseOver()
    {
        if (IsInteractable)
        {
            SelectionObject.SetActive(true);
        }
        else
        {
            NonSelectionObject.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        SelectionObject.SetActive(false);
        NonSelectionObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (IsInteractable)
        {
            SelectionObject.SetActive(false);
            StartCoroutine(MovePlayerToNode(PlayerSpeed, PlayerDistance));
        }
    }

    private IEnumerator MovePlayerToNode(float speed, float distance)
    {
        Rigidbody rb = _player.GetComponent<Rigidbody>();
        float initialY = _player.transform.position.y;

        while (Vector3.Distance(_player.transform.position, transform.position) > distance)
        {
            Vector3 direction = (transform.position - _player.transform.position).normalized;
            Vector3 newPosition = _player.transform.position + direction * speed * Time.deltaTime;
            newPosition.y = initialY;

            rb.MovePosition(newPosition);

            yield return null;
        }

        Vector3 finalPosition = transform.position;
        finalPosition.y = initialY;
        rb.MovePosition(finalPosition);

        OnPlayerArrivesToNode?.Invoke();

        StopAllCoroutines();
    }
}
