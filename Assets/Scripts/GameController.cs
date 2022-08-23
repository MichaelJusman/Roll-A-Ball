using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { Normal, SpeedRun}
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameType gameType;

    // Start is called before the first frame update
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    void Start()
    {
        GetGameType();
    }

    //Gets the game type from our saved data
    public void GetGameType()
    {
        string savedGameType = PlayerPrefs.GetString("GameType");
        if (savedGameType != "")
            gameType = (GameType)System.Enum.Parse(typeof(GameType), savedGameType);
    }

    


    // Sets the game type from our selections
    public void SetGameType(GameType _gameType)
    {
        gameType = _gameType;
        PlayerPrefs.SetString("GameType", _gameType.ToString());
    }

    // To toggle between speedrun on or off
    public void ToggleSpeedRun(bool _speedRun)
    {
        if (_speedRun)
            SetGameType(GameType.SpeedRun);
        else
            SetGameType(GameType.Normal);
    }
}


