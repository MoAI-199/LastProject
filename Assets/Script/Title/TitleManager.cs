using System;
using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[ ] _gamemodeButtons;

    [SerializeField]
    private GameObject[ ] _manualButtonsOn;

    [SerializeField]
    private GameObject[ ] _manualButtonsOff;

    [SerializeField]
    private GameObject[ ] _manualImages;

    [SerializeField]
    private Image _manualMainImage;

    [SerializeField] private GameObject _manualPage;
    [SerializeField] private GameObject _gamemodePage;

    [SerializeField] private GameObject titleGroup;
    [SerializeField] private GameObject titleGroupBg;
    private COMMON_DATA.TITLE_TYPE title_type = COMMON_DATA.TITLE_TYPE.NORMAL;
    void Awake( ) {
        //GameMode
        GameModeButtonAddListener( _gamemodeButtons[ 0 ], COMMON_DATA.GAME_MODE.PVP );
        GameModeButtonAddListener( _gamemodeButtons[ 1 ], COMMON_DATA.GAME_MODE.CHELLENGE );

        //Howto
        howtoButtonAddListener( _gamemodeButtons[ 2 ] );
        //PageButton
        PageButtonAddListener( 0 );
        PageButtonAddListener( 1 );
        PageButtonAddListener( 2 );


        //PageButtonBack
        howtoButtonAddListener( _manualButtonsOn[ 3 ] );

        _manualImages[ 0 ].SetActive( true );
        _manualImages[ 1 ].SetActive( false );
        _manualImages[ 2 ].SetActive( false );
        titleGroupBg.SetActive( false );
    }

    private void PageButtonAddListener( int idx ) {
        Button button = _manualButtonsOff[ idx ].GetComponent<Button>( );
        button.onClick.AddListener( ( ) => {
            //Change Sprite
            switch( idx ) {
                case 0:
                    _manualImages[ 0 ].SetActive( true );
                    _manualImages[ 1 ].SetActive( false );
                    _manualImages[ 2 ].SetActive( false );

                    _manualButtonsOff[ 0 ].SetActive( false );
                    _manualButtonsOff[ 1 ].SetActive( true );
                    _manualButtonsOff[ 2 ].SetActive( true );

                    _manualButtonsOn[ 0 ].SetActive( true );
                    _manualButtonsOn[ 1 ].SetActive( false );
                    _manualButtonsOn[ 2 ].SetActive( false );
                    break;
                case 1:
                    _manualImages[ 0 ].SetActive( false );
                    _manualImages[ 1 ].SetActive( true );
                    _manualImages[ 2 ].SetActive( false );

                    _manualButtonsOff[ 0 ].SetActive( true );
                    _manualButtonsOff[ 1 ].SetActive( false );
                    _manualButtonsOff[ 2 ].SetActive( true );

                    _manualButtonsOn[ 0 ].SetActive( false );
                    _manualButtonsOn[ 1 ].SetActive( true );
                    _manualButtonsOn[ 2 ].SetActive( false );
                    break;
                case 2:
                    _manualImages[ 0 ].SetActive( false );
                    _manualImages[ 1 ].SetActive( false );
                    _manualImages[ 2 ].SetActive( true );

                    _manualButtonsOff[ 0 ].SetActive( true );
                    _manualButtonsOff[ 1 ].SetActive( true );
                    _manualButtonsOff[ 2 ].SetActive( false );

                    _manualButtonsOn[ 0 ].SetActive( false );
                    _manualButtonsOn[ 1 ].SetActive( false );
                    _manualButtonsOn[ 2 ].SetActive( true );
                    break;
            }
        } );

    }

    // Update is called once per frame
    void howtoButtonAddListener( GameObject obj ) {
        Button button = obj.GetComponent<Button>( );
        button.onClick.AddListener( ( ) => {
            //showManuaPage();
            sliderXPage( );
        } );
    }

    public void sliderYPage( ) {
        if( title_type == TITLE_TYPE.NORMAL ) {
            titleGroup.transform.DOLocalMoveY( 0, 1f ).SetEase( Ease.OutBounce );
            titleGroupBg.SetActive( true );

            title_type = TITLE_TYPE.MODE_SELECT;
        }
    }
    void sliderXPage( ) {
        if( title_type == TITLE_TYPE.MODE_SELECT ) {
            titleGroup.transform.DOLocalMoveX( -1270, 0.5f );
            title_type = TITLE_TYPE.MANUAL;
        } else {
            titleGroup.transform.DOLocalMoveX( 0, 0.5f );
            title_type = TITLE_TYPE.MODE_SELECT;

        }
    }

    void showManuaPage( ) {
        if( _manualPage.activeSelf ) {
            _manualPage.SetActive( false );
            _gamemodePage.SetActive( true );
        } else {
            _manualPage.SetActive( true );
            _gamemodePage.SetActive( false );
        }
    }
    void GameModeButtonAddListener( GameObject obj, COMMON_DATA.GAME_MODE mode ) {
        Button button = obj.GetComponent<Button>( );
        button.onClick.AddListener( ( ) => GameModeStart( mode ) );
    }

    void GameModeStart( COMMON_DATA.GAME_MODE mode ) {
        GameManager.instatnce.setGameMode( mode );
        GameManager.instatnce.doneGameStatus( );
        SceneManager.LoadSceneAsync( 1 );

    }
    public TITLE_TYPE getTitleType( ) {
        return title_type;
    }

}
