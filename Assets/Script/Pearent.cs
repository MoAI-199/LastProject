using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearent : MonoBehaviour {
    private float SPEED = 0.1f; //スピードは共通にする（キャラごと設定しない）
    void Start( ) {

    }

    void Update( ) {

    }

    public float getSpeed( ){
        return SPEED;
    }
}
