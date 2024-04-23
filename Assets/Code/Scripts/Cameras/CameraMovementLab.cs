using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovementLab : MonoBehaviour
{
    public Transform targetPlayer;
    public float minHeight, maxHeight;
    public Transform backgroud;
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

        backgroud.position += new Vector3(_amountToMove.x, _amountToMove.y/2, 0f) * .5f;

        //Actualizamos la posición del jugador
        _lastPos = transform.position;
    }
}

