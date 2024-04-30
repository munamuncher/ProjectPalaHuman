using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Skill01script : SkillComponent
{
    private GameObject Player;
    private int increaseValue = 10;
    private void Start()
    {
        skillLevel = 1;
        costOfMana = 10;
        skillCoolDown = 1f;
        skillDmg = 0f;
        Player = GameObject.Find("Player");
        if(!Player)
        {
            Debug.Log("참조 실패");
        }
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
        ResourceManager.Instance.SpawnPoints += increaseValue;
        Player.TryGetComponent<Animator>(out Animator anim);
        anim.SetTrigger("Skill01");
    }
}
