using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Velocidad del jugador
    public float moveSpeed;
    //El rigidbody del jugador
    //Barrabaja indica que la variable es privada
    private Rigidbody2D _theRB;
    //Fuerza de salto del jugador
    public float jumpForce;


    //Variable para saber si el jugador est� en el suelo
    private bool _isGrounded;
    //Referencia al punto por debajo del jugador que tomamos para detectar el suelo
    public Transform groundCheckPoint;
    //Referencia para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //variable para saber si podemos hacer un doble salto
    private bool _canDoubleJump;

    private void Start()
    {
        //Inicializamos el Rigidbody del jugador
        //GetComponent => Va al objeto donde est� metido este c�digo y busca el componente indicado
        _theRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //El jugador se mueve a una velocidad dada en X, y la velocidad que ya tuviera en Y
        _theRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _theRB.velocity.y);

        //La variable isGrounded se har� true siempre que el c�rculo f�sico que hemos creado detecte suelo, sino ser� falsa
        //OverlapCircle(punto donde se genera el c�rculo, radio del c�rculo, layer a detectar)
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        //Si pulsamos el bot�n de salto
        if (Input.GetButtonDown("Jump"))
        {
            //Si el jugador est� en el suelo
            if (_isGrounded)
            {
                //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                _theRB.velocity = new Vector2(_theRB.velocity.x, jumpForce);
                //una vez en el suelo, reactivamos la posibilidad de doble salto
                _canDoubleJump = true;
            }
            //si el jugador no esta en el suelo
            else
            {
                //si canDoubleJumo es verdadera
                if (_canDoubleJump)
                {
                    //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                    _theRB.velocity = new Vector2(_theRB.velocity.x, jumpForce);
                    //hacemos que no se pueda volver a saltar de nuevo
                    _canDoubleJump = false;
                }
            }
        }
    }
}   