using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFirstChar : MonoBehaviour
{
    public static APlayer startCharacter;

    public void SelectCharacter(APlayer data)
    {
        startCharacter = data;
    }
}
