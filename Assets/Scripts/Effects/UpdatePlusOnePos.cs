using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlusOnePos : MonoBehaviour
{
    [SerializeField]
    private float _yPosStartingPoint = 0.5f;
    [SerializeField]
    private int _indicatorLifeTime = 2;
    private void Start()
    {
        //Set indicator above ground
        Vector3 plusOnePos = gameObject.transform.position;
        plusOnePos.y = _yPosStartingPoint;
        gameObject.transform.position = plusOnePos;

        //Destroy indicator in x amount of seconds after spawn
        Destroy(gameObject, _indicatorLifeTime);
    }
    void FixedUpdate()
    {
        //Move indicator to the sky
        Vector3 currentPosition = gameObject.transform.position;
        currentPosition.y += Time.deltaTime;
        gameObject.transform.position = currentPosition;
    }
}
