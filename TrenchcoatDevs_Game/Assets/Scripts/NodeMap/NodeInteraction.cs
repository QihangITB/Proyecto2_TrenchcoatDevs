using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteraction : MonoBehaviour
{
    const string PlayerTag = "Player";

    public GameObject SelectionObject;
    public float PlayerSpeed = 1f;
    public float PlayerDistance = 0.5f;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag(PlayerTag);
    }

    void OnMouseOver()
    {
        Debug.Log("Mouse is over GameObject.");
        SelectionObject.SetActive(true);
    }

    void OnMouseExit()
    {
        SelectionObject.SetActive(false);
    }

    void OnMouseDown()
    {
        SelectionObject.SetActive(false);
        StartCoroutine(MovePlayerToNode(PlayerSpeed, PlayerDistance));
    }

    void OnMouseUp()
    {
    }

    private IEnumerator MovePlayerToNode(float speed, float distance)
    {
        Rigidbody rb = _player.GetComponent<Rigidbody>();
        Vector3 direction;

        while (Vector3.Distance(_player.transform.position, transform.position) > distance)
        {
            direction = (transform.position - _player.transform.position).normalized;

            rb.MovePosition(_player.transform.position + direction * speed * Time.deltaTime);

            yield return null;
        }

        rb.MovePosition(transform.position);
    }
}
