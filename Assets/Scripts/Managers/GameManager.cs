using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using TMPro;
using UnityEngine.UI;
using System.Net.NetworkInformation;
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
    public int SkillUseageCount;
    public int MaxTimeLimit = 300;
    private float timer = 0.0f;
    private GameObject levelUpPop;
    private LevelUpPopUp lvlupPopCS;
    private PlayerMovement pm;
    private PlayerManager playerManager;
    private UIManager um;
    private ResourceManager rm;

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
        levelUpPop = GameObject.Find("LevelUpSelection");
        if (levelUpPop != null)
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
        timer += Time.deltaTime;
        int seconds = (int)(timer % 60);

    }
    public void DisplaySkills()
    {
        lvlupPopCS.Displayskill();
    }

    public void DeathExpUp()
    {
        playerManager.expPoints += 5;
        um.ExpBar_Update();
        Debug.Log("exp has risen" + playerManager.expPoints);
    }



    private void DisplayLevelUpSelection()
    {
        
    }
    #region _starConditions_
    public bool TimeCounter()
    {
        if (timer > MaxTimeLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool skillCounter()
    {
        if(SkillUseageCount > 10)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }    
    public bool StarCounter()
    {
        if(pm.HP> 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


    public void StateOfGame(GameState gs)
    {
        playerManager = PlayerManager.Instance;
        um = UIManager.Instance;
        rm = ResourceManager.Instance;

        switch (gs)
        {
            case GameState.GameStart:
                um.GameOverStarUpdate(GameState.GameStart);
                Time.timeScale = 1f;
                playerManager.maxManaPoint = 200;
                playerManager.expMaxPoint = 20;
                rm.MaxSpawnPoints = 500;
                playerManager.ManaPoints = 0;
                rm.SpawnPoints = 0;
                playerManager.expPoints = 0;
                playerManager.playerLevel = 1;
                playerManager.hasLevelUp = false;
                um.UpdatePoint();
                um.UpdateMana();
                um.LevelUp();
                um.ExpBar_Update();
                playerManager.StartCoroutine("EarnMana");
                rm.StartCoroutine("EarnPoints");  
                um.pausePopUp.SetActive(false);
                um.gameOverPopUp.SetActive(false);
                
                break;
            case GameState.GamePause:
                if(!playerManager.hasLevelUp)
                {
                    um.pausePopUp.SetActive(true);
                }
                Time.timeScale = 0f;
                break;
            case GameState.GameResume:
                Time.timeScale = 1f;
                um.pausePopUp.SetActive(false);
                break;
            case GameState.GameOver:
                um.gameOverPopUp.SetActive(true);
                um.GameEndText_Update(GameState.GameOver);
                Time.timeScale = 0f;
                break;
            case GameState.GameWin:
                StarCounter();
                TimeCounter();
                skillCounter();
                um.gameOverPopUp.SetActive(true);
                um.GameEndText_Update(GameState.GameWin);
                um.GameOverStarUpdate(GameState.GameWin);
                Time.timeScale = 0f;
                break;
       }

    }

}
