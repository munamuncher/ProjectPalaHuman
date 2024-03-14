using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AllyUnitMovement : MonoBehaviour , IDamageable
{
    private CapsuleCollider2D ccd;
    private Rigidbody2D rg;
    [SerializeField]
    private GameObject enemyTarget;
    private bool canAttack = true;
    private Animator anim;
    private float AttackCoolDown = 3f;
    private float moveSpeed;
    [SerializeField]
    private int AllyHp = 100;

    public int Health { get; set; }
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
    private void FixedUpdate()
    {
        Health = AllyHp;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, mask);
        if (hit)
        {
            Debug.Log(hit.collider.gameObject.layer);
            Debug.Log("ally Hit => " + hit.collider.gameObject.name);
            enemyTarget = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.white);
        }
        else
        {
            Debug.Log("No target hit");
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
        Debug.Log("ally Attacking");
        anim.SetTrigger("Attack");
        if (enemyTarget.TryGetComponent(out IDamageable hits))
        {
            hits.Damage(20);
        }
        yield return new WaitForSeconds(AttackCoolDown);
        canAttack = true;

    }

    public void Damage(int DamageAmount)
    {
        Health -= DamageAmount;
        AllyHp = Health;
        Debug.Log("Monster took " + DamageAmount + " damage. Remaining health: " + Health);
        AllyDead();
    }
    public void AllyDead()
    {
        if (Health <= 0)
        {
            Debug.Log(gameObject + ("is Dead"));
            Destroy(gameObject);
        }
    }
}
