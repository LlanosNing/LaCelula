using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _theSR;
    public bool seeLeft = true;


    //variables movimiento
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    //variables dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    //variables doble salto
    private bool _canDoubleJump;
    public float doubleJumpingPower = 12f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        _theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //devolver el valor si isDashing es true. Esto previene al jugador de moverse, saltar o girarse mientras hace el dash
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        //desactivar el doble salto cuando el boton no esta pulsado
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            _canDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || _canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                _canDoubleJump = !_canDoubleJump;
            }
        }

        //activar la tecla que inicia el dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        //llamada al metodo que gira el sprite
        Flip();

        //Girar el Sprite del Jugador según su dirección de movimiento(velocidad)
        //Si el jugador se mueve hacia la izquierda
        if (rb.velocity.x < 0)
        {
            //No cambiamos la dirección del sprite
            _theSR.flipX = true;
            //El jugador mira a la izquierda
            seeLeft = false;
        }
        //Si el jugador se mueve hacia la derecha
        else if (rb.velocity.x > 0)
        {
            //Cambiamos la dirección del sprite
            _theSR.flipX = false;
            //El jugador mira a la derecha
            seeLeft = true;
        }
    }

    private void FixedUpdate()
    {
        //devolver el valor si isDashing es true. Esto previene al jugador de moverse, saltar o girarse mientras hace el dash
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //metodo que gira el sprite
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    //corrutina para el dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //hacer que la gravedad no afecte al jugador mientras hace el dash
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;


        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); //transform.localScale indica la direccion en la que el jugador esta mirando
        

        //tr.emitting para activar el emisor del trailrenderer
        tr.emitting = true;
        //parar al jugador despues de hacer un dash durante un tiempo
        yield return new WaitForSeconds(dashingTime);

      
        tr.emitting = false; //parar la emision del trailrenderer
        rb.gravityScale = originalGravity; //devolver la gravedad al punto original
        isDashing = false;

        //cooldown del dash
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
