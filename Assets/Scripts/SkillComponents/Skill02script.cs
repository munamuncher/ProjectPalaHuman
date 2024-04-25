using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill02script : SkillComponent, IDamageable
{

    [SerializeField]
    private GameObject skillDetection;
    [SerializeField]
    private bool FireIsOn;
    private float duration = 5f;
    private int enemeyCount;
    [SerializeField]
    private List<GameObject> targetList = new List<GameObject>();
    public float Healths { get; set; }

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
        FireIsOn = false;
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
            FireIsOn = true;
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

        while (true)
        {
            if (skillDetection == null)
            {
                Debug.LogError("Skill Detection GameObject is not set.");
                yield break;
            }
            while (duration > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(skillDetection.transform.position, new Vector2(2f, 1f), 0f, mask);
                foreach (Collider2D collider in colliders)
                {
                    GameObject obj = collider.gameObject;
                    targetList.Add(obj);           
                    Debug.Log(targetList.Count);
                }
                duration -= Time.deltaTime;
                Debug.Log(duration);

            }
            while(duration > 0)
            {
                Debug.Log("doing dmg doing dmg");
                yield return new WaitForSeconds(1f);
            }
            if (duration < 0)
            {
                yield return null;
            }
        }
    }

    public void Damage(float Amount)
    {
        
    }
}
