using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject bullet;
    public GameObject package;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es una bala
        if (collision.CompareTag("Bullet"))
        {
            //Destruimos la bala
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(package, transform.position, package.transform.rotation);
        }
    }
}
