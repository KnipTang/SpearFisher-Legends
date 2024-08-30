using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_MenuScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textTotalScore;

    [SerializeField]
    private int _scoreForFamilyMember = 100;

    void Start()
    {
        if (_textTotalScore == null) return;
        //Loop over all the family members, for every member that is still alive add x amount of points to the total score.
        for (int i = 0; i < FamilyFood.instance._family.Length; i++)
        {
            //Check if the family member is dead, If not add to the score
            if (!FamilyFood.instance.FamilyMemberDead(i)) PlayerStats.instance._score += _scoreForFamilyMember;
        }
        //Display score in gameOver menu screen
        _textTotalScore.text = $"Total score: {PlayerStats.instance._score}";
    }
}
