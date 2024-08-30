using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    private Vector3 _moveDirection;

    [SerializeField]
    private string _horizontalInputAxis = "Horizontal";

    [SerializeField]
    private string _verticalInputAxis = "Vertical";

    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        if (_rb == null) return;

        float moveX = Input.GetAxis(_horizontalInputAxis);
        float moveZ = Input.GetAxis(_verticalInputAxis);

        // Set the Y component to zero to prevent upward movement
        _moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void Move()
    {
        // Calculate the new velocity only in the X-Z plane
        if (_rb == null || _moveDirection == null || PlayerStats.instance == null) return;
        Vector3 newVelocity = new Vector3(_moveDirection.x * PlayerStats.instance._movementSpeed, _rb.velocity.y, _moveDirection.z * PlayerStats.instance._movementSpeed);
        _rb.velocity = newVelocity;
    }
}