using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour, IObserver
{
    [SerializeField] SubjectScript _playerSubject;
    [SerializeField]
    private Image PlayerHpbar;
    [SerializeField]
    private Image PlayerUIHPbar;
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
        else
        {
            Debug.Log("cannot be found");
        }

    }
    public void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    } 
    public void OnNotify()
    {
        ReduceHpbar(pm.HP,pm.maxHP);
    }

    private void ReduceHpbar(float currentHP, float maxHealth)
    {
        if (currentHP > 0)
        {
            float healthPercentage = (float)currentHP/maxHealth;
            PlayerHpbar.fillAmount = healthPercentage;
            PlayerUIHPbar.fillAmount = healthPercentage;

        }
        else
            PlayerHpbar.fillAmount = 0;
    }

}

