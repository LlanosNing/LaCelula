using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    public Transform targetPlayer;
    public float minHeight, maxHeight;
    public Transform farBackground, middleBackground;
    private Vector2 _lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar el juego la última posición del jugador será la actual
        _lastPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(targetPlayer.position.x, Mathf.Clamp(targetPlayer.position.y, minHeight, maxHeight), transform.position.z);

        //Referencia que me permite conocer cuanto hay que moverse en X e Y
        Vector2 _amountToMove = new Vector2(transform.position.x - _lastPos.x, transform.position.y - _lastPos.y);

        //Como el fondo del cielo se mueve a la misma velocidad que el jugador, le decimos que se mueva lo mismo que este
        //farBackground.position = farBackground.position + new Vector3(_amountToMoveX, 0f, 0f);
        farBackground.position = farBackground.position + new Vector3(_amountToMove.x, _amountToMove.y, 0f);
        //El fondo de las nubes se va a mover sin embargo a la mita de velocidad que lleve el jugador, luego se moverá la mitad
        //middleBackground.position += new Vector3(_amountToMoveX * .5f, 0f, 0f);
        middleBackground.position += new Vector3(_amountToMove.x, _amountToMove.y, 0f) * .5f;

        //Actualizamos la posición del jugador
        //_lastXPos = transform.position.x;
        _lastPos = transform.position;
    }
}

