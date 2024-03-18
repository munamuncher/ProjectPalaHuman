using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public enum GameState
{     
       GameStart,
       GamePause,
       GameEnd,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Inst => _instance;

    private void Awake()
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    

}
