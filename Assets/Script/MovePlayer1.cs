using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer1 : MonoBehaviour {

    private Pearent _pearent;
    void Start( ) {
        _pearent = GetComponent<Pearent>( );
    }

    void Update( ) {
        Vector2 position = transform.position;
        float speed = _pearent.getSpeed( );
        if( Input.GetKey( KeyCode.A ) ) {
            position.x -= speed;
        } 
        if( Input.GetKey( KeyCode.D ) ) {
            position.x += speed;
        }
        if( Input.GetKey( KeyCode.W ) ) {
            position.y += speed;
        }
        if( Input.GetKey( KeyCode.S ) ) {
            position.y -= speed;
        }

        transform.position = position;
    }
}
