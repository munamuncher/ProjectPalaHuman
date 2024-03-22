using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthbar;
    private int health;
    private int maxHealth;

    private static HealthBar _inst;

    public static HealthBar _Inst => _inst;

    [SerializeField]
    private List<HealthBar> observers = new List<HealthBar>();

    public void AddObserver(HealthBar observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(HealthBar observer)
    {
        observers.Remove(observer);
    }
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        health = currentHealth;
        this.maxHealth = maxHealth;
        displayBar();
    }

    private void displayBar()
    {
        if(maxHealth !=0)
        {
            float healthPercentage = (float)health/maxHealth;
            healthbar.fillAmount = healthPercentage;
        }
        else
            healthbar.fillAmount = 0;
    }

}
