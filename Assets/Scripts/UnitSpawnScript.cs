using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static JsonReader;
public enum Faction
{ 
    Ally,
    Enemy,
}

public class UnitSpawnScript : MonoBehaviour
{
    [SerializeField]
    private UnitStatusScriptableObject usd;
    [SerializeField]
    private GameObject[] unitPrefabs;
    private Dictionary<int , GameObject> unitPrefabDictionary = new Dictionary<int , GameObject>();
    [SerializeField]
    private List<Transform> enemySpawnPoints;
    [SerializeField]
    private List<Transform> allySpawnPoints;
    private MonsterMovement enemyUnitScript;
    private AllyUnitMovement allyUnitScript;
    private static UnitSpawnScript _instances;
    public static UnitSpawnScript _Instances => _instances;

    private void Awake()
    {
        if (_instances && _instances != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instances = this;
            DontDestroyOnLoad(gameObject);
        }
        foreach (GameObject prefab in unitPrefabs)
        {
            for (int i = 0; i < usd.UnitData.Length; i++)
            {
                if (usd.UnitData != null)
                {
                    unitPrefabDictionary[usd.UnitData[i].ID] = prefab;
                }

            }

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnUnit(10001,Faction.Enemy);
        }

    }

    public void SpawnUnit(int id , Faction faction)
    {
        List<Transform> spawnPoints = faction == Faction.Enemy ? enemySpawnPoints : allySpawnPoints;
        int ran = Random.Range(0, spawnPoints.Count);
        if(usd!=null && unitPrefabDictionary.ContainsKey(id))
        {
            GameObject newUnit = Instantiate(unitPrefabDictionary[id], spawnPoints[ran].position, Quaternion.identity);
            if(newUnit.TryGetComponent<AllyUnitMovement>(out allyUnitScript))
            {
                allyUnitScript._MyData = usd.UnitData[0];
            }
            else if(newUnit.TryGetComponent<MonsterMovement>(out enemyUnitScript))
            {
                enemyUnitScript.MyData = usd.UnitData[id];
            }
        }
        else
        {
            Debug.LogError("UnitStatusScriptableObject not assigned or unit ID not found!");
        }
    }
}
