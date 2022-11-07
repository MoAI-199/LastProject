using System;
using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using Unity.VisualScripting;
using UnityEngine;

public class CharaAnimation : MonoBehaviour {

    private const string ANIMETION_NAME_RUN = "Run";

    Animator animator;
    Parameter character_parameter;

    private void Awake( ) {
        animator = GetComponent<Animator>( );
        character_parameter = GetComponentInParent<CharacterBase>( ).getParameter( );
    }

    private void Start( ) {
       
    }

    private void Update( ) {
        Vector2 lscale = gameObject.transform.localScale;
        if( ( lscale.x > 0 && character_parameter.force.x < 0 )
            || ( lscale.x < 0 && character_parameter.force.x > 0 ) ) {
            lscale.x *= -1;
            gameObject.transform.localScale = lscale;
        }

        animator.SetBool( ANIMETION_NAME_RUN, character_parameter.is_moveing ); //動いている時にRunアニメーションをONにする
    }
}
