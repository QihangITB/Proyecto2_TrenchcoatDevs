using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class DerivedNetworkTransform : NetworkTransform
{
    public enum AuthorityModes
    {
        Owner,
        Server
    }

    [SerializeField]
    public AuthorityModes AuthorityMode = AuthorityModes.Owner;

    protected override bool OnIsServerAuthoritative()
    {
        return AuthorityMode == AuthorityModes.Server;
    }
}
