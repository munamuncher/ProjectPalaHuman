using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
    private Dictionary<int, GameObject> unitPrefabDictionary = new Dictionary<int, GameObject>();
    [SerializeField]
    private List<Transform> enemySpawnPoints;
    [SerializeField]
    private List<Transform> allySpawnPoints;

    private MonsterMovement enemyUnitScript;
    private AllyUnitMovement allyUnitScript;
    private static UnitSpawnScript _instances;
    private ResourceManager rm;
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

        for (int i = 0; i < usd.UnitData.Length; i++)
        {
            if (usd.UnitData != null)
            {
                unitPrefabDictionary[usd.UnitData[i].ID] = unitPrefabs[i];
            }

        }


        //foreach (var pair in unitPrefabDictionary)
        //{
        //    Debug.Log("Key: " + pair.Key + ", Value: " + pair.Value);
        //}

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnUnit(10001, Faction.Enemy);
        }

    }

    public void SpawnUnit(int id, Faction faction)
    {
        rm = ResourceManager.Instance;
        List<Transform> spawnPoints = faction == Faction.Enemy ? enemySpawnPoints : allySpawnPoints;
        int ran = Random.Range(0, spawnPoints.Count);
        if (usd != null && unitPrefabDictionary.ContainsKey(id))
        {
            if (rm.CanBuy(usd.scriptableDictionary[id].Cost))
            {
                GameObject newUnit = Instantiate(unitPrefabDictionary[id], spawnPoints[ran].position, Quaternion.identity);
                SetLayer(ran, newUnit);
                newUnit.SetActive(true);
                Debug.Log(usd.scriptableDictionary[id].Cost);

                if (newUnit.TryGetComponent<AllyUnitMovement>(out allyUnitScript))
                {
                    rm.isBuy(usd.scriptableDictionary[id].Cost);
                    allyUnitScript._MyData = usd.scriptableDictionary[id];
                    newUnit.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                }


                else if (newUnit.TryGetComponent<MonsterMovement>(out enemyUnitScript))
                {
                    enemyUnitScript.MyData = usd.scriptableDictionary[id];
                }

            }
            else
            {
                Debug.Log("not enough SpawnPoints");
            }
        }
        else
        {
            Debug.LogError("UnitStatusScriptableObject not assigned or unit ID not found!");
        }
    }

    private void SetLayer(int spawnPosLayer , GameObject target)
    {
        Debug.Log("Setting layer of" + target + "retreived" + spawnPosLayer);
        if(!target.TryGetComponent<SortingGroup>(out SortingGroup sg))
        {
            Debug.Log("UnitSpawnScript.cs - SetLayer() - SortingGroup not Found");
        }
        sg.sortingOrder = spawnPosLayer + 1;
        Debug.Log("Setting layer of " + target.name + " to " + sg.sortingOrder);
    }
}
