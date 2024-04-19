using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill02script : SkillComponent
{

    [SerializeField]
    private GameObject skillDetection;


    private void OnDrawGizmosSelected()
    {
        if (skillDetection != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(skillDetection.transform.position, new Vector3(2f, 1f, 0f));
        }
    }
    private void Start()
    {
        skillLevel = 1;
        costOfMana = 20;
        skillCoolDown = 5f;
        skillDmg = 10f;
    }


    public override void Activate()
    {
        if (CanCast(costOfMana))
        {
            Cast(costOfMana);
            StartCoroutine(ActivationSkill());

            Debug.Log("Skill02 has been activated.");
        }
        else
        {
            Debug.Log("Not enough mana to activate Skill02.");
        }
    }
    IEnumerator ActivationSkill()
    {
        if (skillDetection == null)
        {
            Debug.LogError("Skill Detection GameObject is not set.");
            yield break;
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(skillDetection.transform.position, new Vector2(2f, 1f), 0f);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Overlap with: " + collider.name);
        }

        yield return new WaitForSeconds(skillCoolDown);
    }
}
