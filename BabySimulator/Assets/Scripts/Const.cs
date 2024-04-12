using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.FullSerializer;
namespace Const
{
    public enum Level
    {
        TUTORIAL,
        NORMAL,
        HARD
    }

    public enum BabyRequest
    {
        MILK,
        NAPPY,
        TOY
    }

    public enum CoolTime
    {
        SLOW = 1,
        NORMAL = 2,
        FAST = 3
    }

    public class Common
    {

        public const string TEST = "aaa";

        public const string KEY_GAME_SCORE = "score";

        public const string KEY_TITLE_MODE = "mode";

        public const string KEY_GAME_LEVEL = "level";

        public const int MODE_NORMAL = 1;

        public const int MODE_HARD = 2;

        public const int MODE_DEFAULT = 0;

        public const int MODE_SELECT = 1;

        /// <summary>
        /// シーン読み込み関数
        /// </summary>
        public static void LoadScene(string _name)
        {
            SceneManager.LoadScene(_name);
            Debug.Log(_name + "シーンを読み込んだ");
        }

        /// <summary>
        /// マウス座標をワールド座標に変換して返す
        /// </summary>
        public static Vector3 GetWorldMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
