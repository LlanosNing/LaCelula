using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject bullet;
    public GameObject package;

    public float moveSpeed;
    public Transform topPoint, bottomPoint;
    public bool movingUp;
    public float moveTime, waitTime;
    private float _moveConunt, _waitCount;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        topPoint.parent = null;
        bottomPoint.parent = null;
        _moveConunt = moveTime;
    }


    void Update()
    {
        if (canMove == true)
        {
            if (_moveConunt > 0)
            {
                _moveConunt -= Time.deltaTime;
                if (movingUp)
                {
                    rb.velocity = new Vector2(rb.velocity.y, moveSpeed);

                    if (transform.position.x < bottomPoint.position.x)
                        movingUp = false;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.y, -moveSpeed);
                    if (transform.position.x > topPoint.position.x)
                        movingUp = true;
                }
                if (_moveConunt <= 0)
                    _waitCount = Random.Range(waitTime + .25f, waitTime + 1.25F);
            }

            else if (_waitCount > 0)
            {
                _waitCount -= Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.y, 0f);
                if (_waitCount <= 0)
                {
                    _moveConunt = moveTime;
                }
            }
        }
    }

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
