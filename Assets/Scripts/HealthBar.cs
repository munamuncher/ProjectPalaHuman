using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private int health;
    private int maxHealth;

    private static HealthBar _inst;

    public static HealthBar _Inst => _inst;

    [SerializeField]
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        health = currentHealth;
        this.maxHealth = maxHealth;

        //observers.OnNotify();
    }


}
