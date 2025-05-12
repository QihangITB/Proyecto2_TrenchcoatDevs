using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    //gira el objeto al que se le asigna este script poco a poco
    private void Update()
    {
        //gira el objeto al que se le asigna este script poco a poco
        transform.Rotate(0, 0.5f, 0);
    }
}
