using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScorePointClickable : NetworkBehaviour, IPoolable<ScorePointClickable>
{
    [SerializeField]
    private uint _points;
    private Stack<ScorePointClickable> _originPool;
    public uint Points
    {
        get { return _points; }
    }

    public Stack<ScorePointClickable> OriginPool { set => _originPool = value; }

    public void Return()
    {
        throw new System.NotImplementedException();
    }
}
