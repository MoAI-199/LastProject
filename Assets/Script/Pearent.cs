using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearent : MonoBehaviour {
    private const float SPEED = 0.05f; // 移動速度
    private const float MASS = 100.0f; // 質量

    Rigidbody2D rigid_body;

    private void Awake( ) {
        rigid_body = GetComponent<Rigidbody2D>();
    }
    void Start( ) {
        rigid_body.mass = MASS;
    }

    void Update( ) {

    }

    public float getSpeed( ){
        return SPEED;
    }
}
