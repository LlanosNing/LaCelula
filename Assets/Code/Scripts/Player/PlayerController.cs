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
    public bool canMove = true;
    private bool _isGrounded;

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

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    private Animator _anim;
    public Sprite thePlayerSprite;

    public bool canInteract = false;

   
    public float knockBackForce;
    //Variables para controlar el contador de tiempo de Knocback
    public float knockBackLength; //Variable que nos sirve para rellenar el contador
    private float _knockBackCounter; //Contador de tiempo
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _theSR = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
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

        if (canMove == true)
        {
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
                    _rb.velocity = new Vector2(_rb.velocity.x, jumpingPower);

                    jumpBufferCounter = 0f;

                    _canDoubleJump = !_canDoubleJump;
                }
            }

            if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * .5f);

                //desactivar el coyotetime cuando soltamos el boton
                coyoteTimeCounter = 0f; //esto previene el doble salto al spamear el boton
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash") && canDash)
            {
                StartCoroutine(Dash());
            }

                Flip();

            if (_rb.velocity.x < 0)
            {
                _theSR.flipX = false;
                seeLeft = true;
            }
            else if (_rb.velocity.x > 0)
            {
                _theSR.flipX = true;
                seeLeft = false;
            }
        }

        _anim.SetFloat("moveSpeed", Mathf.Abs(_rb.velocity.x));//Mathf.Abs hace que un valor negativo sea positivo, lo que nos permite que al movernos a la izquierda también se anime esta acción
        //Cambiamos el valor del parámetro del Animator "isGrounded", dependiendo del valor de la booleana del código "_isGrounded"
        _anim.SetBool("isGrounded", _isGrounded);
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

        _rb.velocity = new Vector2(horizontal * speed, _rb.velocity.y);
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
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        AudioManager.audioMReference.PlaySFX(4);
        _rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); //transform.localScale indica la direccion en la que el jugador esta mirando
        
        tr.emitting = true;
        //parar al jugador despues de hacer un dash durante un tiempo
        yield return new WaitForSeconds(dashingTime);

      
        tr.emitting = false;
        _rb.gravityScale = originalGravity;
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
        _rb.velocity = new Vector2(0f, knockBackForce);
    }

    //Método para que el jugador rebote
    public void Bounce(float bounceForce)
    {
        //Impulsamos al jugador rebotando
        _rb.velocity = new Vector2(_rb.velocity.x, bounceForce);
    }
    
    #endregion
}
