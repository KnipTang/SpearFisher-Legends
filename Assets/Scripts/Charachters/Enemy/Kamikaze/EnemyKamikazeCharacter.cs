using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] 
    private float _attackRange = 2.0f;
    [SerializeField]
    private int _damageAmount = 20;

    private void Start()
    {
        //expensive method, use with caution
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player) 
            _playerTarget = player.gameObject;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null || _playerTarget == null)
            return;

        //Set the target of the enemy to the player
        _movementBehaviour.Target = _playerTarget;
    }

    private const string KILL_METHOD = "Kill";
    void HandleAttacking()
    {
        if (_playerTarget == null) return;

        Health playerHealth = _playerTarget.GetComponent<Health>();
        if (_attackBehaviour == null) return;


        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude
            < _attackRange * _attackRange)
        {
            _attackBehaviour.Attack();

            playerHealth.Damage(_damageAmount);
            Health enemyHealth = GetComponent<Health>();
            enemyHealth.Kill();
            //this is a kamikaze enemy, 
            //when it fires, it should destroy itself
            //we do this with a delay so other logic (like player feedback and the attack, will have the time to execute)
            Invoke(KILL_METHOD, 0.2f);
        }
    }
}

