using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FishMovement : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _jumpInterval = 3f;
    private Rigidbody _rb;
    private bool _isJumping = false;
    private float _timeSinceLastJump = 0f;
    private void Start()
    {
        //Take rigibody and lock rotation and X/Z position
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    private void FixedUpdate()
    {
        //Update jump timer
        _timeSinceLastJump += Time.deltaTime;

        //Check if its time to jump again
        if (_timeSinceLastJump >= _jumpInterval )
        {
            JumpFromHole();
            //Reset jump timer
            _timeSinceLastJump = 0f;
        }
    }
    
    private void JumpFromHole()
    {
        //If fish is not jumping, jump and set the isJumping booling on true to indicate that the fish is jumping.
        if (!_isJumping)
        {
            //Increase fish high using the jumpforce.
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the fish has landed back on the "fish ground" under the "player ground" so creates the effect of the fish going under the ground
        if (collision.gameObject.CompareTag("GroundFish"))
        {
            //Set is jumping back to false so the the JumpFromHole() function knows the fish is ready to jump again.
            _isJumping = false;
        }
    }
}
