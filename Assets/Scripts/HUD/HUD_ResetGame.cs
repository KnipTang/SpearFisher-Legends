using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_ResetGame : MonoBehaviour
{
    public static HUD_ResetGame instance;

    private const string GAME_SCENE_NAME = "SampleScene";

    private void Awake()
    {
        //Ensure only one instance of this class exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Delete player so the game resets
    public void ResetGame()
    {
        //Calling this function will load the game and deleting the player triggering the game to reset
        SceneManager.LoadScene(GAME_SCENE_NAME);

        //Find the player with the specified tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Check if the player object exists before attempting to destroy it
        if (player != null)
        {
            Destroy(player);
        }
    }
}
