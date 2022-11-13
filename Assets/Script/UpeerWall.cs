using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpeerWall : MonoBehaviour {
    void Start( ) {

    }

    void Update( ) {

    }

    private void OnCollisionEnter( Collision collision ) {
        float target_y = collision.gameObject.transform.position.y;
        float wall_y = gameObject.transform.position.y;
        if( target_y > wall_y ){
        }
    }
}
