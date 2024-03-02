using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //movimiento de la bala
        rb.velocity = transform.right * bulletSpeed;
    }

}
