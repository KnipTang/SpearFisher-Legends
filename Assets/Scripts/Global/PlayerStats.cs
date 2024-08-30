using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    // Start is called before the first frame update
    public int _ammo = 10;
    public int _movementSpeed = 1;
    public float _fireRate = 1f;
    public int _money = 0;
    public int _score = 0;
    public int _maxHealth = 100;
    public int _currentPlayerHealth = 100;

    public delegate void StatsChange(int ammo, int movementSpeed, float fireRate, int money, int score, int maxHealth, int currentPlayerHealth);
    public event StatsChange OnStatsChanged;
    private void Awake()
    {
        instance = this;
    }

    //Invoke function gets called if any of the variables gets changed in the menu screen
    public void InvokeStatsChanged()
    {
        OnStatsChanged?.Invoke(_ammo, _movementSpeed, _fireRate, _money, _score, _maxHealth, _currentPlayerHealth);
    }

    private void OnDestroy()
    {
        OnStatsChanged -= OnStatsChanged;
    }
}
