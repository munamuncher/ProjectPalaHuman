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
    [SerializeField]
    private List<GameObject> stars;


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
        Debug.Log(GameEnd);
        if(GameEnd == GameState.GameWin)
        {
            GameEndText.text = ("You have Won!! \n Congratulation!" );
            GameEndText.color = Color.green;
        }
        else if(GameEnd == GameState.GameOver)
        {
            GameEndText.text = ("You have Died!! \n Misson Failed");
            GameEndText.color = Color.red;
        }
    }

    private void GameOverStarUpdate(GameState EndStar) // todo leenTween 사용해서 개선하기
    {
        Debug.Log("Star has been called");
        if(EndStar == GameState.GameStart)
        {
            Debug.Log("Game start has been called and will trun off all the stars");
            for(int i =0; i <= 2 ;i++)
            {
                Debug.Log("turning off all stars");
                stars[i].gameObject.SetActive(false);
                Debug.Log(stars[i].gameObject);
            }
        }
        else if(EndStar == GameState.GameWin)
        {
            Debug.Log("Game Win has been called now fillin the stars");
           stars[0].gameObject.SetActive(true);
           stars[2].gameObject.SetActive(true);
        }
    }

    public void StateOfGame(GameState gs)
    {
        switch (gs)
        {
            case GameState.GameStart:
                GameOverStarUpdate(GameState.GameStart);
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
                GameOverStarUpdate(GameState.GameWin);
                Time.timeScale = 0f;
                break;
       }

    }

}
