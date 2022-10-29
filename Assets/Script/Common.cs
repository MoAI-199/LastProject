using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
namespace FAMILY_DATA{
=======
namespace COMMON_DATA{

    public class SettingName{
        public const string FAMILY_MANAGER = "FamilyManager";
        public const string PREFAB_PARENT_PATH = "Prefab/Parent";
        public const string PREFAB_HENCHMAN_PATH = "Prefab/Henchman";
        public const float TIMER = 180.0f;
    }
    public enum SCENE_TYPE{
        TITLE,
        GAME,
    }

    public enum TITLE_TYPE{
        NORMAL,
        MODE_SELECT,
        MANUAL,
    }
   
    public enum GAME_STATE_TYPE{
        GAME_SETTING,
        GAME_PLAYING,
        GAME_END,
        RESULT,
        NONE
    }
    public enum GAME_MODE {
        PVP,
        CHELLENGE,
        NONE,
    }
    //並び替え禁止
>>>>>>> 1777511e0c21dbd24eb50dc1a42b5ed410198f5a
    public enum RELATIONSHIP_TYPE{
        MY_PARENT,
        MY_HENCHMAN,
        ENEMY_PARENT,
        ENEMY_HENCHMAN,
        WILD_HENCHMAN,
        NONE,
    }
    public enum TAG_NAME{
        PARENT,
        HENCHMAN,
    }
}

