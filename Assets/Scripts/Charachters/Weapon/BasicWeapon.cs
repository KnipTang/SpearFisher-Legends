using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bulletTemplate = null;
    [SerializeField] 
    private List<Transform> _fireSockets = new List<Transform>();
    private bool _triggerPulled = false;
    private float _fireTimer = 0.0f;

    //Sound
    [SerializeField]
    private UnityEvent _onFireEvent;

    private void Update()
    {
        //handle the countdown of the fire timer
        if (_fireTimer > 0.0f)
            _fireTimer -= Time.deltaTime;

        if (_fireTimer <= 0.0f && _triggerPulled)
            FireProjectile();

        //the trigger will release by itself, 
        //if we still are firing, we will receive new fire input
        _triggerPulled = false;
    }

    private void FireProjectile()
    {
        //no bullet to fire
        if (_bulletTemplate == null)
            return;

        for (int i = 0; i < _fireSockets.Count; i++)
        {
            if (_fireSockets[i] != null)
            {
                Instantiate(_bulletTemplate, _fireSockets[i].position, _fireSockets[i].rotation);
            }
        }

        if (PlayerStats.instance == null) return;

        //set the time so we respect the firerate
        _fireTimer += 1.0f / PlayerStats.instance._fireRate;
        //Sound
        _onFireEvent?.Invoke();
    }

    //Sets triggerPulled to true so it starts shooting.
    public void Fire()
    {
        _triggerPulled = true;
    }
}


