using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES   
    private SpriteRenderer _theSR;
    public bool seeLeft = true;


    //variables movimiento
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public float bounceForce;

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

    //Variable para la fuerza del KnockBack
    public float knockBackForce;
    //Variables para controlar el contador de tiempo de Knocback
    public float knockBackLength; //Variable que nos sirve para rellenar el contador
    private float _knockBackCounter; //Contador de tiempo
    #endregion

    #region UNITY METHODS
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
            _theSR.flipX = false;
            seeLeft = true;
        }
        //Si el jugador se mueve hacia la derecha
        else if (rb.velocity.x > 0)
        {
            _theSR.flipX = true;
            seeLeft = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el que colisiona contra el jugador es una plataforma
        if (collision.gameObject.CompareTag("Platform"))
            transform.parent = collision.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el objeto con el que dejamos de colisionar es una plataforma
        if (collision.gameObject.CompareTag("Platform"))
            transform.parent = null;
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

    #endregion

    #region OWN METHODS 
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

    public void Knockback()
    {
        //Inicializamos el contador de KnockBack
        _knockBackCounter = knockBackLength;
        //Paralizamos al jugador en X y hacemos que salte en Y
        rb.velocity = new Vector2(0f, knockBackForce);
    }

    //Método para que el jugador rebote
    public void Bounce(float bounceForce)
    {
        //Impulsamos al jugador rebotando
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }
    #endregion
}
