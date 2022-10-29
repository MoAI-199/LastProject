using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveEnemy : MoveCommonBase {

    private Parent _pearent;
    private float _sin_x = 0;
    private float _sin_y = 0;
    void Start( ) {
        _pearent = GetComponent<Parent>( );
    }

    void Update( ) {
        move( );
    }

    private void move( ) {
        _pearent.transform.RotateAround( new Vector3(0,0,0), Vector3.forward, 1.0f);
    }
}
