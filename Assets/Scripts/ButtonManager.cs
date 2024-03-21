using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PausePopUp;
    private void Awake()
    {
        Time.timeScale = 1f;
        PausePopUp.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePopUp.SetActive(true);
    }

    public void ReturnGame()
    {
        Time.timeScale = 1f;
        PausePopUp.SetActive(false);
    }
    public void AllyUnitBuy(int id) 
    {
        UnitSpawnScript._Instances.SpawnUnit(id, Faction.Ally);
    }
}
