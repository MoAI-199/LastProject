using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Burst.Intrinsics;
using Unity.UI;
using UnityEngine.UI;
using TMPro.SpriteAssetUtilities;

public class ResultScreenManager : MonoBehaviour {
    private Text[ ] _texts;
    private Text _winner_text;
    private Text _judgment_text;
    private void Awake( ) {
       
    }
    void Start( ) {
        foreach( var text in GetComponentsInChildren<Text>( ) ) {
            if( text.tag == "WINNER_TEXT" ){
                _winner_text = text;
            }else{
                _judgment_text = text;
            }
         }
    }

    void Update( ) {
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.PVP:
                //if( _family_manager.getParentCount( ) == 1 && one_time ) {
                //    one_time = false;
                //    _result_screen.SetActive( true );
                //    _draw_text.SetActive( false );
                //    _winner_name.text = _family_manager.getWinnerParent( ).GetComponent<Parent>( ).getParemeter( ).name;
                //} else if( _family_manager.getParentCount( ) == 0 && one_time ) {
                //    one_time = false;
                //    _winner_text_box.SetActive( false );
                //    _result_screen.SetActive( true );
                //}

                string winner_name = GameManager.instatnce.getUserData( ).getWinnerName( );
                if( string.IsNullOrEmpty( winner_name ) ){
                    //drawになる
                    _judgment_text.text = "DRAW";
                    _winner_text.gameObject.SetActive( false );
                } else{
                    //勝者がいる
                    _judgment_text.text = "WINNER";
                    _winner_text.text = winner_name;
                }
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// もう一度同じモードをプレイ。
    /// </summary>
    public void clickRetry( ) {
        SceneManager.LoadScene( SceneManager.GetActiveScene( ).buildIndex );//現在のシーンをリロード
        GameManager.instatnce.doneGameStatus( );
    }

    /// <summary>タイトルへ移動</summary>
    public void clickTitle( ){
    }

}
