using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(PositionConstraint))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerNetGhost : NetworkBehaviour
{
    public const string PlayerTag = "Player";
    PositionConstraint _constraint;
    private void OnNetworkInstantiate()
    {
        if (IsOwner)
        {
            GetComponent<MeshRenderer>().enabled = false;
            _constraint = GetComponent<PositionConstraint>();
            AddPositionConstraint();
            _constraint.constraintActive = true;
        }       
    }
    void AddPositionConstraint()
    {
        Transform player = GameObject.FindWithTag(PlayerTag).transform;
        ConstraintSource conSource = new();

        conSource.sourceTransform = player;
        conSource.weight = 1.0f;

        _constraint.AddSource(conSource);
    }
}
