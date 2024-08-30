using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TMPro;
using UnityEngine;

public class HUD_UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textStats = null;

    [SerializeField]
    private TextMeshProUGUI _textFeedAll = null;
    private void Start()
    {
        if (PlayerStats.instance == null ||
            GameStats.instance == null ||
            FamilyFood.instance == null) return;

        //If any of the player stats gets changed while in the menu screen
        // initialize
        StatsChange(
            PlayerStats.instance._ammo, 
            PlayerStats.instance._movementSpeed, 
            PlayerStats.instance._fireRate, 
            PlayerStats.instance._money, 
            PlayerStats.instance._score, 
            PlayerStats.instance._maxHealth, 
            PlayerStats.instance._currentPlayerHealth
        );
        // hook to monitor changes
        PlayerStats.instance.OnStatsChanged += StatsChange;

        //If any of the family stats gets changed while in the menu screen
        // initialize
        FamilyStats(new int[FamilyFood.instance._amountOfMembers]);
        // hook to monitor changes
        FamilyFood.instance.OnStatsChanged += FamilyStats;
    }

    private void StatsChange(int ammo, int movementSpeed, float fireRate, int money, int score, int maxHealth, int currentPlayerHealth)
    {
        if (_textStats == null) return;

        //Update the player stats info in the hub when any if there values changes because of a upgrade/buy
        StringBuilder statsStringBuilder = new StringBuilder();
        statsStringBuilder.AppendLine($"Money: {money}$ \tFireRate: {fireRate}");
        statsStringBuilder.AppendLine($"Ammo: {ammo} \tSpeed: {movementSpeed}");
        statsStringBuilder.Append($"Health: {currentPlayerHealth}/{maxHealth}");

        _textStats.text = statsStringBuilder.ToString();
    }

    private void FamilyStats(int[] family)
    {
        if (_textFeedAll == null) return;

        //Update the family stats info in the hub when any if there values changes because of a feeding them
        int totalCost = 0;
        //Loop though all the family members to calculate the cost of feeding the whole family
        for (int i = 0; i < family.Length; i++)
        {
            totalCost += FamilyFood.instance.CalculateCost(i);
        }

        _textFeedAll.text = $"FeedAll {totalCost}$";
    }
}
