using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int _price = 50;
    [SerializeField]
    private int _ammoIncrease = 10;
    public void BuyAmmo()
    {
        if (PlayerStats.instance == null) return;
        //Checks if player has enough money, if they do decrease the amount it costs and increase the bought perks.
        if (PlayerStats.instance._money >= _price)
        {
            PlayerStats.instance._money -= _price;
            PlayerStats.instance._ammo += _ammoIncrease;
            //Call InvokeStatsChange so the global player stats know they have been changed
            PlayerStats.instance.InvokeStatsChanged();
        }
        else
            //If the player doesnt have enough money call noMoney Function
            NoMoney();
    }

    public void BuyHealth()
    {
        if (PlayerStats.instance == null) return;
        //If the health is already max just return
        if (PlayerStats.instance._currentPlayerHealth == PlayerStats.instance._maxHealth)
                return;
        //Else if the player has enough money. put the healf on max and decrease money
        else if (PlayerStats.instance._money >= _price)
        {
            PlayerStats.instance._money -= _price;
            PlayerStats.instance._currentPlayerHealth = PlayerStats.instance._maxHealth;
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
