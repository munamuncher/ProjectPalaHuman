using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill02script : SkillComponent, IDamageable
{

    [SerializeField]
    private GameObject skillDetection;
    private float duration = 5f;
    private int enemeyCount;

    private PlayerMovement pm;
    public float Healths { get; set; }

    private void OnDrawGizmosSelected()
    {
        if (skillDetection != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(skillDetection.transform.position, new Vector3(3f, 1f, 0f));
        }
    }
    private void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();
        if (!pm)
        {
            Debug.Log("playerMovement.cs 참조 실패");
        }
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
        int mask = (1 << 8) | (1 << 9);
        if (skillDetection == null)
        {
            Debug.LogError("Skill Detection GameObject is not set.");
            yield break;
        }
        while (duration > 0)
        {
            //pm.anim.SetTrigger("skill02");   todo animManager 완료후 넣을 에정
            Collider2D[] colliders = Physics2D.OverlapBoxAll(skillDetection.transform.position, new Vector2(3f, 1f), 0f, mask);
            foreach (Collider2D collider in colliders)
            {
                GameObject obj = collider.gameObject;
                if (obj != null && obj.TryGetComponent(out IDamageable hits))
                {
                    hits.Damage(10f);
                }
                Debug.Log("Detecting " + obj.name);
            }
            duration -= 1;
            yield return new WaitForSeconds(1f);

        }
        Resetskill();

    }

    private void Resetskill()
    {
        Debug.Log("reseting");
        duration = 5f;
    }
    public void Damage(float Amount)
    {

    }
}
