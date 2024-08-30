using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = null;

    private const string GAME_SCENE = "SampleScene";
    private const string BETWEEN_WAVES_MENU_SCENE = "BetweenWavesMenu";
    private const string MENU_SCORE_SCENE = "MenuScore";
    private void FixedUpdate()
    {
        if (GameStats.instance == null) return;
        //Update game wave timer
        GameStats.instance._currentTime -= Time.deltaTime;

        //If timer is under 0 load menu screen
        if (GameStats.instance._currentTime <= 0)
        {
            FamilyFood.instance.EndRoundFood();
            SceneManager.UnloadSceneAsync(GAME_SCENE);
            SceneManager.LoadScene(BETWEEN_WAVES_MENU_SCENE, LoadSceneMode.Additive);
        }

        //If player is killd load end screen.
        if (_player == null)
            TriggerGameOver();
    }

    void TriggerGameOver()
    {
        //Show game over screen with score
        SceneManager.LoadScene(MENU_SCORE_SCENE);
    }
}



