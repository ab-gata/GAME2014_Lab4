using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Movement")] 
    public Bounds movementBounds;
    public Bounds startingRange;

    [Header("Bullets")] 
    public Transform bulletSpawn;
    public int frameDelay;

    private float startingPoint;
    private float randomSpeed;

    private BulletManager bulletManager;

    void Start()
    {
        randomSpeed = Random.Range(movementBounds.min, movementBounds.max);
        startingPoint = Random.Range(startingRange.min, startingRange.max);
        bulletManager = GameObject.FindObjectOfType<BulletManager>();

        frameDelay = 25; // how fast enemy shoots
    }

    void Update()
    {
        transform.position = new Vector2(Mathf.PingPong(Time.time, randomSpeed) + startingPoint, transform.position.y);
    }

    void FixedUpdate()
    {
        if (Time.frameCount % frameDelay == 0)
        {
            bulletManager.GetBullet(bulletSpawn.position, BulletTypes.ENEMY);
        }
    }
}
