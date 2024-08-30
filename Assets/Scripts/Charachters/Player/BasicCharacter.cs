using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    protected AttackBehaviour _attackBehaviour;
    protected MovementBehaviour _movementBehaviour;

    protected virtual void Awake()
    {
        _attackBehaviour = GetComponent<AttackBehaviour>();
        _movementBehaviour = GetComponent<MovementBehaviour>();

        if (_attackBehaviour == null)
        {
            Debug.LogError("AttackBehaviour component not found.");
        }

        if (_movementBehaviour == null)
        {
            Debug.LogError("MovementBehaviour component not found.");
        }
    }
}

