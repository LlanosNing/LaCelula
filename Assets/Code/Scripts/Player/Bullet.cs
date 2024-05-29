using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public int bulletDamage;
    private float bulletDestroy = 0.63f;

    public Rigidbody2D rb;
    //private EnemyDamage _eD;

    // Start is called before the first frame update
    void Start()
    {
        //_eD = GameObject.Find("Boss").GetComponent<EnemyDamage>(); 

        //movimiento de la bala
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, bulletDestroy);
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Boss"))
    //    {
    //        _eD.EnemyHealth = _eD.EnemyHealth - bulletDamage;
    //    }
    //}
    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
