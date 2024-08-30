using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Ignore ground and groundFish layer so the fish don't collide with the ground
        Physics.IgnoreLayerCollision(6, 9);
        Physics.IgnoreLayerCollision(10, 9);
    }

}
