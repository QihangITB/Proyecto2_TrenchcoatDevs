using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ScrollViewHandler<TComponent> : MonoBehaviour where TComponent : MonoBehaviour
{
    List<TComponent> _scrollViewElements = new List<TComponent>();
    List<TComponent> _pool = new List<TComponent>();
    [SerializeField]
    [Tooltip("The provided GameObject should be a prefab... Ok?")]
    TComponent _scrollViewComponentPrefab;
    [SerializeField]
    Transform _scrollViewContentObjects;

    public TComponent AddSectionToList()
    {
        TComponent[] viableObjects = _pool.Where(obj => !obj.gameObject.activeSelf).ToArray();
        TComponent spawnComponent;
        if (viableObjects.Count() > 0)
        {
            spawnComponent = viableObjects[0];
            spawnComponent.gameObject.SetActive(true);
        }
        else
        {
            spawnComponent = Instantiate(_scrollViewComponentPrefab,transform.parent);
        }
        spawnComponent.transform.SetParent(_scrollViewContentObjects);
        _scrollViewElements.Add(spawnComponent);
        return spawnComponent;
    }
    public void EmptyList()
    {
        foreach (TComponent content in _scrollViewElements)
        {
            content.gameObject.SetActive(false);
        }
        _scrollViewElements.Clear();
    }
}
