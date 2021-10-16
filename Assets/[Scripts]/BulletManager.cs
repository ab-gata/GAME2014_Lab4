using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    // Enemy Bullets
    public Queue<GameObject> enemyBulletPool;
    public int enemyBulletCount;

    // Player Bullets
    public Queue<GameObject> playerBulletPool;
    public int playerBulletCount;

    // Reference
    private BulletFactory factory;

    // Start is called before the first frame update
    void Start()
    {
        // Bullet Pools
        enemyBulletPool = new Queue<GameObject>(); // create empty Queues
        playerBulletPool = new Queue<GameObject>();

        // Reference
        factory = GetComponent<BulletFactory>();
    }

    private void AddBullet(BulletTypes type = BulletTypes.ENEMY) // Create bullets and add to respective queue
    {
        var temp_bullet = factory.createBullet(type);

        switch (type)
        {
            case BulletTypes.ENEMY:
                enemyBulletPool.Enqueue(temp_bullet);
                enemyBulletCount++;
                break;
            case BulletTypes.PLAYER:
                playerBulletPool.Enqueue(temp_bullet);
                playerBulletCount++;
                break;
            default:
                break;
        }
    }

    // <summary>
    // This method removes a bullet prefab from the bullet pool
    // and returns a reference to it.
    // </summary>
    // <param name="spawnPosition"></param>
    // <returns></returns>
    public GameObject GetBullet(Vector2 spawnPosition, BulletTypes type = BulletTypes.ENEMY)
    {
        GameObject temp_bullet = null;

        switch (type)
        {
            case BulletTypes.ENEMY:
                if (enemyBulletPool.Count < 1)
                {
                    AddBullet(type);
                }
                temp_bullet = enemyBulletPool.Dequeue();
                break;
            case BulletTypes.PLAYER:
                if (playerBulletPool.Count < 1)
                {
                    AddBullet(type);
                }
                temp_bullet = playerBulletPool.Dequeue();
                break;
            default:
                break;
        }

        temp_bullet.transform.position = spawnPosition;
        temp_bullet.SetActive(true);
        return temp_bullet;
    }

    // <summary>
    // This method returns a bullet back into the bullet pool
    // </summary>
    // <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet, BulletTypes type = BulletTypes.ENEMY)
    {
        returnedBullet.SetActive(false);

        switch (type)
        {
            case BulletTypes.ENEMY:
                enemyBulletPool.Enqueue(returnedBullet);
                break;
            case BulletTypes.PLAYER:
                playerBulletPool.Enqueue(returnedBullet);
                break;
            default:
                break;
        }
    }
}
