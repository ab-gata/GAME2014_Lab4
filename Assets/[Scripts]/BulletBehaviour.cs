using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet Movement")]
    [Range(0.0f, 0.5f)]
    public float speed;
    public Bounds bulletBounds;
    public BulletDirection direction;

    private BulletManager bulletManager;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindObjectOfType<BulletManager>();

        switch (direction)
        {
            case BulletDirection.UP:
                velocity = new Vector3(0.0f, speed, 0.0f);
                break;
            case BulletDirection.DOWN:
                velocity = new Vector3(0.0f, -speed, 0.0f);
                break;
            case BulletDirection.RIGHT:
                velocity = new Vector3(speed, 0.0f, 0.0f);
                break;
            case BulletDirection.LEFT:
                velocity = new Vector3(-speed, 0.0f, 0.0f);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += velocity;
    }

    private void CheckBounds() // top bound, lower bound
    {
        if (transform.position.y < bulletBounds.max) // Player side of screen = Enemy bullet
        {
            bulletManager.ReturnBullet(this.gameObject);
        }
        else if (transform.position.y > bulletBounds.min) // Enemy side of screen = Player bullet
        {
            bulletManager.ReturnBullet(this.gameObject, BulletTypes.PLAYER);
        }
    }
}
