using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField]
    private InputActionAsset _inputAsset = null;

    [SerializeField]
    private InputActionReference _movementAction = null;

    private InputAction _shootAction = null;

    protected Rigidbody _rigidBody = null;
    private GameObject _closestFish = null;

    [SerializeField]
    private GameObject _fishKeyIndicator = null;

    [SerializeField]
    private TextMeshProUGUI _textNoAmmo = null;

    private float _timer = 0;

    [SerializeField]
    private float _textTimer = 0.5f;

    [SerializeField]
    private int _minFishCaptureDistance = 3;

    [SerializeField]
    private GameObject _plusOnePrefab;

    [SerializeField]
    private const string CaptureFishInputKey = "e";
    protected override void Awake()
    {
        //Set noAmmo text indicator on false
        if (_textNoAmmo != null)
            _textNoAmmo.enabled = false;
        _rigidBody = GetComponent<Rigidbody>();
        
        //Check for null pointers for attack and movement scripts
        base.Awake();

        if (_inputAsset == null || _movementAction == null) return;
        _shootAction = _inputAsset.FindActionMap("Gameplay").FindAction("Shoot");
    }

    private void OnEnable()
    {
        if (_inputAsset == null || _movementAction == null) return;

        _inputAsset.Enable();
    }
    private void OnDisable()
    {
        if (_inputAsset == null || _movementAction == null) return;

        _inputAsset.Disable();
    }
    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandleAimingInput();
        HandleFishInput();
        NoAmmoTextHandler();
    }

    private void NoAmmoTextHandler()
    {
        //Delete text after x amount of seconds if enabled
        if (_textNoAmmo.enabled)
        {
            _timer += _textTimer * Time.deltaTime;
            if (_timer >= _textTimer)
            {
                _textNoAmmo.enabled = false;
                _timer = 0;
            }
        }
    }

    //Handle Movements input
    private void HandleMovementInput()
    {
        if (_movementBehaviour == null ||
            _movementAction == null)
            return;

        //movement
        float movementInput = _movementAction.action.ReadValue<float>();

        Vector3 movement = movementInput * Vector3.right;

        //Set the right movement direction
        _movementBehaviour.DesiredMovementDirection = movement;
    }

    private void HandleFishInput()
    {
        if (_rigidBody == null) return;

        //Get closestFish to player
        _closestFish = _movementBehaviour.HandleFishLookAt();

        if (_closestFish == null) return;

        //Get distance between closestFish and player
        float distance = Vector3.Distance(_rigidBody.transform.position, _closestFish.transform.position);

        //Decrease alpha color of key indicator
        var color = _fishKeyIndicator.GetComponent<Image>().color;
        color.a -= 1f * Time.fixedDeltaTime;

        //If distance between player and fish is smaller than the min distance to capture that fish
        if (distance < _minFishCaptureDistance)
        {
            //Set alpha indicator to max so its showing
            color.a = 255;
            //If player presses e -> fish gets captured
            if (Input.GetKeyDown(CaptureFishInputKey))
            {
                CaptureFish();
            }
        }
        //If player is to far from the fish set alpha to 0 so its in showing
        else
            color.a = 0;
        _fishKeyIndicator.GetComponent<Image>().color = color;
    }

    private void CaptureFish()
    {
        //If fish gets captured spawn captured indicator
        SpawnPlusOneIndicator(_closestFish.transform.position);


        Health fishHealth = _closestFish.GetComponent<Health>();

        if (fishHealth == null) return;

        //Damage fish so it dies
        fishHealth.Damage(9000);
    }

    private void HandleAttackInput()
    {
        if (_attackBehaviour == null
            || _shootAction == null || PlayerStats.instance == null)
            return;

        //If player tries to shoot
        if (_shootAction.IsPressed())
        {
            //check for ammo
            if(PlayerStats.instance._ammo > 0)
                _attackBehaviour.Attack();
            //If there is no ammo left display no ammo screen.
            else
                _textNoAmmo.enabled = true;
        }
    }

    //Handle Aim input
    private void HandleAimingInput()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.transform.position.y - transform.position.y;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.y = transform.position.y;
        //Set the right aim position
        _movementBehaviour.DesiredLookatPoint = worldMousePosition;
    }

    void SpawnPlusOneIndicator(Vector3 fishPos)
    {
        //Spawn indicator when fish gets destroyed
        Instantiate(_plusOnePrefab, fishPos, Quaternion.identity);
    }
}



