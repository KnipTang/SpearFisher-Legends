using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectToSpawn = null;
    [SerializeField]
    private GameObject _objectGround = null;
    private Transform _ground = null;
    [SerializeField]
    private int _maxInstances = 5;
    [SerializeField]
    private float _radiusPlayer = 5;

    private List<GameObject>[] _spawnedObjects = null;

    private bool _objectSpawned = false;

    private const string GAME_SCENE = "SampleScene";
    private const string BETWEEN_WAVES_MENU_SCENE = "BetweenWavesMenu";

    private void Start()
    {
        if (_objectGround == null) return;
        _ground = _objectGround.transform;
        _spawnedObjects = new List<GameObject>[_objectToSpawn.Length];
        //Loop through the amount of differen objects to spawn and like each one to the spawned object list
        for (int i = 0; i < _objectToSpawn.Length; i++)
        {
            _spawnedObjects[i] = new List<GameObject>();
        }
    }

    const string ENEMY_TAG = "Enemy";
    const string FISH_TAG = "Fish";
    private void NextRound()
    {
        if (GameStats.instance == null || _spawnedObjects == null || _objectToSpawn == null) return;

        //Set objectSpawned to true to indicate that the object for this round have been spawned
        _objectSpawned = true;

        //Loop through all the objects needed to be spawned
        for (int i = 0; i < _spawnedObjects.Length; i++)
        {
            //if the needed object to spawn is a fish/enemy set maxInstance to the amound of fish/enemies needed to be spawned that round
            if (_objectToSpawn[i].tag == FISH_TAG)
                _maxInstances = GameStats.instance._nrOfFish;
            else if (_objectToSpawn[i].tag == ENEMY_TAG)
                _maxInstances = GameStats.instance._nrOfEnemies;

            //Loop over every object that needs to be spawned until enough object of that gameobject have been spawned
            while (_spawnedObjects[i].Count < _maxInstances)
            {
                SpawnObject(_objectToSpawn[i]);
            }
        }
    }

    private void Update()
    {
        if (GameStats.instance == null || FamilyFood.instance == null) return;

        //If object have not been spawned yet spawn objects
        if(!_objectSpawned)
        NextRound();

        //If all objects have been killed switch scenes
        if(GameStats.instance._nrOfEnemies == 0 && GameStats.instance._nrOfFish == 0 && !SceneManager.GetSceneByName(BETWEEN_WAVES_MENU_SCENE).IsValid())
        {
            //Call endRoundFood to decrease the food points of every family member
            FamilyFood.instance.EndRoundFood();
            //Unload game scene and load menu scene
            SceneManager.UnloadSceneAsync(GAME_SCENE);
            SceneManager.LoadScene(BETWEEN_WAVES_MENU_SCENE, LoadSceneMode.Additive);
        }
    }

    //Function to spawn objects.
    void SpawnObject(GameObject objectPrefab)
    {
        //Set gameScene as active scene to be sure that the object get created in the game scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GAME_SCENE));
        if (objectPrefab == null || _ground == null) return;
        if (GameStats.instance == null) return;
        //Get a random position on the ground
        Vector3 randomPosition = GetRandomPositionAboveGround();
        //spawn gameobject on that random position
        GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
        //Add spawned object to spawnedobjects list to indicate how many still need to be spawned
        _spawnedObjects[Array.IndexOf(_objectToSpawn, objectPrefab)].Add(newObject);
    }

    void UpdateSpawnedObjectsList()
    {
        //Remove destroyed objects from the list
        for (int i = 0; i < _spawnedObjects.Length; i++)
        {
            _spawnedObjects[i].RemoveAll(obj => obj == null);
        }
    }

    //Get random position of objects to spawn on
    Vector3 GetRandomPositionAboveGround()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (_ground == null || player == null)
            return Vector3.zero;

        float randomX, randomZ;
        //Spawn enemy/fish above the ground
        float yCoordinate = _ground.position.y + _ground.localScale.y;
        Vector3 randomPosition;

        //Ensure the random position is not within a radius of _radiusPlayer around the player
        do
        {
            randomX = UnityEngine.Random.Range(-_ground.localScale.x / 2f, _ground.localScale.x / 2f);
            randomZ = UnityEngine.Random.Range(-_ground.localScale.z / 2f, _ground.localScale.z / 2f);
            randomPosition = new Vector3(randomX, yCoordinate, randomZ);
        } while (Vector3.Distance(randomPosition, player.transform.position) < _radiusPlayer);

        return randomPosition;
    }

    private void LateUpdate()
    {
        //Update the list of spawned objects in the LateUpdate method to ensure all updates are finished.
        UpdateSpawnedObjectsList();
    }
}

