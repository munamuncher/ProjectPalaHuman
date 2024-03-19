using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Unit Stat", menuName = "ScriptableObject/UnitStatusScriptableObject" ,order = 1)]
public class UnitStatusScriptableObject : ScriptableObject
{

    //한번 저장된 데이터는 지워지지 않는다
    [SerializeField]
    private JsonReader.UnitData[] unitData;
    public JsonReader.UnitData[] UnitData
    {
        get => unitData;
    }

    #region UpdateUnitCode
    public void BringData(int id)
    {
        JsonReader jsonReader = JsonReader.Insts;
        if (jsonReader != null)
        {
            unitData = jsonReader.unitsData.units;
            if (unitData != null)
            {
                foreach (JsonReader.UnitData data in unitData)
                {
                    if (data.ID == id)
                    {
                        Debug.Log("Unit ID: " + data.ID);
                        Debug.Log("Unit Name: " + data.Name);
                        Debug.Log("Unit Health: " + data.Health);
                        Debug.Log("Unit MoveSpeed: " + data.MoveSpeed);
                        Debug.Log("Unit AttackRange: " + data.AttackRange);
                        Debug.Log("Unit AttackSpeed: " + data.AttackSpeed);
                        Debug.Log("Unit Damage: " + data.Damage);

                    }
                }
            }
            else
                Debug.LogError("unit data null");
        }
        else
            Debug.LogError("json file cant be found");
    }
    #endregion
}
