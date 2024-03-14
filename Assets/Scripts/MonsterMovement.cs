using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterMovement : MonoBehaviour , IDamageable
{
    private Rigidbody2D rg;
    private CapsuleCollider2D ccd;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject Target;
    //private bool isSpawned;
    private Animator anim;
    private bool canAttack = true;
    private float AttackCoolDown = 3f;
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    private int MonHeatlh = 100;

    public int Health { get; set;}
    #region Awake
    private void Awake()
    {
        Health = MonHeatlh;
        if (!TryGetComponent<Rigidbody2D>(out rg))
        {
            Debug.Log("MonsterMovement.cs - Awake() - rigidBody2D참조 실패");
        }
        else
        {
            rg.simulated = true;
            rg.gravityScale = 0f;
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

    private void FixedUpdate()
    {
        rayCastTarget();
        if (Target == null)
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
                StartCoroutine(Attack());

            }
        }
    }
    #region RayCast

    private void rayCastTarget()
    {
        int mask = (1 << 7) | (1 << 6) ;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,0.5f, mask);
        if (hit)
        {
            Debug.Log(hit.collider.gameObject.layer);
            Debug.Log("Hit => " + hit.collider.gameObject.name);
            Target = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.white);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.red);
            Debug.Log("No target hit");
            Target = null;
        }

    }
    private void moveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        anim.SetFloat("MoveSpeed", 1f);
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        Debug.Log("Attacking");
        anim.SetTrigger("Attack");
        if (Target.TryGetComponent(out IDamageable hits))
        {
            hits.Damage(20);
        }
        yield return new WaitForSeconds(AttackCoolDown);
        canAttack = true;


    }
    #endregion
    public void Damage(int DamageAmount)
    {
        Health -= DamageAmount;
        MonHeatlh = Health;
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

}
