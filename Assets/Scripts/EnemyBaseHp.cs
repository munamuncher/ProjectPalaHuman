using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBaseHp : MonoBehaviour ,IObserver
{
    [SerializeField]
    SubjectScript _EnemyBase;
    [SerializeField]
    private Image enemyBaseHP;
    [SerializeField]
    private Image enemyBaseUIHP;
    private EnemeyBase eb;
    private GameObject Base;
    public void OnEnable()
    {
        _EnemyBase.AddObserver(this);
        Base = GameObject.FindGameObjectWithTag("EnemyBase");
        if (Base.TryGetComponent<EnemeyBase>(out eb))
        {
            Debug.Log("gotten eb");
        }
        else
        {
            Debug.Log("cannot be found");
        }

    }
    public void OnDisable()
    {
        _EnemyBase.RemoveObserver(this);
    }
    public void OnNotify()
    {
        ReduceHpbar(eb.HP, eb.maxHP);
    }

    private void ReduceHpbar(float currentHP, float maxHealth)
    {
        if (currentHP > 0)
        {
            float healthPercentage = (float)currentHP / maxHealth;
            enemyBaseHP.fillAmount = healthPercentage;
            enemyBaseUIHP.fillAmount = healthPercentage;

        }
        else
            enemyBaseHP.fillAmount = 0;
    }

}
