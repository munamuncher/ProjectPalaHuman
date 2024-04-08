using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using TMPro;
using UnityEngine.UI;
public enum GameState
{     
       GameStart,
       GamePause,
       GameResume,
       GameOver,
       GameWin,
}

public class GameManager : MonoBehaviour
{  
    [SerializeField]
    private Image spPointBar;
    [SerializeField]
    private TextMeshProUGUI spawnPointText;
    [SerializeField]
    private GameObject PausePopUp;
    [SerializeField]
    private GameObject GameOverPopUp;
    [SerializeField]
    private TextMeshProUGUI GameEndText;


    private int spawnPoints;
    private int maxSpawnPonints;


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
        StateOfGame(GameState.GameStart);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            spawnPoints += 100;
        }
    }

    public bool CanBuy(int cost)
    {
        return spawnPoints >= cost;
    }

    public void isBuy(int cost)
    {
        if(spawnPoints - cost >= cost)
        {
            spawnPoints -= cost;
        }
        else
        {
            spawnPoints = 0;
        }
    }
    public int SpawnPoints
    {
        set 
        {
            spawnPoints = value;
            UpdatePoint();

        }
        get { return spawnPoints; }
    }
    IEnumerator EarnPoints()
    {
        while (true)
        {
            if (spawnPoints >= maxSpawnPonints)
            {
                Debug.Log("maxPoints");
            }
            else
            {
                spawnPoints += 5;
                UpdatePoint();

            }
            yield return new WaitForSeconds(0.5f);
            Debug.Log(spawnPoints);
        }
    }
    private void UpdatePoint()
    {
        if (spawnPoints > 0)
        {
            spawnPointText.text = (spawnPoints.ToString() + "/" + maxSpawnPonints.ToString());
            float spawnPointPercentage = (float)spawnPoints / maxSpawnPonints;
            spPointBar.fillAmount = spawnPointPercentage;
        }
        else
        {
            spPointBar.fillAmount = 0;
        }
    }
    
    private void GameEndText_Update(GameState GameEnd)
    {
        if(GameEnd == GameState.GameWin)
        {
            GameEndText.text = ("You have Won!! \n Congratulation!" );
        }
        else
        {
            GameEndText.text = ("You have Died!! \n Misson Failed");
        }
    }

    public void StateOfGame(GameState gs)
    {
        switch (gs)
        {
            case GameState.GameStart:
                Time.timeScale = 1f;
                maxSpawnPonints = 500;
                spawnPoints = 0;    
                UpdatePoint();
                StartCoroutine("EarnPoints");
                PausePopUp.SetActive(false);
                GameOverPopUp.SetActive(false);
                break;
            case GameState.GamePause:
                PausePopUp.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.GameResume:
                Time.timeScale = 1f;
                PausePopUp.SetActive(false);
                break;
            case GameState.GameOver:
                GameOverPopUp.SetActive(true);
                GameEndText_Update(GameState.GameOver);
                Time.timeScale = 0f;
                break;
            case GameState.GameWin:
                GameOverPopUp.SetActive(true);
                GameEndText_Update(GameState.GameWin);
                Time.timeScale = 0f;
                break;
       }

    }

}
