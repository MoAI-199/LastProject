using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer1 : MoveCommonBase {

    private Parent _pearent;
    protected override void setup( ) {
        _pearent = GetComponent<Parent>( );
    }

    protected override void update( ) {
        move( );
    }

    private void move( ){
        Vector2 position = transform.position;
        float speed = _pearent.getParemeter( ).speed;
        setMoving( false );
        if( Input.GetKey( KeyCode.A ) ) {
            position.x -= speed;
            setMoving( true );
        }
        if( Input.GetKey( KeyCode.D ) ) {
            position.x += speed;
            setMoving( true );
        }
        if( Input.GetKey( KeyCode.W ) ) {
            position.y += speed;
            setMoving( true );
        }
        if( Input.GetKey( KeyCode.S ) ) {
            position.y -= speed;
            setMoving( true );
        }

        transform.position = position;
    }
}
