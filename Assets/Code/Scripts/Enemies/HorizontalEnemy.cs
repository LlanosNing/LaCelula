using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : MonoBehaviour
{
    public bool canPackage;

    public Rigidbody2D rb;
    public GameObject bullet;
    public GameObject package;

    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;
    public float moveTime, waitTime;
    private float _moveConunt, _waitCount;
    public bool canMove = true;

    void Start()
    {
        leftPoint.parent = null;
        rightPoint.parent = null;
        _moveConunt = moveTime;
    }


    void Update()
    {
        if (canMove == true)
        {
            if (_moveConunt > 0)
            {
                _moveConunt -= Time.deltaTime;
                if (movingRight)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                    if (transform.position.x > rightPoint.position.x)
                        movingRight = false;
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                    if (transform.position.x < leftPoint.position.x)
                        movingRight = true;
                }
                if (_moveConunt <= 0)
                    _waitCount = Random.Range(waitTime + .25f, waitTime + 1.25F);
            }

            else if (_waitCount > 0)
            {
                _waitCount -= Time.deltaTime;
                rb.velocity = new Vector2(0f, rb.velocity.y);
                if (_waitCount <= 0)
                {
                    _moveConunt = moveTime;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && canPackage)
        {
            //Destruimos la bala
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(package, transform.position, package.transform.rotation);
        }
    }
}
