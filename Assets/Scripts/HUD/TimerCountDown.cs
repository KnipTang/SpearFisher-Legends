using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField]
    private int _startCountDown = 5;
    [SerializeField]
    private int _timeUpTimer = 1;
    private void Update()
    {
        if (GameStats.instance == null) return;

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text == null) return;

        //If the game timer is under the countDown limit start showing the timer indicator the let the player know the wave is about to end
        if (GameStats.instance._currentTime <= _startCountDown)
        {
            if (GameStats.instance._currentTime > _timeUpTimer)
            {
                text.enabled = true;
                //Show time in whole seconds on the screen
                text.text = ((int)GameStats.instance._currentTime).ToString();
            }
            //If the timer is below timeUpTimer show TimeUp to let the player know they only have around 1 second left.
            else
                text.text = "Time up!";
        }
    }
}
