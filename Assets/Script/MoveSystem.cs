using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    private Parent _pearent;
    
    private enum MOVE_TYPE
    {
        PLAYER1,
        PLAYER2
    }

    private MOVE_TYPE moveTipe;

    private Action[] ActionType;

    // Start is called before the first frame update
    void Start( )
    {
        moveTipe = MOVE_TYPE.PLAYER1;
        _pearent = GetComponent<Parent>( );

        ActionType = new Action[ ]
        {
            MovingPlayer1,
            MovingPlayer2
        };
    }

    // Update is called once per frame
    void Update()
    {
        var fanc = ActionType[(int)moveTipe];
        fanc.Invoke();

    }

    private void MovingPlayer1()
    {
        Vector2 position = transform.position;
        float speed = _pearent.getSpeed( );
        if( Input.GetKey( KeyCode.A ) ) 
        {
            position.x -= speed;
        }
        if( Input.GetKey( KeyCode.D ) )
        {
            position.x += speed;
        }
        if( Input.GetKey( KeyCode.W ) ) 
        {
            position.y += speed;
        }
        if( Input.GetKey( KeyCode.S ) ) 
        {
            position.y -= speed;
        }

        transform.position = position;
    }

    private void MovingPlayer2()
    {
        Vector2 position = transform.position;
        float speed = _pearent.getSpeed( );
        if( Input.GetKey( "left" ) ) {
            position.x -= speed;
        }
        if( Input.GetKey( "right" ) ) {
            position.x += speed;
        }
        if( Input.GetKey( "up" ) ) {
            position.y += speed;
        }
        if( Input.GetKey( "down" ) ) {
            position.y -= speed;
        }

        transform.position = position;
    }
}
