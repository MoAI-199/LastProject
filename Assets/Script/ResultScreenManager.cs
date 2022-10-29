using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Burst.Intrinsics;

public class ResultScreenManager : MonoBehaviour
{
    [Header("必要なスクリプト")]
    [SerializeField]
    private FamilyManager _family_manager;

    [Header("ResultScreenの要素")]
    [SerializeField]
    private GameObject _result_screen;
    [SerializeField]
    private TextMeshProUGUI _winner_name;
    [SerializeField]
    private GameObject _winner_text_box;
    [SerializeField]
    private GameObject _draw_text;


    private bool one_time;


    // Start is called before the first frame update
    void Start()
    {
        _result_screen.SetActive(false);
        one_time = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch( COMMON_DATA.GAME_MODE.CHELLENGE ) {
            case COMMON_DATA.GAME_MODE.PVP:
                if( _family_manager.getParentCount( ) == 1 && one_time ) {
                    one_time = false;
                    _result_screen.SetActive( true );
                    _draw_text.SetActive( false );
                    _winner_name.text = _family_manager.getWinnerParent( ).GetComponent<Parent>( ).getParemeter( ).name;
                } else if( _family_manager.getParentCount( ) == 0 && one_time ) {
                    one_time = false;
                    _winner_text_box.SetActive( false );
                    _result_screen.SetActive( true );
                }
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                break;
            default:
                break;
        }
        
    }

    public void sceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//現在のシーンをリロード
    }

}
