using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill04script : SkillComponent
{
    private void Start()
    {
        costOfMana = 200;
        skillCoolDown = 30f;
        skillDmg = 500;
    }
    public override void Activate()
    {
        if (CanCast(costOfMana))
        {
            Cast(costOfMana);
            Skill04Activate();
            Debug.Log("Skill02 has been activated.");
        }
        else
        {
            Debug.Log("Not enough mana to activate Skill02.");
        }
    }

    private void Skill04Activate()
    {
        Debug.Log("skill04 has been activated");
    }
}
