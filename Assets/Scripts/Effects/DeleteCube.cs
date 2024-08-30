using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCube : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 2;
    private const string KILL_METHOD = "Kill";
    private void Awake()
    {
        //Destroys gameobject after lifetime
        Invoke(KILL_METHOD, _lifeTime);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
