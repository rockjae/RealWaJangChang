using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    internal static class DataName
    {
        public const string STAGE0_NAME = "Tutorial";
        public const string STAGE1_NAME = "Main1";
        public const string STAGE2_NAME = "Main2";
        public const string STAGE3_NAME = "Main3";
        public const string STAGE4_NAME = "Main4";
        public const string STAGE5_NAME = "Main5";
        public const string STAGE6_NAME = "Main6";

        public const string FLAPPY_GAME_NAME = "FlappySausage";
        public const string FLAPPY_SCORE_NAME = "flappyscore";

        public const string SHOOTINGSAUSAGE_GAME_NAME = "ShootingSausage";
        public const string SHOOTINGSAUSAGE_SCORE_NAME = "shootingScore";

        public const string HIGHNOON_GAME_NAME = "HighNoonSausage";
        public const string HIGHNOON_SCORE_NAME = "HighNoonScore";

        public const string AUDIO_SETTING_NAME = "isAudio";
    }

    [Header("AudioSetting")]
    public bool AudioSetting;
    [Header("StageOpen")]
    public bool Stage0;
    public bool Stage1;
    public bool Stage2;
    public bool Stage3;
    public bool Stage4;
    public bool Stage5;
    public bool Stage6;
    [Header("Flappy")]
    public bool FlappyGame;
    public int FlappyScore;
    [Header("ShootingSausage")]
    public bool ShootingSausageGame;
    public float ShootingSausageScore;
    [Header("HighNoon")]
    public bool HighNoonSausageGame;
    public float HighNoonSausageScore;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<DataManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("DataManager").AddComponent<DataManager>();
                    instance = newSingleton;
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        var obj = FindObjectsOfType<DataManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        saveStageValue(DataName.STAGE1_NAME, true); //스테이지1 해금
    }

    private void Start()
    {
        Stage0 = loadStageValue(DataName.STAGE0_NAME) == 1 ? true : false;
        Stage1 = loadStageValue(DataName.STAGE1_NAME) == 1 ? true : false;
        Stage2 = loadStageValue(DataName.STAGE2_NAME) == 1 ? true : false;
        Stage3 = loadStageValue(DataName.STAGE3_NAME) == 1 ? true : false;
        Stage4 = loadStageValue(DataName.STAGE4_NAME) == 1 ? true : false;
        Stage5 = loadStageValue(DataName.STAGE5_NAME) == 1 ? true : false;
        Stage6 = loadStageValue(DataName.STAGE6_NAME) == 1 ? true : false;

        FlappyGame = getFlappyGameOpen() == 1 ? true : false;
        FlappyScore = getFlappyScore();

        int isaudio = DataManager.instance.getAudioSetting();
        Debug.Log("rockjae0 : " +isaudio);
        AudioListener.volume = isaudio == 0 ? 0 : 1;
        AudioSetting = isaudio == 0 ? false : true;
    }

    public void saveStageValue(string name,bool isOpen)
    {
        PlayerPrefs.SetInt(name, isOpen==true?1:0);
        PlayerPrefs.Save();

        switch (name)
        {
            case DataName.STAGE0_NAME:
                {
                    Stage0 = loadStageValue(DataName.STAGE0_NAME) == 1 ? true : false;
                    break;
                }
            case DataName.STAGE1_NAME:
                {
                    Stage1 = loadStageValue(DataName.STAGE1_NAME) == 1 ? true : false;
                    break;
                }
            case DataName.STAGE2_NAME:
                {
                    Stage2 = loadStageValue(DataName.STAGE2_NAME) == 1 ? true : false;
                    break;
                }
            case DataName.STAGE3_NAME:
                {
                    Stage3 = loadStageValue(DataName.STAGE3_NAME) == 1 ? true : false;
                    break;
                }
            case DataName.STAGE4_NAME:
                {
                    Stage4 = loadStageValue(DataName.STAGE4_NAME) == 1 ? true : false;
                    break;
                }
            case DataName.STAGE5_NAME:
                {
                    Stage5 = loadStageValue(DataName.STAGE5_NAME) == 1 ? true : false;
                    break;
                }
            case DataName.STAGE6_NAME:
                {
                    Stage6 = loadStageValue(DataName.STAGE6_NAME) == 1 ? true : false;
                    break;
                }
        }
    }

    public int loadStageValue(string name)
    {
        return PlayerPrefs.GetInt(name, 0);
    }

    public void setFlappyGameOpen()
    {
        FlappyGame = true;
        PlayerPrefs.SetInt(DataName.FLAPPY_GAME_NAME, 1);
        PlayerPrefs.Save();
    }
    public int getFlappyGameOpen()
    {
        return PlayerPrefs.GetInt(DataName.FLAPPY_GAME_NAME, 0);
    }

    public void setFlappyScore(int score)
    {
        FlappyScore = score;
        PlayerPrefs.SetInt(DataName.FLAPPY_SCORE_NAME, score);
        PlayerPrefs.Save();
    }
    public int getFlappyScore()
    {
        return PlayerPrefs.GetInt(DataName.FLAPPY_SCORE_NAME, 0);
    }

    public void setShootingSausageGameOpen()
    {
        ShootingSausageGame = true;
        PlayerPrefs.SetInt(DataName.SHOOTINGSAUSAGE_GAME_NAME, 1);
        PlayerPrefs.Save();
    }
    public int getShootingSausageGameOpen()
    {
        return PlayerPrefs.GetInt(DataName.SHOOTINGSAUSAGE_GAME_NAME, 0);
    }

    public void setHighNoonSausageGameOpen()
    {
        HighNoonSausageGame = true;
        PlayerPrefs.SetInt(DataName.HIGHNOON_GAME_NAME, 1);
        PlayerPrefs.Save();
    }
    public int getHighNoonSausageGameOpen()
    {
        return PlayerPrefs.GetInt(DataName.HIGHNOON_GAME_NAME, 0);
    }

    public void setShootingSausageScore(float score)
    {
        ShootingSausageScore = score;
        PlayerPrefs.SetFloat(DataName.SHOOTINGSAUSAGE_SCORE_NAME, score);
        PlayerPrefs.Save();
    }
    public float getShootingSausageScore()
    {
        return PlayerPrefs.GetFloat(DataName.SHOOTINGSAUSAGE_SCORE_NAME, 0);
    }

    public void setHighNoonSausageScore(float score)
    {
        HighNoonSausageScore = score;
        PlayerPrefs.SetFloat(DataName.HIGHNOON_SCORE_NAME, score);
        PlayerPrefs.Save();
    }
    public float getHighNoonSausageScore()
    {
        return PlayerPrefs.GetFloat(DataName.HIGHNOON_SCORE_NAME, 0);
    }

    public void setAudioSetting(int setting)
    {
        AudioSetting = setting == 0 ? false : true;
        PlayerPrefs.SetInt(DataName.AUDIO_SETTING_NAME, setting);
        PlayerPrefs.Save();
    }
    public int getAudioSetting()
    {
        return PlayerPrefs.GetInt(DataName.AUDIO_SETTING_NAME, 1);
    }
}
