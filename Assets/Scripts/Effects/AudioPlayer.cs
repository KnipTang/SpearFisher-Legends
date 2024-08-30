using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;
    public bool _fishKilledSound = false;
    public bool _enemyKilledSound = false;

    [SerializeField]
    private AudioSource _audioFish;
    [SerializeField]
    private AudioSource _audioEnemy;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void Sound()
    {
        //Play sound of killed object
        if(_fishKilledSound)
        {
            _fishKilledSound = false;

            if (_audioFish != null)
            {
                _audioFish.Play();
            }
        }
        if (_enemyKilledSound)
        {
            _enemyKilledSound = false;

            if (_audioEnemy != null)
            {
                _audioEnemy.Play();
            }
        }
    }
}
