using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    //posicion del objetivo de la camara
    public Transform targetPlayer;
    //posicion minima y maxima de la camara
    public float minHeight, maxHeight;
    //ultima posicion del jugador en x e y
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
    }
}
