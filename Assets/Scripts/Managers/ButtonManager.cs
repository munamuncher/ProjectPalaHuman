using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void PauseGame()
    {
        GameManager.Inst.StateOfGame(GameState.GamePause);
    }

    public void ReturnGame()
    {
        GameManager.Inst.StateOfGame(GameState.GameResume);
    } 
    public void AllyUnitBuy(int id) 
    {
        UnitSpawnScript._Instances.SpawnUnit(id, Faction.Ally);
    }

}
