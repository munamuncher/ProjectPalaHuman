using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterMovement : MonoBehaviour, IDamageable
{
    [SerializeField] private int monsterID;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float health;
    [SerializeField] private float MDamage;

    private JsonReader.UnitData unitData;
    [SerializeField]
    private GameObject target;
    private Rigidbody2D rb;
    private CapsuleCollider2D ccd;
    private Animator anim;
    private bool canAttack = true;



    public float Health { get; set; }
    #region Awake
    private void Awake()
    {

        if (!TryGetComponent<Rigidbody2D>(out rb))
        {
            Debug.Log("MonsterMovement.cs - Awake() - rigidBody2D참조 실패");
        }
        else
        {
            rb.simulated = true;
            rb.gravityScale = 0f;
        }

        if (!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.Log("MonsterMovement.cs - Awake() - CapsuleCOllider2D참조 실패");
        }
        else
        {
            ccd.isTrigger = true;
            ccd.size = new Vector2(0.5f, 1.5f);
        }
        //isSpawned = false;
        if (!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("MonsterMovement.cs - Awake() - Animator참조 실패");
        }
    }
    #endregion
    private void Start()
    {
        LoadUnitData();
        health = unitData.Health;
        Health = health;
        moveSpeed = unitData.Movespeed;
        attackRange = unitData.AttackRange;
        attackSpeed = unitData.AttackSpeed;
        MDamage = unitData.Damage;
    }
    private void FixedUpdate()
    {
        rayCastTarget();
        if (target == null)
        {
            moveLeft();
            moveSpeed = 2f;
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0f);
            if (canAttack)
            {
                moveSpeed = 0f;
                StartCoroutine(MAttack());

            }
        }
    }
    #region RayCast

    private void rayCastTarget()
    {
        int mask = (1 << 7) | (1 << 6);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange, mask);
        if (hit)
        {
            Debug.Log(hit.collider.gameObject.layer);
            target = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.white);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.red);
            target = null;
        }

    }
    private void moveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        anim.SetFloat("MoveSpeed", 1f);
    }

    private IEnumerator MAttack()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        if (target.TryGetComponent(out IDamageable hits))
        {
            hits.Damage(MDamage);
        }
        yield return new WaitForSeconds(attackSpeed);        
        canAttack = true;
    }
    #endregion
    public void Damage(float DamageAmount)
    {
        Health -= DamageAmount;
        health = Health;
        Debug.Log("Monster took " + DamageAmount + " damage. Remaining health: " + Health);
        Dead();
    }
    public void Dead()
    {
        if (Health <= 0)
        {
            Debug.Log(gameObject + ("is Dead"));
            Destroy(gameObject);
        }
    }

    private void LoadUnitData()
    {
        JsonReader jsonReader = JsonReader.Instance;
        if (jsonReader != null)
        {
            Debug.Log(jsonReader.unitsData.units.Length);
            foreach (JsonReader.UnitData unit in jsonReader.unitsData.units)
            {
                if (unit.ID == monsterID)
                {
                    unitData = unit;
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("not found Component");
        }
    }
}
