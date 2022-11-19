using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasAction : MonoBehaviour, IPointerClickHandler {
    // Start is called before the first frame update
    [SerializeField]
    private Image _manual;
    void Start( ) {
        switch( GameManager.instatnce.getGameMode( ) ) {
            case GAME_MODE.PVP:
                _manual.sprite = Resources.Load<Sprite>(SettingName.PVP_MANUAL);
                //_manual.color = Color.red;
                break;
            case GAME_MODE.CHELLENGE:
                _manual.sprite = Resources.Load<Sprite>(SettingName.CHALLENGE_MANUAL);
                //_manual.color = Color.blue;
                break;
        }
    }

    // Update is called once per frame
    void Update( ) {

    }

    public void OnPointerClick( PointerEventData eventData ) {
        if( GameManager.instatnce.getGameState( ) == GAME_STATE_TYPE.GUIDE ) {
            gameObject.SetActive( false );
            GameManager.instatnce.doneGameStatus( );
        }
    }
}
