using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{

    public abstract void Activate();

    protected int skillLevel;
    protected int costOfMana;
    protected float skillCoolDown;
    protected float skillDmg;

    protected bool CanCast(int mpCost)
    {
        return PlayerManager.Instance.ManaPoints >= mpCost;
    }

    protected void Cast(int mpCost)
    {
        PlayerManager.Instance.ManaPoints -= mpCost;
    }
    private void ActivateSkill()
    {
        if (CanCast(costOfMana))
        {
            Cast(costOfMana);
            Activate();
        }
        else
        {
            Debug.Log("Not enough mana to cast the skill.");
        }
    }
}
