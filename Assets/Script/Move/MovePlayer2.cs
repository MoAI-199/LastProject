using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer2 : MoveCommonBase {
    private Parent _pearent;

    void Start( ) {
        _pearent = GetComponent<Parent>( );
    }

    void Update( ) {
        move( );

    }
    private void move( ) {
        Vector2 position = transform.position;
        float speed = _pearent.getParemeter( ).speed;
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
