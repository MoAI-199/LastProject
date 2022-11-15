using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveEnemy : MoveCommonBase {

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
        _pearent.transform.RotateAround( new Vector3( 0, 0, 0 ), Vector3.forward, 1.0f );
        _pearent.transform.localRotation = Quaternion.Euler( 0, 0, _pearent.transform.rotation.z );
    }
}
