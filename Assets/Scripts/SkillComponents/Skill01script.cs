using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Skill01script : SkillComponent
{

    private void Start()
    {
        costOfMana = 10;
        skillCoolDown = 1f;
        skillDmg = 0f;
    }
    public override void Activate()
    {
        if (CanCast(costOfMana))
        {
            Cast(costOfMana);
            Skill01Activate();
            Debug.Log("Skill01 has been activated.");
        }
        else
        {
            Debug.Log("Not enough mana to activate Skill01.");
        }
    }

    private void Skill01Activate()
    {
        GameManager.Inst.SpawnPoints += 10;
    }
}
