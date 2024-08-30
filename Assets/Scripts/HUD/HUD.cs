using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private ProgressBar _healthbar = null;
    private Label _enemieLabel = null;
    private Label _fishLabel = null;
    private Label _ammoLabel = null;
    private ProgressBar _timerbar = null;

    private PlayerCharacter _player;


    void Start()
    {
        //UI
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument != null)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root != null)
        {
            //Set all elements to variable
            _player = FindObjectOfType<PlayerCharacter>();
            _healthbar = _root.Q<ProgressBar>("PlayerHealthbar");
            _enemieLabel = _root.Q<Label>("PlayerEnemies");
            _fishLabel = _root.Q<Label>("PlayerFish");
            _ammoLabel = _root.Q<Label>("PlayerAmmo");
            _timerbar = _root.Q<ProgressBar>("Timerbar");

            if (_player != null)
            {
                Health playerHealth = _player.GetComponent<Health>();
                if (playerHealth != null && PlayerStats.instance != null)
                {
                    //If the player health changes
                    // initialize
                    UpdateHealth(PlayerStats.instance._maxHealth, PlayerStats.instance._currentPlayerHealth);
                    // hook to monitor changes
                    playerHealth.OnHealthChanged += UpdateHealth;
                }
                if (GameStats.instance != null)
                {
                    //If any of the stats that are shown in the game HUD get changed
                    // initialize
                    UpdateStats(GameStats.instance._nrOfEnemies, GameStats.instance._nrOfFish, PlayerStats.instance._ammo);
                    // hook to monitor changes
                    GameStats.instance.OnStatsChanged += UpdateStats;
                }
            }
        }
    }

    private void Update()
    {
        if (GameStats.instance == null) return;

        //Update the time
        UpdateTimer();

        //Use keys
        if (Input.anyKeyDown) // Check if any key is pressed
        {
            switch (Input.inputString)
            {
                case "r":
                case "R":
                    // Reload the current scene (reset the game)
                    HUD_ResetGame.instance.ResetGame();
                    break;
                default:
                    // Handle other key presses or do nothing
                    break;
            }
        }
    }

    //Update health bar
    private void UpdateHealth(float startHealth, float currentHealth)
    {
        //If the health gets changed update the health bar indicator
        if (_healthbar == null) return;
        _healthbar.title = $"{currentHealth}/{startHealth}";
        _healthbar.value = currentHealth;
        _healthbar.highValue = startHealth;
    }

    private void UpdateStats(int nrOfEnemies, int nrOfFish, int ammo)
    {
        //If any of the stats gets changed update the indicators
        if (_enemieLabel == null || _fishLabel == null) return;
        _enemieLabel.text = $"Enemy: {nrOfEnemies}";
        _fishLabel.text = $"Fish: {nrOfFish}";
        _ammoLabel.text = $"Ammo: {ammo}";
    }

    //Update timer bar
    private void UpdateTimer()
    {
        if (_timerbar == null) return;
        //Update timer and round up the timer so it only shows whole numbers
        _timerbar.title = $"{Mathf.Round(GameStats.instance._currentTime)}/{Mathf.Round(GameStats.instance._startTime)}";
        _timerbar.value = GameStats.instance._currentTime;
        _timerbar.highValue = GameStats.instance._startTime;
    }
}


