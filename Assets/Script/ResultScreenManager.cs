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
                    //draw�ɂȂ�
                    _judgment_text.text = "DRAW";
                    _winner_text.gameObject.SetActive( false );
                } else{
                    //���҂�����
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
    /// ������x�������[�h���v���C�B
    /// </summary>
    public void clickRetry( ) {
        SceneManager.LoadScene( SceneManager.GetActiveScene( ).buildIndex );//���݂̃V�[���������[�h
        GameManager.instatnce.doneGameStatus( );
    }

    /// <summary>�^�C�g���ֈړ�</summary>
    public void clickTitle( ){
    }

}
