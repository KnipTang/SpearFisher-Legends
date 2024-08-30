using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttons = null;
    private Color[] _memberColor = null;

    private TextMeshProUGUI[] _buttonText = null;
    private Image[] _image = null;

    [SerializeField]
    private Image[] _imageHealthBar = null;

    private void Start()
    {
        if (FamilyFood.instance == null) return;

        //Change color if any of the family members stats changes
        // initialize
        ChangeColor(new int[FamilyFood.instance._amountOfMembers]);
        // hook to monitor changes
        FamilyFood.instance.OnStatsChanged += ChangeColor;
    }

    private void ChangeColor(int[] family)
    {
        //Change button/health bar color of family members to indicate "food" points 
        if (_imageHealthBar == null || _buttons == null) return;

        _memberColor = new Color[_buttons.Length];
        _buttonText = new TextMeshProUGUI[_buttons.Length];
        _image = new Image[_buttons.Length];

        //Loop through all the family members buttons
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (_imageHealthBar[i] == null || FamilyFood.instance == null) return;

            if (i < FamilyFood.instance._family.Length)
            {
                //Change health bar
                _imageHealthBar[i].fillAmount = (float)FamilyFood.instance._family[i] / 100.0f;

                //Normalize food points so it can be used to indicate color
                float normalizedFoodPoints = (float)FamilyFood.instance._family[i] / 100.0f;

                //change color from red to green regarding the family members food points
                _memberColor[i] = Color.Lerp(Color.red, Color.green, normalizedFoodPoints);
                _buttonText[i] = _buttons[i].GetComponentInChildren<TextMeshProUGUI>();

                //Check if family member is dead
                if(FamilyFood.instance.FamilyMemberDead(i))
                {
                    //If family member is dead change text/color to indicate this
                    _image[i] = _buttons[i].GetComponentInChildren<Image>();
                    _image[i].color = _memberColor[i];
                    _buttonText[i].color = Color.black;
                    _buttonText[i].text = "DEAD";
                }
                //If family member is alife indicate color regarding there food points
                else
                    _buttonText[i].color = _memberColor[i];
            }
        }
    }
}
