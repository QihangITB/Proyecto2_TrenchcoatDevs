

using System.Collections.Generic;
using UnityEngine;

public interface IPoolable<T> where T : MonoBehaviour
{
    public Stack<T> OriginPool { set; }
    public void Return();    
}
