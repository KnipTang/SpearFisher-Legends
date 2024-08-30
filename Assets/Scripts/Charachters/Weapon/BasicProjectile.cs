using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    private const string KILL_METHOD = "Kill";
    [SerializeField]
    private float _speed = 30.0f;
    [SerializeField]
    private float _lifeTime = 10.0f;

    [SerializeField]
    private int _damage = 5;

    private void Awake()
    {
        //Decrease ammo
        if (PlayerStats.instance == null) return;
        PlayerStats.instance._ammo--;

        GameStats.instance.InvokeStatsChanged();
        //Destroy object after lifetime seconds no mather what happends
        Invoke(KILL_METHOD, _lifeTime);
    }

    void FixedUpdate()
    {
        //Keep moving until Bullet Object hits wall
        if (!WallDetection())
            transform.position += transform.forward * Time.deltaTime * _speed;
    }


    static readonly string[] RAYCAST_MASK = { "Ground", "StaticLevel" };
    //Delete object when object with layer "Ground" is hit
    bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay,
            Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }
        return false;
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    const string ENEMY_TAG = "Enemy";
    //if bullet hits a object
    void OnTriggerEnter(Collider other)
    {
        //make sure we only hit a enemy otherwise just continue
        if (other.tag == ENEMY_TAG)
        { 
            Health otherHealth = other.GetComponent<Health>();
            if (otherHealth != null)
            {
                otherHealth.Damage(_damage);
            }

            Kill();
        }
    }
}

