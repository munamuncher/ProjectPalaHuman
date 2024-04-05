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
       GameEnd,
}

public class GameManager : MonoBehaviour
{
    private int spawnPoints;
    private int maxSpawnPonints;
    [SerializeField]
    private Image spPointBar;
    [SerializeField]
    private TextMeshProUGUI spawnPointText;

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
        maxSpawnPonints = 500;
        spawnPoints = 0;
        UpdatePoint();
        StartCoroutine("EarnPoints");
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

}
