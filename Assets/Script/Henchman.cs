using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Henchman : MonoBehaviour {
    private const float SPEED = 50.0f; //���l��������ƍő呬�x��������
    private GameObject _parent_go = null;
    private Transform _transform = null;
    private Transform _transform_parent = null;
    void Start( ) {
        _transform = this.transform;
    }

    void Update( ) {
        move( );
    }

    private void move( ) {
        float distance = Vector2.Distance(_transform.position, _transform_parent.position );
        if ( distance < 0.5f ) {//�_�~�[�̃T�C�Y�Őe�Ɣ��Ȃ��悤�ɂ���B
            return;
        }
       _transform.position = Vector2.Lerp( _transform.position, _transform_parent.position, distance / SPEED );
    }
    public void setingParent( GameObject parent ) {
        _parent_go = parent;
        _transform_parent = parent.transform;
    }
}
