using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _waveSpawner = null;

    private const string DONT_DESTROY_ON_LOAD_CUSTOM_SCENE = "DontDestroyOnLoadCustom";
    private const string GAME_SCENE = "SampleScene";
    private const string MENU_SCORE_SCENE = "MenuScore";
    private void Awake()
    {
        //Load custom DontDestroyOnLoadCustom scene of it doesnt exist yet
        Scene dontDestroyScene = SceneManager.GetSceneByName(DONT_DESTROY_ON_LOAD_CUSTOM_SCENE);
        if (!dontDestroyScene.isLoaded)
            SceneManager.LoadScene(DONT_DESTROY_ON_LOAD_CUSTOM_SCENE, LoadSceneMode.Additive);
    }
    private void Start()
    {
        //Function gets called at beginning of every wave
        NextRound();
    }

    private void NextRound()
    {
        //Current wave time is set to start time to reset the timer.
        GameStats.instance._currentTime = GameStats.instance._startTime;

        //Set currect amount of fish/enemies that need to be spawned for current wave
        if (GameStats.instance != null)
        {
            int nrOfFish = 0;
            int nrOfEnemies = 0;
            //Check what the current wave is
            switch (GameStats.instance._nrOfWave)
            {
                case 0:
                    nrOfFish = 5;
                    nrOfEnemies = 5;
                    break;
                case 1:
                    nrOfFish = 7;
                    nrOfEnemies = 5;
                    break;
                case 2:
                    nrOfFish = 10;
                    nrOfEnemies = 5;
                    break;
                case 3:
                    nrOfFish = 10;
                    nrOfEnemies = 5;
                    break;
                //If the current wave is the last one load End menu screen
                case 4:
                    SceneManager.LoadScene(MENU_SCORE_SCENE);
                    break;
                default:
                    return; //Return for wave doesnt exist
            }

            GameStats.instance._nrOfFish = nrOfFish;
            GameStats.instance._nrOfEnemies = nrOfEnemies;

            //Call InvokeStatsChanged to update the global game stats after they have been changed.
            GameStats.instance.InvokeStatsChanged();
            //Spawn the wave spawner after the right amount of enemies/fish have been specified that need to be spawned.
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        if(_waveSpawner == null) return;
        //Check if the current wave has a prefab attached to it.
        GameObject waveSpawnerPrefab = _waveSpawner[GameStats.instance._nrOfWave];
        if (waveSpawnerPrefab == null) return;
        //if there is a prefab for the current wave set the game scene as the active one to insure that the wave spawner object gets created in the game scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GAME_SCENE));
        //create the wave spawner object
        Instantiate(waveSpawnerPrefab);
    }
}
