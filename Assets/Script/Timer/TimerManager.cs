using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Sprite[] numbers;
    
    [SerializeField] private Image[] timerObj;

    private float time = COMMON_DATA.SettingName.TIMER;
    void Start()
    {
        switch (GameManager.instatnce.getGameMode())
        {
            case COMMON_DATA.GAME_MODE.PVP:
                timerObj[0].gameObject.transform.parent.gameObject.SetActive(false);
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                timerObj[0].gameObject.transform.parent.gameObject.SetActive(true);
                break;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.instatnce.getGameMode())
        {
            case COMMON_DATA.GAME_MODE.PVP:
                
                return;
                
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                if (checkgamestate())
                {
                    time -= Time.deltaTime;
                    if (time <= 0)
                    {
                        time = 0;
                        GameManager.instatnce.doneGameStatus( );
                    }
                    int min = (int)time / 60;
                    int sec = (int)time % 60;
                    timerObj[0].sprite = numbers[min % 10];
                    timerObj[1].sprite = numbers[sec / 10];
                    timerObj[2].sprite = numbers[sec % 10];
                }
                
                break;
                
        }
    }
    bool checkgamestate()
    {
        if (GameManager.instatnce.getGameState() == COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING)
        {
            return true;
        }

        return false;

    }
}
