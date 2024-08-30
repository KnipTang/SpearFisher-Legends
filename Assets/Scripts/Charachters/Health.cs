using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 10;
    public int _currentHealth = 10;
    public float StartHealth { get { return _startHealth; } }
    public float CurrentHealth { get { return _currentHealth; } }

    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChanged;

    [SerializeField]
    private GameObject _gotHitScreen = null;

    [SerializeField] GameObject _attackVFXTemplate = null;

    private float _hitColorIntensity = 0.8f;

    [SerializeField]
    private TextMeshProUGUI _damageAmountIndicator = null;

    private float _damageIndicatorTimer = 0f;
    [SerializeField]
    private float _damageIndicatorDelay = 1.5f;

    const string PLAYER_TAG = "Player";
    private void Start()
    {
        //If object is player set health stats to the global player healf stats.
        if (CompareTag(PLAYER_TAG))
        {
            if (PlayerStats.instance == null) return;
            _currentHealth = PlayerStats.instance._currentPlayerHealth;
            _startHealth = PlayerStats.instance._maxHealth;
        }
        else
            _currentHealth = _startHealth;
    }
    public void Damage(int amount)
    {
        //Lower the currentHealth to the damage amount.
        _currentHealth -= amount;

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);

        //If health of object is lower or equal to 0 destroy/kill it
        if (_currentHealth <= 0)
        {
            Kill();
        }

        if (CompareTag(PLAYER_TAG))
        {
            //If the object is player also change the global health stats.
            if (PlayerStats.instance == null) return;
            PlayerStats.instance._currentPlayerHealth = _currentHealth;

            //Set the color effect when player is hit.
            if (_gotHitScreen == null) return;
            var color = _gotHitScreen.GetComponent<Image>().color;
            color.a = _hitColorIntensity;

            _gotHitScreen.GetComponent<Image>().color = color;

            if (_damageAmountIndicator == null) return;
            _damageAmountIndicator.enabled = true;
            _damageAmountIndicator.text = "-" + amount;
        }
    }

    private void FixedUpdate()
    {
        //If player has been hit slowly reduse the hit color effect
        if (CompareTag(PLAYER_TAG))
        {
            if (_gotHitScreen == null) return;
            if (_gotHitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = _gotHitScreen.GetComponent<Image>().color;
                color.a -= 1f * Time.fixedDeltaTime;
                color.a = Mathf.Max(color.a, 0f);
                _gotHitScreen.GetComponent<Image>().color = color;
            }

            if (_damageAmountIndicator == null) return;
            if (_damageAmountIndicator.enabled)
            {
                //Check if the timer has reached the delay
                if (_damageIndicatorTimer >= _damageIndicatorDelay)
                {
                    //Reset the timer
                    _damageIndicatorTimer = 0f;

                    //Disable the damage indicator
                    _damageAmountIndicator.enabled = false;
                }
                else
                {
                    //Increment the timer
                    _damageIndicatorTimer += Time.fixedDeltaTime;
                }
            }
        }
    }
    const string ENEMY_TAG = "Enemy";
    const string FISH_TAG = "Fish";
    public void Kill()
    {
        if (!CompareTag(PLAYER_TAG))
        {
            //If the killed object is not a player call onDead to update global stats
            WhenDestoyed destroy = GetComponent<WhenDestoyed>();
            destroy.OnDead();
        }
        if (CompareTag(ENEMY_TAG))
        {
            //Set the sound of true so the audio player knows what sound to play
            AudioPlayer.instance._enemyKilledSound = true;
            Explosion explosion = GetComponent<Explosion>();
            explosion.Explode();
        }
        if(CompareTag(FISH_TAG))
        {
            //Set the sound of true so the audio player knows what sound to play
            AudioPlayer.instance._fishKilledSound = true;
        }

        //If any object is killed spawn VFX effect
        if (_attackVFXTemplate)
            Instantiate(_attackVFXTemplate, transform.position, transform.rotation);
        //Play sound 
        AudioPlayer.instance.Sound();
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnHealthChanged -= OnHealthChanged;
    }
}