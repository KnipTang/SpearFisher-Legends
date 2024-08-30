using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public static GameStats instance;
    public int _totalWaveMoney = 0;
    public int _totalKilledFish = 0;
    public int _totalKilledEnemies = 0;
    public int _nrOfWave = 0;
    public int _nrOfFish = 5;
    public int _nrOfEnemies = 5;
    public int _moneyForFish = 0;
    public int _moneyForEnemy = 0;

    public delegate void StatsChange(int nrOfEnemies, int nrOfFish, int ammo);
    public event StatsChange OnStatsChanged;

    public float _currentTime = 30;
    public float _startTime = 30;
    private void Awake()
    {
        instance = this;
    }

    //Invoke function gets called when mid game any of these variables gets changed
    public void InvokeStatsChanged()
    {
        if (PlayerStats.instance == null) return;
        OnStatsChanged?.Invoke(_nrOfEnemies, _nrOfFish, PlayerStats.instance._ammo);
    }

    private void OnDestroy()
    {
        OnStatsChanged -= OnStatsChanged;
    }
}
