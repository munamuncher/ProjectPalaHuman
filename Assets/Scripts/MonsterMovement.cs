using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterMovement : MonoBehaviour
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
    #region Awake
    private void Awake()
    {
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

    private void Update()
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
    [SerializeField]
    Transform tr;
    int mask = (1 << 6);
    private void rayCastTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(tr.position, Vector2.left, 0.5f, mask);
        if (Physics.Raycast(tr.position,Vector2.left , 0.5f, mask))
        {
            Debug.Log("Hit => " + hit.collider.gameObject.name);
        }
        Debug.DrawRay(transform.position, Vector2.left, Color.blue, 0.5f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("AllyForce") || hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log(hit.collider.gameObject.name);
                Target = hit.collider.gameObject;
                Debug.Log("Working");
                Debug.DrawRay(transform.position, Vector2.left, Color.white, 0.5f);
            }
        }
        else
        {
            Debug.Log("null");
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
        yield return new WaitForSeconds(AttackCoolDown);
        canAttack = true;


    }
}
