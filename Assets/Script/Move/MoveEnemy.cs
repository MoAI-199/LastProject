using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveEnemy : MoveCommonBase {
    enum PATTERN {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    private Parent _pearent;

    void Start( ) {
        _pearent = GetComponent<Parent>( );
    }

    protected override void setup( ) {
        _pearent = GetComponent<Parent>( );
    }
    protected override void update( ) {
        move( );
    }

    private void move( ) {
        setMoving( true );
        int rand = UnityEngine.Random.Range( 0, 4 );
        doMove( ( MOVE_TYPE )rand );
    }
}
