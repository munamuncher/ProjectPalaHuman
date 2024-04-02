using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour, IObserver
{
    [SerializeField] SubjectScript _playerSubject;
    [SerializeField]
    private Image PlayerHpbar;
    private PlayerMovement pm;
    private GameObject player;

    public void OnEnable()
    {
        _playerSubject.AddObserver(this);
        player = GameObject.FindGameObjectWithTag("Player");
        if(player.TryGetComponent<PlayerMovement>(out pm))
        {
            Debug.Log("gotten");
        }

    }
    public void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    } 
    public void OnNotify()
    {
        ReduceHpbar(pm.HP);
        Debug.Log(pm.HP);
        Debug.Log("HealthBar has been noticed changing Healthbar");
    }

    private void ReduceHpbar(float currentHP)
    {
        float maxHealth = currentHP;
        if (currentHP > 0)
        {
            
            float healthPercentage = (float)currentHP/maxHealth;
            PlayerHpbar.fillAmount = healthPercentage;
        }
        else
            PlayerHpbar.fillAmount = 0;
    }

}

