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
    //todo UImanager 만들어서 UI 옮기기
    [Header("---------UI--------")]
    [SerializeField]
    private Image spPointBar;    
    [SerializeField]
    private Image expBar;
    [SerializeField]
    private Image manaBar;

    [Header("-------PopUp-------")]
    [SerializeField]
    private GameObject pausePopUp;
    [SerializeField]
    private GameObject gameOverPopUp;    
    [SerializeField]
    private List<GameObject> stars;    
    [SerializeField]
    private GameObject levelUpPopUp;

    [Header("-------UIText------")]
    [SerializeField]
    private TextMeshProUGUI gameEndText;   
    [SerializeField]
    private TextMeshProUGUI spawnPointText;
    [SerializeField]
    private TextMeshProUGUI expBarText;
    [SerializeField]
    private TextMeshProUGUI manaPointtext;

    private int manaPoint;
    private int maxManaPoint;
    private int playerLevel;
    private int expMaxPoint;
    private int expPoints;
    private bool hasLevelUp;
    private int spawnPoints;
    private int maxSpawnPonints;
    private int SkillUseageCount;

    private LevelUpPopUp lvlupPopCS;
    private PlayerMovement pm;

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


        if(levelUpPopUp != null)
        {
            if(!TryGetComponent<LevelUpPopUp>(out lvlupPopCS))
            {
                Debug.Log("GameManager.cs - Awake() - LevelUpPopUp 참조 실패");
            }
        }
        pm = FindFirstObjectByType<PlayerMovement>();
        if(!pm)
        {
            Debug.Log("playerMovement.cs 참조 실패");
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
    #region _Buy_&&_Spawn_
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
    #endregion

    public bool CanCast(int mpCost)
    {
        return manaPoint >= mpCost;
    }

    public void isCast(int mpCost)
    {
        if (manaPoint - mpCost >= mpCost)
        {
            manaPoint -= mpCost;
        }
        else
        {
            manaPoint = 0;
        }
    }
    public int ManaPoints
    {
        set
        {
            manaPoint = value;
            UpdatePoint();

        }
        get { return manaPoint; }
    }
    IEnumerator EarnMana()
    {
        
        while(true)
        {
            if(manaPoint >= maxManaPoint)
            {
                Debug.Log("mana is full");
                
            }
            else
            {
                manaPoint += 5;
                UpdateMana();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void UpdateMana()
    {
        
        if(manaPoint > 0)
        {  
            manaPointtext.text = (manaPoint.ToString() + "/"+ maxManaPoint.ToString());
            float manaPointPercentage = (float)manaPoint / maxManaPoint;
            manaBar.fillAmount = manaPointPercentage;
        }
        else
        {
            manaBar.fillAmount = 0;
        }
    }
    public void ManaUsage()
    {

    }
    #region _GameState_
    private void GameEndText_Update(GameState GameEnd)
    {
        Debug.Log(GameEnd);
        if(GameEnd == GameState.GameWin)
        {
            gameEndText.text = ("You have Won!! \n Congratulation!" );
            gameEndText.color = Color.green;
        }
        else if(GameEnd == GameState.GameOver)
        {
            gameEndText.text = ("You have Died!! \n Misson Failed");
            gameEndText.color = Color.red;
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
        else if(EndStar == GameState.GameWin) // todo 조건에 따라 별 휙득
        {
            if (pm.HP > 50)
            {
                stars[1].gameObject.SetActive(true);
            }
            if(SkillUseageCount >  5)
            {
                stars[2].gameObject.SetActive(true);
            }
        }
    }
    #endregion

    #region _EXP_&&_LevelUp_
    public void DeathExpUp()
    {
        expPoints += 5;
        ExpBar_Update();
        Debug.Log("exp has risen" + expPoints);
    }

    private void ExpBar_Update()
    {
        if (expPoints >= expMaxPoint)
        {
            hasLevelUp = true;
        }
        if (!hasLevelUp)
        {
            float expPointsPercentage = (float)expPoints / expMaxPoint;
            expBar.fillAmount = expPointsPercentage;
        }
        else
        {
            LevelUp();
            expPoints = 0;
            expBar.fillAmount = 0;
        }
    }

    private void LevelUp()
    {
        if(hasLevelUp)
        {
            expMaxPoint *= 2;
            Debug.Log(expMaxPoint);
            playerLevel++;
            expBarText.text = playerLevel.ToString();
            levelUpPopUp.gameObject.SetActive(true);
            lvlupPopCS.Displayskill(); // todo Skill script 완성후 스킬 종류를 게임 매니저한테 받아서 levelupPopup 한테 전달
            StateOfGame(GameState.GamePause);
        }
        else
        {
            expBarText.text = playerLevel.ToString();
        }
    }
    #endregion


    private void DisplayLevelUpSelection()
    {
        
    }

    public void StateOfGame(GameState gs)
    {
        switch (gs)
        {
            case GameState.GameStart:
                GameOverStarUpdate(GameState.GameStart);
                Time.timeScale = 1f;
                maxManaPoint = 200;
                
                expMaxPoint = 20;
                maxSpawnPonints = 500;
                manaPoint = 0;
                spawnPoints = 0;
                expPoints = 0;
                playerLevel = 1;
                hasLevelUp = false;
                UpdatePoint();
                UpdateMana();
                LevelUp();
                ExpBar_Update();
                StartCoroutine("EarnMana");
                StartCoroutine("EarnPoints");  
                pausePopUp.SetActive(false);
                gameOverPopUp.SetActive(false);
                
                break;
            case GameState.GamePause:
                if(!hasLevelUp)
                {
                    pausePopUp.SetActive(true);
                }
                Time.timeScale = 0f;
                break;
            case GameState.GameResume:
                Time.timeScale = 1f;
                pausePopUp.SetActive(false);
                break;
            case GameState.GameOver:
                gameOverPopUp.SetActive(true);
                GameEndText_Update(GameState.GameOver);
                Time.timeScale = 0f;
                break;
            case GameState.GameWin:
                gameOverPopUp.SetActive(true);
                GameEndText_Update(GameState.GameWin);
                GameOverStarUpdate(GameState.GameWin);
                Time.timeScale = 0f;
                break;
       }

    }

}
