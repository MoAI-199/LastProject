using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour {
    private float SPEED = 0.05f; //�X�s�[�h�͋��ʂɂ���i�L�������Ɛݒ肵�Ȃ��j

    private Rigidbody2D _rigidbody2;
    void Start( ) {
        _rigidbody2 = GetComponent<Rigidbody2D>();
    }

    void Update( ) {

    }

    public float getSpeed( ){
        return SPEED;
    }

    public bool isMoving( )
    {
        
        if( _rigidbody2.IsSleeping( ) ) {
            return false;
        }
        return true;
    }
}
