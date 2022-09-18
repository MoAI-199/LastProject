using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer2 : MoveCommonBase {
    private Pearent _pearent;

    void Start( ) {
        _pearent = GetComponent<Pearent>( );
    }

    void Update( ) {
        move( );

    }
    public void move( ) {
        Vector2 position = transform.position;
        float speed = _pearent.getSpeed( );
        setMoving( false );
        if( Input.GetKey( KeyCode.LeftArrow ) ) {
            position.x -= speed;
            setMoving( true );
        }
        if( Input.GetKey( KeyCode.RightArrow ) ) {
            position.x += speed;
            setMoving( true );
        }
        if( Input.GetKey( KeyCode.UpArrow ) ) {
            position.y += speed;
            setMoving( true );
        }
        if( Input.GetKey( KeyCode.DownArrow ) ) {
            position.y -= speed;
            setMoving( true );
        }

        transform.position = position;
    }
}
