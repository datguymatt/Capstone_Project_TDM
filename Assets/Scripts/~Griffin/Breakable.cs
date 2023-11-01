using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        Destroy(gameObject);
    }
}
