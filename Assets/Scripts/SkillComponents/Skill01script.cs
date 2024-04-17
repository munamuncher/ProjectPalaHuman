using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Skill01script : SkillComponent
{
    public override void Activate()
    {
        if(GameManager.Inst.CanCast(10))
        {
            GameManager.Inst.isCast(10);
            GameManager.Inst.SpawnPoints += 10;
        }
        else
        {
            Debug.Log("not enough to activate skill01");
        }
    }
}
