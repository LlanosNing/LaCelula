using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;
    private float bulletDestroy = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        //movimiento de la bala
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, bulletDestroy);
    }
    
    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
