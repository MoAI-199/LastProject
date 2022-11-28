using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
public class OverrideSprite : MonoBehaviour {

    

    private SpriteRenderer sr;

    private static int idMainTex = Shader.PropertyToID( "_MainTex" );
    private MaterialPropertyBlock block;

   
    [SerializeField]
    private Texture texture = null;
    public Texture overrideTexture {
        get {
            return texture;
        }
        set {
            texture = value;
            if( block == null ) {
                Init( );
            }
            block.SetTexture( idMainTex, texture );
        }
    }

    void Awake( ) {
        Init( );
        overrideTexture = texture;
    }

    void LateUpdate( ) {
        sr.SetPropertyBlock( block );
        overrideTexture = texture;

    }

    void OnValidate( ) {
        overrideTexture = texture;
    }

    void Init( ) {
        block = new MaterialPropertyBlock( );
        sr = GetComponent<SpriteRenderer>( );
        sr.GetPropertyBlock( block );
    }

   

    public void setSprite( bool parent, COMMON_DATA.TEXTURE_COLOR color ) {
        if( parent ) {
            texture = GameManager.instatnce._texture_parent_list[ ( int )color ];
        }else{
            texture = GameManager.instatnce._texture_henchman_list[ ( int )color ];
        }
    }

}