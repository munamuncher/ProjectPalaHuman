using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AllyUnitMovement : MonoBehaviour , IDamageable
{
    [SerializeField] private int allyUnitID;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float health;
    [SerializeField] private float ADamage;

    private JsonReader.UnitData unitData;
    private CapsuleCollider2D ccd;
    private Rigidbody2D rg;
    [SerializeField]
    private GameObject enemyTarget;
    private bool canAttack = true;
    private Animator anim;

    public float Healths { get; set; }
    #region Awake
    private void Awake()
    {
        if (!TryGetComponent<Rigidbody2D>(out rg))
        {
            Debug.Log("AllyUnitMovement.cs - Awake() - RigidBody참조 실패");
        }
        else
        {
            rg.simulated = true;
            rg.gravityScale = 0f;
        }

        if (!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.Log("AllyUnitMovement.cs - Awake() - CapsuleCollider2D참조 실패");
        }
        if (!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("AllyUnitMovement.cs - Awake() - Animator참조 실패");
        }
        else
        {
            ccd.isTrigger = true;
            ccd.offset = new Vector2(0, 0.8f);
            ccd.size = new Vector2(0.5f, 3f);
        }
    }
    #endregion
    private void Start()
    {
        LoadUnitData();
        health = unitData.Health;
        Healths = health;
        moveSpeed = unitData.MoveSpeed;
        attackRange = unitData.AttackRange;
        attackSpeed = unitData.AttackSpeed;
        ADamage = unitData.Damage;
        if (unitData == null)
        {
            Debug.LogError("Unit data not loaded properly!");
            return;
        }
        else
        {
            Debug.Log("its working");
        }
    }
    private void FixedUpdate()
    {
        DetectTarget();
        if (enemyTarget == null)
        {
            MoveRight();
            moveSpeed = 2f;
        }
        else
        {
            anim.SetFloat("Speeds", 0f);
            if (canAttack)
            {
                moveSpeed = 0f;
                StartCoroutine(AttackEnemy());

            }
        }
    }
    private void DetectTarget()
    {
        int mask = (1 << 8)| (1<<9);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, mask);
        if (hit)
        {
            enemyTarget = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.white);
        }
        else
        {
            enemyTarget = null;
        }
    }
    private void MoveRight()
    {
        transform.Translate(Vector2.left * (Time.deltaTime * moveSpeed));
        anim.SetFloat("Speeds", 1f);
    }

    private IEnumerator AttackEnemy()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        if (enemyTarget.TryGetComponent(out IDamageable hits))
        {
            hits.Damage(ADamage);
        }
        yield return new WaitForSeconds(attackSpeed);        

        canAttack = true;

    }

    public void Damage(float DamageAmount)
    {
        Healths -= DamageAmount;
        health = Healths;
        Debug.Log(gameObject.name + " has taken" + DamageAmount + "Damage" + health + "remainging");
        AllyDead();
    }
    public void AllyDead()
    {
        if (Healths <= 0)
        {
            Debug.Log(gameObject + ("is Dead"));
            Destroy(gameObject);
        }
    }

    private void LoadUnitData()
    {
        JsonReader jsonReader = JsonReader.Insts;
        if (jsonReader != null)
        {
            foreach (JsonReader.UnitData unit in jsonReader.unitsData.units)
            {
                if (unit.ID == allyUnitID)
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
