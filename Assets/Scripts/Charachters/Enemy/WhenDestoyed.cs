using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenDestoyed : MonoBehaviour
{
    [SerializeField]
    private int _money  = 1;
    [SerializeField]
    private int _score  = 1;
    [SerializeField]
    private int _enemie = 0;
    [SerializeField]
    private int _fish   = 0;
    public void OnDead()
    {
        if (GameStats.instance == null || PlayerStats.instance == null) return;

        //Update Game/Player global stats when an enemy gets killed/Captured
        GameStats.instance._totalKilledFish    += _fish;
        GameStats.instance._totalKilledEnemies += _enemie;
        GameStats.instance._nrOfEnemies -= _enemie;
        GameStats.instance._nrOfFish -= _fish;
        GameStats.instance._moneyForEnemy += _enemie * _money;
        GameStats.instance._moneyForFish += _fish * _money;
        GameStats.instance._totalWaveMoney += _money;
        PlayerStats.instance._score += _score;

        GameStats.instance.InvokeStatsChanged();
    }
}
