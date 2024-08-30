using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthColor : MonoBehaviour
{
    [SerializeField]
    private Material _live3 = null;
    [SerializeField]
    private Material _live2 = null;
    [SerializeField]
    private Material _live1 = null;

    [SerializeField]
    private GameObject _enemy = null;

    private float _enemyMiddleHealth;

    // Update is called once per frame
    void Update()
    {
        if (_enemy == null || _live1 == null || _live2 == null || _live3 == null) return;

        Health enemyHealth = _enemy.GetComponent<Health>();
        if (enemyHealth == null) return;

        Renderer rend = GetComponent<Renderer>();
        if (rend == null) return;

        //Rounds up health if its not a whole numbers
        _enemyMiddleHealth = Mathf.Ceil(enemyHealth.StartHealth / 2);

        //Change color of enemy when enemy gets hit
        if (enemyHealth._currentHealth > _enemyMiddleHealth)
            rend.material = _live3;
        else if (enemyHealth._currentHealth == _enemyMiddleHealth)
            rend.material = _live2;
        else if (enemyHealth._currentHealth < _enemyMiddleHealth)
            rend.material = _live1;
    }
}
