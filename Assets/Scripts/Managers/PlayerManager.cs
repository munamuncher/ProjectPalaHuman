using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int manaPoint; 
    private int maxManaPoint; 
    private int playerLevel;  
    private int expMaxPoint; 
    private int expPoints;  
    private bool hasLevelUp;


    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();

                
                if (_instance == null)
                {
                    GameObject PlayerManager = new GameObject("PlayerManager");
                    _instance = PlayerManager.AddComponent<PlayerManager>();
                }
            }
            return _instance;
        }
    }

    public int ManaPoints
    {
        set
        {
            manaPoint = value;
            //UpdateMana();

        }
        get { return manaPoint; }
    }
    IEnumerator EarnMana()
    {

        while (true)
        {
            if (manaPoint >= maxManaPoint)
            {
                Debug.Log("mana is full");

            }
            else
            {
                manaPoint += 5;
                //UpdateMana();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
