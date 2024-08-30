using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private int _cubesPerAxis = 8;
    [SerializeField]
    private float _force = 300f;
    [SerializeField]
    private float _radius = 2f;
    [SerializeField]
    private float _destroyParticalTime = 1f;
    public void Explode()
    {
        //Loop through all the axes of the enemy cube
        for (int x = 0; x < _cubesPerAxis; x++)
        {
            for (int y = 0; y < _cubesPerAxis; y++)
            {
                for (int z = 0; z < _cubesPerAxis; z++)
                {
                    //Create new cubes for every axes depending on cubesPerAxis
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }
        //Destroy the enemy gameobject
        Destroy(gameObject);
    }

    void CreateCube(Vector3 coordinates)
    {
        //Create a new cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //Renderer rd = cube.GetComponent<Renderer>();
        //rd.material = GetComponent<Renderer>().material;

        //Disable shadows for performance
        //rd.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        //Transform the scale and location of the cube
        cube.transform.localScale = transform.localScale / _cubesPerAxis;
                
        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        //Take the rb and add the "Explosion" effect to it
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(_force, transform.position, _radius);

        //Destroy the box collider of every cube so it doesnt effect the gameplay
        Destroy(cube.GetComponent<BoxCollider>());

        //Destroy cube after x amound of seconds
        Destroy(cube, _destroyParticalTime);
    }
}