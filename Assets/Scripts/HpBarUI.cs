using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour, IObserver
{
    [SerializeField]
    private Image PlayerHpbar;
    private int health;
    private int maxHealth;

    public void OnNotify()
    {
        ReduceHpbar();
    }

    private void ReduceHpbar()
    {
        if (maxHealth != 0)
        {
            float healthPercentage = (float)health / maxHealth;
            PlayerHpbar.fillAmount = healthPercentage;
        }
        else
            PlayerHpbar.fillAmount = 0;
    }

}

