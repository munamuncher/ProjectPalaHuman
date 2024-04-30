using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("-----------Image-----------")]
    [SerializeField] private Image spPointBar;
    [SerializeField] private Image expBar;
    [SerializeField] private Image manaBar;
    [Header("------------Text-----------")]
    [SerializeField] private TextMeshProUGUI spawnPointText;
    [SerializeField] private TextMeshProUGUI expBarText;
    [SerializeField] private TextMeshProUGUI manaPointtext;
    [SerializeField] private TextMeshProUGUI gameEndText;
    [Header("-----------PopUp-----------")]
    [SerializeField] public GameObject pausePopUp;
    [SerializeField] public GameObject gameOverPopUp;
    [SerializeField] public GameObject levelUpPopUp;
    [SerializeField] private List<GameObject> stars;

    private ResourceManager rm;
    private PlayerManager pm;
    private GameManager gm;
    private static UIManager _instance;

 
    public static UIManager Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

             
                if (_instance == null)
                {
                    GameObject UIManager = new GameObject("UIManager");
                    _instance = UIManager.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }
    public void UpdatePoint()
    {
        rm = ResourceManager.Instance;
        if (rm.SpawnPoints > 0)
        {
            spawnPointText.text = (rm.SpawnPoints.ToString() + "/" + rm.MaxSpawnPoints.ToString());
            float spawnPointPercentage = (float)rm.SpawnPoints / rm.MaxSpawnPoints;
            spPointBar.fillAmount = spawnPointPercentage;
        }
        else
        {
            spPointBar.fillAmount = 0;
        }
    }
    public void ExpBar_Update()
    {
        pm = PlayerManager.Instance;
        if (pm.expPoints >= pm.expMaxPoint)
        {
            pm.hasLevelUp = true;
        }
        if (!pm.hasLevelUp)
        {
            float expPointsPercentage = (float)pm.expPoints / pm.expMaxPoint;
            expBar.fillAmount = expPointsPercentage;
        }
        else
        {
            LevelUp();
            pm.expPoints = 0;
            expBar.fillAmount = 0;
        }
    }

    public void LevelUp()
    {
        pm = PlayerManager.Instance;
        if (pm.hasLevelUp)
        {
            pm.expMaxPoint *= 2;
            pm.playerLevel++;
            expBarText.text = pm.playerLevel.ToString();
            levelUpPopUp.gameObject.SetActive(true);
            //GameManager.Inst.DisplaySkills();// todo Skill script 완성후 스킬 종류를 게임 매니저한테 받아서 levelupPopup 한테 전달
            GameManager.Inst.StateOfGame(GameState.GamePause);
        }
        else
        {
            expBarText.text = pm.playerLevel.ToString();
        }
    }

    public void UpdateMana()
    {
        pm = PlayerManager.Instance;
        if (pm.ManaPoints > 0)
        {
            manaPointtext.text = pm.ManaPoints.ToString() + "/" + pm.maxManaPoint.ToString();
            float manaPointPercentage = (float)pm.ManaPoints / pm.maxManaPoint;
            manaBar.fillAmount = manaPointPercentage;
        }
        else
        {
            manaBar.fillAmount = 0;
        }
    }

    public void GameEndText_Update(GameState GameEnd)
    {
        Debug.Log(GameEnd);
        if (GameEnd == GameState.GameWin)
        {
            gameEndText.text = ("You have Won!! \n Congratulation!");
            gameEndText.color = Color.green;
        }
        else if (GameEnd == GameState.GameOver)
        {
            gameEndText.text = ("You have Died!! \n Misson Failed");
            gameEndText.color = Color.red;
        }
    }
    public void GameOverStarUpdate(GameState EndStar) // todo leenTween 사용해서 개선하기
    {
        if (EndStar == GameState.GameStart)
        {
            //Debug.Log("Game start has been called and will trun off all the stars");
            for (int i = 0; i <= 2; i++)
            {
                //Debug.Log("turning off all stars");
                stars[i].gameObject.SetActive(false);
                //Debug.Log(stars[i].gameObject);
            }
        }
        else if (EndStar == GameState.GameWin) // todo 조건에 따라 별 휙득  leentween으로 수정
        {
            stars[0].gameObject.SetActive(false);
            if (gm.StarCounter() == true)
            {
                stars[1].gameObject.SetActive(true);
            }
            if (gm.skillCounter() == true)
            {
                stars[2].gameObject.SetActive(true);
            }
            if (gm.TimeCounter() == true)
            {
                stars[3].gameObject.SetActive(true);
            }
        }
    }
}
