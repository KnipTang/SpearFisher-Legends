using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FamilyFood : MonoBehaviour
{
    public static FamilyFood instance;
    public int[] _family;
    [SerializeField]
    private int _minDecreaseInterval = 30;
    [SerializeField]
    private int _maxDecreaseInterval = 75;
    private int _maxFood = 100;
    [SerializeField]
    public int _amountOfMembers = 4;

    public delegate void StatsChange(int[] family);
    public event StatsChange OnStatsChanged;
    void Start()
    {
        _family = new int[_amountOfMembers];
        //Set all family members food points to there max at the start of the game
        for (int i = 0; i < _family.Length; i++)
        {
            _family[i] = _maxFood;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void InvokeStatsChanged()
    {
        OnStatsChanged?.Invoke(_family);
    }

    //Decrease the "food" points of the family members at the end of a round
    public void EndRoundFood()
    {
        int deadCounter = 0;
        //Loop over all family members
        for (int i = 0; i < _family.Length; i++)
        {
            //Take a random number between a range to indicate by how many food points every family members has to be decreased at the end of a round
            float decreaseAmount = Random.Range(_minDecreaseInterval, _maxDecreaseInterval);
            decreaseAmount = Mathf.Round(decreaseAmount / 5.0f) * 5.0f;
            _family[i] -= Mathf.FloorToInt(decreaseAmount);

            //If a family members had under 0 "Food" points they die
            if (_family[i] <= 0)
            {
                _family[i] = 0;
                deadCounter++;
            }
        }
        //If all family members dead the player loses
        if (deadCounter == _family.Length)
        {
            SceneManager.LoadScene("MenuScore");
        }

        InvokeStatsChanged();
    }

    //Calculate how much is cost to feed a family members
    public int CalculateCost(int familyMember)
    {
        //if the family members food points is 0 or below they are dead. So return 0 because they can't be fed anymore
        if (_family[familyMember] <= 0) return 0;
        //Calculate the cost to feed the family member and return it
        int cost = (_maxFood - _family[familyMember]);
        if (cost < 0)
            return 0;
        else
        return cost;
    }

    //Increases the "food" points of a family member if the player has enough money to feed them
    public bool Feed(int familyMember, int cost)
    {
        //check if the family members food points is above 0 they are not dead.
        if (PlayerStats.instance != null && _family[familyMember] > 0)
        {
            //if the player can afford to feed the family member set its food points to max, decrease the money of the player by the cost and call invokestatschanged to update the changes made
            if (PlayerStats.instance._money > cost)
            {
                _family[familyMember] = _maxFood;
                PlayerStats.instance._money -= cost;
                PlayerStats.instance.InvokeStatsChanged();
                InvokeStatsChanged();
                return true;
            }
        }
        return false;
    }

    public bool FamilyMemberDead(int familyMember)
    {
        //Getter to check if a family members is dead
        if (_family[familyMember] == 0) return true;
        return false;
    }

    private void OnDestroy()
    {
        OnStatsChanged -= OnStatsChanged;
    }
}
