using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackBehaviour : MonoBehaviour
{
    //Template for the gun
    [SerializeField]
    private GameObject _gunTemplate = null;
    //Position of the gun in regard of player
    [SerializeField]
    private GameObject _socket = null;
    //Script for weapon behavior
    private BasicWeapon _weapon = null;
    void Awake()
    {
        //Spawn weapon
        if (_gunTemplate != null && _socket != null)
        {
            var gunObject = Instantiate(_gunTemplate,
            _socket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _weapon = gunObject.GetComponent<BasicWeapon>();
        }
    }
    public void Attack()
    {
        //Call fire function of weapon.
        if (_weapon != null && _gunTemplate != null && _socket != null)
        {
            _weapon.Fire();
        }
    }
}