using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected GameObject _poolObj = null;
    [SerializeField]
    private float _poolY_Pos = 0.5f;
    void Start()
    {
        //Spawn a bool at the same location as the fish
        if (_poolObj != null)
        {
            Instantiate(_poolObj, new Vector3(transform.position.x, _poolY_Pos, transform.position.z), transform.rotation);
        }
    }
}
