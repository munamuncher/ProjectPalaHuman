using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset MonsterData;
    public Units unitsData;
    private static JsonReader instance;
    public static JsonReader Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<JsonReader>();


                if (instance == null)
                {
                    GameObject obj = new GameObject("JsonReader");
                    instance = obj.AddComponent<JsonReader>();
                }
            }
            return instance;
        }
    }


    void Start()
    {

        unitsData = JsonUtility.FromJson<Units>(MonsterData.text);
        foreach (UnitData unit in unitsData.units)
        {
            Debug.Log("Unit ID: " + unit.ID);
            Debug.Log("Unit Name: " + unit.Name);
            Debug.Log("Unit Health: " + unit.Health);
            Debug.Log("Unit Damage: " + unit.Damage);
            Debug.Log("Unit MoveSpeed: " + unit.Movespeed);
            Debug.Log("Unit AttackSpeed: " + unit.AttackSpeed);
            Debug.Log("Unit AttackRange: " + unit.AttackRange);
            Debug.Log("Unit Cost: " + unit.Cost);
        }
    }
    [System.Serializable]
    public class UnitData
    {
        public int ID;
        public string Name;
        public float Health;
        public float Damage;
        public float Movespeed;
        public float AttackSpeed;
        public float AttackRange;
        public int Cost;
    }

    [System.Serializable]
    public class Units
    {
        public UnitData[] units;
    }
    public Units MyUnitList = new Units();

}