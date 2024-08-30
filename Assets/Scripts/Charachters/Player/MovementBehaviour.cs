using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected GameObject _arrowObject = null;

    protected Rigidbody _rigidBody;

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    protected Vector3 _desiredLookAtPoint = Vector3.zero;

    protected Vector3 _desiredLookArrowPoint = Vector3.zero;

    protected GameObject _target;

    protected bool _grounded = false;

    protected const float GROUND_CHECK_DISTANCE = 0.2f;
    protected const string GROUND_LAYER = "Ground";

    private GameObject _closestFish = null;

    [SerializeField]
    private string _horizontalInputAxis = "Horizontal";

    [SerializeField]
    private string _verticalInputAxis = "Vertical";

    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }

    public Vector3 DesiredLookatPoint
    {
        get { return _desiredLookAtPoint; }
        set { _desiredLookAtPoint = value; }
    }
    public Vector3 DesiredLookArrowPoint
    {
        get { return _desiredLookArrowPoint; }
        set { _desiredLookArrowPoint = value; }
    }
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleLookAt();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
        HandleFishLookAt();
        //check if there is ground beneath our feet
        _grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, GROUND_CHECK_DISTANCE, LayerMask.GetMask(GROUND_LAYER));
    }

    //Handle player movement
    protected virtual void HandleMovement()
    {
        if (_rigidBody == null || _desiredMovementDirection == null || PlayerStats.instance == null) return;

        float moveX = Input.GetAxis(_horizontalInputAxis);
        float moveY = Input.GetAxis(_verticalInputAxis);

        Vector3 movement = _desiredMovementDirection.normalized;
        //Add global player movement speed
        movement *= PlayerStats.instance._movementSpeed;

        //maintain vertical velocity as it was otherwise gravity would be stripped out
        movement = new Vector3(moveX, moveY, 0);
        _rigidBody.velocity = movement;
    }

    protected virtual void HandleLookAt()
    {
        //Player look at mouse position
        if (_desiredLookAtPoint == null) return;
        gameObject.transform.LookAt(_desiredLookAtPoint);

        //Arrow indicator look at fish position
        if (_arrowObject == null || _closestFish == null) return;
        _arrowObject.transform.LookAt(new Vector3(_closestFish.transform.position.x, transform.position.y, _closestFish.transform.position.z));
    }

    const string FISH_TAG = "Fish";
    //Find closest fish to point arrow at.
    public virtual GameObject HandleFishLookAt()
    {
        if (_rigidBody == null) return null;
        //Get all fish objects
        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag(FISH_TAG);

        if (_arrowObject == null || fishObjects == null) return null;
        //loop over all fish objects
        if (fishObjects.Length > 0)
        {
            //If there are any fish left turn on the arrow indicator
            _arrowObject.gameObject.SetActive(true);
            //Get distance between the player and 1 of the fish
            float closestDistance = Vector3.Distance(_rigidBody.transform.position, fishObjects[0].transform.position);
            //Set this one temp as the closest fish
            _closestFish = fishObjects[0];

            //loop over all the other fish
            foreach (GameObject target in fishObjects)
            {
                //Take the distance between the player and the fish
                float distance = Vector3.Distance(_rigidBody.transform.position, target.transform.position);
                //check if this fish is closer to the player than the other fish that have already been checked
                if (distance < closestDistance)
                {
                    //if so update the closest fish to this fish
                    closestDistance = distance;
                    _closestFish = target;
                }
            }
            return _closestFish;
        }
        //If no fish are left in the round disable the arrow indicator to remove confusion
        else
            _arrowObject.gameObject.SetActive(false);

        return null;
    }
}