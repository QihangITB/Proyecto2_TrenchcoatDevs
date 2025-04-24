using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(PositionConstraint))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerNetGhost : NetworkBehaviour
{
    private static Dictionary<ulong, PlayerNetGhost> _instances = new Dictionary<ulong, PlayerNetGhost>();
    public const string PlayerTag = "Player";
    [SerializeField]
    float _distanceOffsetTrigger;
    [SerializeField]
    Vector3 _onProximityOffset;
    Transform _player;
    PositionConstraint _constraint;
    public override void OnNetworkSpawn()
    {
        _player = GameObject.FindWithTag(PlayerTag).transform;
        _instances.Add(OwnerClientId,this);
        if (IsOwner)
        {
            GetComponent<MeshRenderer>().enabled = false;
            _constraint = GetComponent<PositionConstraint>();
            AddPositionConstraint();
            _constraint.constraintActive = true;
        }       
    }
    public override void OnNetworkDespawn()
    {
        _instances.Remove(OwnerClientId);
    }
    private void FixedUpdate()
    {
        if (IsOwner && IsSpawned)
        {
            PlayerNetGhost otherNetworkGhost = _instances.Values.Where(obj=>obj.OwnerClientId!=OwnerClientId).FirstOrDefault();
            if (otherNetworkGhost != null)
            {
                float distance = Vector3.Distance(transform.position, otherNetworkGhost.transform.position);
                if (distance <= _distanceOffsetTrigger)
                {
                    _constraint.translationOffset = _onProximityOffset;
                }
                else
                {
                    _constraint.translationOffset = Vector3.zero;
                }
            }
            
        }
    }
    void AddPositionConstraint()
    {
        ConstraintSource conSource = new();

        conSource.sourceTransform = _player;
        conSource.weight = 1.0f;

        _constraint.AddSource(conSource);
    }
}
