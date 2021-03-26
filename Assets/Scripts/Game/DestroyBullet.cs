using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyCall", 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Bullet is collision with :  {collision.gameObject} ");
        if (collision.collider.CompareTag(GameConstants.ENEMY_TAG))
        {
            Destroy(this.gameObject);
        }
    }


    private void DestroyCall()
    {
        Destroy(this.gameObject);
    }

}


