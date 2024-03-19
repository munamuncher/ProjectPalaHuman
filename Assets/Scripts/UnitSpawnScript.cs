using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnScript : MonoBehaviour
{
    [SerializeField]
    private UnitStatusScriptableObject usd;
    [SerializeField]
    private GameObject[] unitPrefab;
    private MonsterMovement unitScript;
    [SerializeField]
    private Transform[] SpawnPoints;
    private int spawnid;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnUnit(10001);
            spawnid = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnUnit(10002);
            spawnid = 1;
        }

    }
    public void SpawnUnit(int id)
    {
        
        if (usd != null)
        {
            
            GameObject newUnit = Instantiate(unitPrefab[spawnid], transform.position, Quaternion.identity);
            if (newUnit.TryGetComponent<MonsterMovement>(out unitScript))
            {
                for(int i = 0; i< usd.UnitData.Length; i++)
                {
                    if(usd.UnitData[i].ID == id)
                    {
                        unitScript.MyData = usd.UnitData[i];
                    }
                }
            }
           
        }
        else
        {
            Debug.LogError("UnitStatusScriptableObject not assigned!");
        }
    }

}
