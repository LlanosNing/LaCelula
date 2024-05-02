using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES   
    private SpriteRenderer _theSR;
    public bool seeLeft = true;

    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public float bounceForce;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    private bool _canDoubleJump;
    public float doubleJumpingPower = 12f;

    [SerializeField] private float coyoteTime;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

   
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
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else//dejar que el contador se baje quitando time.deltatime
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
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

        //salto del jugador
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0 || _canDoubleJump)
            {
                AudioManager.audioMReference.PlaySFX(3);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                jumpBufferCounter = 0f;

                _canDoubleJump = !_canDoubleJump;
            }
        }

        //hace que al solo pulsar el boton en vez de mantener salte menos 
        //(si sueltas el boton y el jugador todavia se mueve hacia arriba, su velocidad se multiplica *0.5)
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);

            //desactivar el coyotetime cuando soltamos el boton
            coyoteTimeCounter = 0f; //esto previene el doble salto al spamear el boton
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();

        if (rb.velocity.x < 0)
        {
            _theSR.flipX = false;
            seeLeft = true;
        }
        else if (rb.velocity.x > 0)
        {
            _theSR.flipX = true;
            seeLeft = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            transform.parent = collision.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            transform.parent = null;
    }

    private void FixedUpdate()
    {
        //Previene al jugador de moverse, saltar o girarse mientras hace el dash
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //hacer que la gravedad no afecte al jugador mientras hace el dash
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        AudioManager.audioMReference.PlaySFX(4);
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); //transform.localScale indica la direccion en la que el jugador esta mirando
        
        tr.emitting = true;
        //parar al jugador despues de hacer un dash durante un tiempo
        yield return new WaitForSeconds(dashingTime);

      
        tr.emitting = false;
        rb.gravityScale = originalGravity;
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
