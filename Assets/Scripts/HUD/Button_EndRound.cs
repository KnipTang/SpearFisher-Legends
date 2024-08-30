using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_EndRound : MonoBehaviour
{
    [SerializeField]
    private Canvas _currentCanvas = null;
    [SerializeField]
    private Canvas _newCanvas = null;
    public void ButtonClicked()
    {
        SellAll();
        SwitchCanvas();
    }

    //Sell all killed objects and increase money
    protected void SellAll()
    {
        if (GameStats.instance == null || PlayerStats.instance == null) return;

        //Add all money earned that round to the players money
        PlayerStats.instance._money += GameStats.instance._totalWaveMoney;
        //Reset the gameStats variables of that wave for the next round
        GameStats.instance._moneyForEnemy = 0;
        GameStats.instance._moneyForFish = 0;
        GameStats.instance._totalWaveMoney = 0;
        GameStats.instance._totalKilledFish = 0;
        GameStats.instance._totalKilledEnemies = 0;
        //Call InvokeStatsChanged to update the changed playerStats
        PlayerStats.instance.InvokeStatsChanged();
    }

    protected void SwitchCanvas()
    {
    //Switch between stats menu screen canvas to button menu screen canvas
        if (_currentCanvas == null || _newCanvas == null) return;
        _currentCanvas.gameObject.SetActive(false);
        _newCanvas.gameObject.SetActive(true);
    }
}
