using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeedPartner : MonoBehaviour
{
    public void OnClick(int familyMember)
    {
        if (FamilyFood.instance == null || PlayerStats.instance == null) return;
        //Get cost to feed family member
        int cost = FamilyFood.instance.CalculateCost(familyMember);
        //Feed family member(s) and decrease the cost from money
        if (cost < PlayerStats.instance._money)
        {
            //If cost is lower than the players money feed family member
            FamilyFood.instance.Feed(familyMember, cost);
        }
        else
        {
            //If cost is higher than the players money show no money indicator
            NoMoneyIndicator indicator = GetComponentInParent<NoMoneyIndicator>();
            indicator?.NoMoney();
        }
        //call InvokeStatsChanged to update the changed family stats
        FamilyFood.instance.InvokeStatsChanged();
    }
}
