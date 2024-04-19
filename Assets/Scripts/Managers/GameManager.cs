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

    private int manaPoint;   // pm
    private int maxManaPoint; //pm
    private int playerLevel;  //pm
    private int expMaxPoint;  //pm
    private int expPoints;   //pm
    private bool hasLevelUp; //pm
    //private int spawnPoints;  //rm
    //private int maxSpawnPonints; //rm
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

    #region _Mana&SkillCast_

    public int ManaPoints
    {
        set
        {
            manaPoint = value;
            UpdateMana();

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
    #endregion

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
            stars[0].gameObject.SetActive(false);
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
    //public void DeathExpUp()
    //{
    //    expPoints += 5;
    //    ExpBar_Update();
    //    Debug.Log("exp has risen" + expPoints);
    //}


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
                //maxSpawnPonints = 500;
                //manaPoint = 0I
                //spawnPoints = 0;
                //expPoints = 0;
                //playerLevel = 1;
                //hasLevelUp = false;
                //UpdatePoint();
                UpdateMana();
                //LevelUp();
                //ExpBar_Update();
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
