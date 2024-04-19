using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill03script : SkillComponent
{
    private void Start()
    {
        skillLevel = 1;
        costOfMana = 50;
        skillCoolDown = 15f;
        skillDmg = 0;
    }
    public override void Activate()
    {
        if (CanCast(costOfMana))
        {
            Cast(costOfMana);
            Skill03Activate();
            Debug.Log("Skill03 has been activated.");
        }
        else
        {
            Debug.Log("Not enough mana to activate Skill03.");
        }
    }

    private void Skill03Activate()
    {
        Debug.Log("shield has Been Given");
    }
}
