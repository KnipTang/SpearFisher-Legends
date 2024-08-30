using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int _price = 100;
    [SerializeField]
    private int _healthIncrease = 50;
    [SerializeField]
    private float _fireRateIncrease = 0.3f;
    [SerializeField]
    private int _speedIncrease = 1;
    public void MaxHealth()
    {
        if (PlayerStats.instance == null) return;

        //Checks if player has enough money, if they do decrease the amount it costs and increase the bought stats.
        if (PlayerStats.instance._money >= _price)
        {
            PlayerStats.instance._money -= _price;
            PlayerStats.instance._maxHealth += _healthIncrease;
            //Call InvokeStatsChange so the global player stats know they have been changed
            PlayerStats.instance.InvokeStatsChanged();
        }
        else
            //If the player doesnt have enough money call noMoney Function
            NoMoney();
    }

    public void FireRate()
    {
        if (PlayerStats.instance == null) return;

        //Checks if player has enough money, if they do decrease the amount it costs and increase the bought stats.
        if (PlayerStats.instance._money >= _price)
        {
            PlayerStats.instance._money -= _price;
            PlayerStats.instance._fireRate += _fireRateIncrease;
            //Call InvokeStatsChange so the global player stats know they have been changed
            PlayerStats.instance.InvokeStatsChanged();
        }
        else
            //If the player doesnt have enough money call noMoney Function
            NoMoney();
    }

    public void Speed()
    {
        if (PlayerStats.instance == null) return;

        //Checks if player has enough money, if they do decrease the amount it costs and increase the bought stats.
        if (PlayerStats.instance._money >= _price)
        {
            PlayerStats.instance._money -= _price;
            PlayerStats.instance._movementSpeed += _speedIncrease;
            //Call InvokeStatsChange so the global player stats know they have been changed
            PlayerStats.instance.InvokeStatsChanged();
        }
        else            
            //If the player doesnt have enough money call noMoney Function
            NoMoney();
    }

    private void NoMoney()
    {
        //Shows NoMoney Indicator
        NoMoneyIndicator indicator = GetComponentInParent<NoMoneyIndicator>();
        indicator?.NoMoney();
    }
}
