using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyBase : SubjectScript , IDamageable
{
    public float maxHP;
    public float HP;

    public float Healths { get; set;}


    private void Start()
    {
        maxHP = 1000f;
        HP = maxHP;
        Healths = HP;
    }
    public void Damage(float Amount)
    {
        Healths -= Amount;
        HP = Healths;
        Debug.Log(gameObject.name + " has taken" + Amount + "Damage" + HP + "remainging");
        notifyObservers();
        if (HP <= 0)
        {
            baseExplode();
        }
    }

    private void baseExplode()
    {
        Destroy(gameObject);
    }
}
