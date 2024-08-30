using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextWave : MonoBehaviour
{
    [SerializeField]
    private int _minAmountOfAmmo = 15;
    [SerializeField]
    private Canvas _ammoCanvasIndicator = null;
    [SerializeField]
    private int _lastWave = 4;

    private const string GAME_SCENE_NAME = "SampleScene";
    private const string BETWEEN_WAVES_MENU_SCENE_NAME = "BetweenWavesMenu";
    private const string MENU_SCORE_SCENE = "MenuScore";
    public void OnClick()
    {
        if (GameStats.instance == null || PlayerStats.instance == null) return;

        //Check if the player has enough ammo to kill all enemies for the next round if they do load scene
        if (PlayerStats.instance._ammo >= _minAmountOfAmmo)
        {
            //Load game / unload menu
            WaveScene();
        }
        //If they player doesnt have enough ammo show a indicator warning the player of this fact and if they're sure that they want to start the round
        else
        {
            //Enable indicator
            CancelButton();
        }
    }

    public void WaveScene()
    {
        if (GameStats.instance == null || PlayerStats.instance == null) return;

        //Increase current wave
        GameStats.instance._nrOfWave++;

        //Check if current coming wave is the last wave
        if(GameStats.instance._nrOfWave >= _lastWave)
            //If its the last wave show end scene
            SceneManager.LoadScene(MENU_SCORE_SCENE);
        //If not start the next round
        else
        {
            //Load scene for next wave
            SceneManager.LoadScene(GAME_SCENE_NAME, LoadSceneMode.Additive);
            //Unload menu screen
            SceneManager.UnloadSceneAsync(BETWEEN_WAVES_MENU_SCENE_NAME);
        }
    }

    public void CancelButton()
    {
        //If player clicks the cancel button on the low ammo canvas indicator disable the indicator so they can buy more ammo in the menu screen
        if (_ammoCanvasIndicator != null)
            _ammoCanvasIndicator.enabled = !_ammoCanvasIndicator.enabled;
    }
}
