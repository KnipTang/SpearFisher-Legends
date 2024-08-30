using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class HUD_Stats : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textStats;
    void Start()
    {
        if (PlayerStats.instance == null ||
            GameStats.instance   == null ||
            FamilyFood.instance  == null ||
            _textStats           == null) return;

        //Display all the stats of the last wave
        int totalKilledFish = GameStats.instance._totalKilledFish;
        int totalKilledEnemies = GameStats.instance._totalKilledEnemies;

        int totalFishMoney = GameStats.instance._moneyForFish;
        int totalEnemyMoney = GameStats.instance._moneyForEnemy;

        int totalEarnedMoney = GameStats.instance._totalWaveMoney;

        StringBuilder statsStringBuilder = new StringBuilder();
        statsStringBuilder.AppendLine($"Fish: {totalKilledFish} -> {totalFishMoney}$");
        statsStringBuilder.AppendLine($"Enemy: {totalKilledEnemies} -> {totalEnemyMoney}$");
        statsStringBuilder.AppendLine($"Total killed: {totalKilledFish + totalKilledEnemies}");
        statsStringBuilder.AppendLine();
        statsStringBuilder.AppendLine($"Sell: {totalEarnedMoney}$");
        statsStringBuilder.AppendLine();
        statsStringBuilder.AppendLine($"Money: {PlayerStats.instance._money}$ + {totalEarnedMoney}$");
        statsStringBuilder.Append($"-> {PlayerStats.instance._money + totalEarnedMoney}$");

        _textStats.text = statsStringBuilder.ToString();
    }
}
