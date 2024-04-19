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
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private GameObject levelUpPopUp;
    [SerializeField] private List<GameObject> stars;

    private ResourceManager rm;

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
    private void UpdatePoint()
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
    //private void ExpBar_Update()
    //{
    //    if (expPoints >= expMaxPoint)
    //    {
    //        hasLevelUp = true;
    //    }
    //    if (!hasLevelUp)
    //    {
    //        float expPointsPercentage = (float)expPoints / expMaxPoint;
    //        expBar.fillAmount = expPointsPercentage;
    //    }
    //    else
    //    {
    //        LevelUp();
    //        expPoints = 0;
    //        expBar.fillAmount = 0;
    //    }
    //}

    //private void LevelUp()
    //{
    //    if(hasLevelUp)
    //    {
    //        expMaxPoint *= 2;
    //        Debug.Log(expMaxPoint);
    //        playerLevel++;
    //        expBarText.text = playerLevel.ToString();
    //        levelUpPopUp.gameObject.SetActive(true);
    //        lvlupPopCS.Displayskill(); // todo Skill script 완성후 스킬 종류를 게임 매니저한테 받아서 levelupPopup 한테 전달
    //        StateOfGame(GameState.GamePause);
    //    }
    //    else
    //    {
    //        expBarText.text = playerLevel.ToString();
    //    }
    //}

    //private void UpdateMana()
    //{
        
    //    if (manaPoint > 0)
    //    {
    //        manaPointtext.text = (manaPoint.ToString() + "/" + maxManaPoint.ToString());
    //        float manaPointPercentage = (float)manaPoint / maxManaPoint;
    //        manaBar.fillAmount = manaPointPercentage;
    //    }
    //    else
    //    {
    //        manaBar.fillAmount = 0;
    //    }
    //}
}
