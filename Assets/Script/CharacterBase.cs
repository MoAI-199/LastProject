﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBase : MonoBehaviour{
    protected FamilyManager _family_manager;
    protected Transform _transform;
    protected Rigidbody2D _rigid_body;
    protected SpriteRenderer _sprite_renderer;
    protected Parameter _parameter;

    public class Parameter {
        public float speed; //定数
        public Vector2 pos; //現在地
        public Vector2 velocity; //移動量
        public Parent my_parent;
        public bool is_moveing; //移動中か否か

    }
    private void Awake( ) {
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
        _rigid_body = GetComponent<Rigidbody2D>( );
        _sprite_renderer = GetComponent<SpriteRenderer>( );
        _parameter = new Parameter( );
        _transform = this.transform;
    }
    private void Start( ) {
        setup( );
        
    }
    private void Update( ) {
        update();
    }

    protected virtual void setup( )  {
    }

    protected virtual void update( ) {
    }

    /// <summary>ターゲットの関係性を取得する</summary>
    protected FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject my_obj, GameObject target_obj ) {
        GameObject my_parent = _family_manager.getParentObject( my_obj );
        GameObject target_parent = _family_manager.getParentObject( target_obj );
        FAMILY_DATA.TAG_NAME target_tag_name;
        
        Enum.TryParse( target_obj.tag, out target_tag_name );//タグを列挙に変換

        if( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        // 味方の親である。
        if( target_tag_name == FAMILY_DATA.TAG_NAME.PARENT &&
            target_obj == my_parent ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_PARENT;
        }
        // 敵の親である。
        if( target_tag_name == FAMILY_DATA.TAG_NAME.PARENT &&
            target_parent == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // 味方の子分である。
        if( target_tag_name == FAMILY_DATA.TAG_NAME.HENCHMAN &&
           ( target_parent == my_parent || target_parent == my_obj ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // 味方以外の子分である。
        if( target_tag_name == FAMILY_DATA.TAG_NAME.HENCHMAN &&
            target_parent != my_parent ) {
            //野生の子分である
            if( target_parent == null ) {
                return FAMILY_DATA.RELATIONSHIP_TYPE.WILD_HENCHMAN;
            }
            //敵の子分である
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
    }
}
