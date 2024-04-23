using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int spawnPoints { get; set; }
    private int maxSpawnPoints { get; set; }

    private static ResourceManager _instance;

    public static ResourceManager Instance
    {
        get
        { 
            if(_instance == null)
            {
                _instance = FindObjectOfType<ResourceManager>();
                if(_instance == null)
                {
                    GameObject ResourceManager = new GameObject("ResourceManager");
                    _instance = ResourceManager.AddComponent<ResourceManager>();
                }
            }
            return _instance;
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
        }
        get { return spawnPoints; }
    }
    public int MaxSpawnPoints
    {
        set
        {
            maxSpawnPoints = value;
        }
        get { return maxSpawnPoints; }
    }
    IEnumerator EarnPoints()
    {
        while (true)
        {
            if (spawnPoints >= maxSpawnPoints)
            {
                Debug.Log("maxPoints");
                
            }
            else
            {
                spawnPoints += 5;
                UIManager.Instance.UpdatePoint();
            }
            yield return new WaitForSeconds(0.5f);

        }
    }
}
