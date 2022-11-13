using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Burst.Intrinsics;
using UnityEngine.UI;
using TMPro.SpriteAssetUtilities;

public class ResultScreenManager : MonoBehaviour {
    private TMP_Text _winner_text;
    private TMP_Text _judgment_text;
    private void Awake( ) {
       
    }
    void Start( ) {
        foreach( var text in GetComponentsInChildren<TMP_Text>( ) ) {
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
