using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isStatic, isLinear;

    public Rigidbody2D rb;
    public GameObject bullet;
    public GameObject package;

    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;
    public float moveTime, waitTime;
    private float _moveConunt, _waitCount;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        leftPoint.parent = null;
        rightPoint.parent = null;
        _moveConunt = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLinear)
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
        
    }

    //Método que detecta cuando un objeto se mete dentro del trigger de la nave enemiga
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
