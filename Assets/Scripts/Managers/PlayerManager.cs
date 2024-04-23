using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int manaPoint;
    public int maxManaPoint { get; set; }
    public int playerLevel { get; set; }
    public int expMaxPoint { get; set; }
    public int expPoints { get; set; }
    public bool hasLevelUp { get; set; }


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
            UIManager.Instance.UpdateMana();

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
                UIManager.Instance.UpdateMana();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
