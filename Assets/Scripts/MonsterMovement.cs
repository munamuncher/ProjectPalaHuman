using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterMovement : MonoBehaviour,IDamageable
{
    private JsonReader.UnitData myData;
    public JsonReader.UnitData MyData
    {
        set
        {
            myData = value;
        }
    }
    [SerializeField]
    private GameObject target;
    private Rigidbody2D rb;
    private CapsuleCollider2D ccd;
    private Animator anim;
    private bool canAttack = true;
    


    public float Healths { get; set; }
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
        Healths = myData.Health;
        Debug.Log("unit stats are" + " " + myData.Damage + " " + myData.AttackRange + " " + myData.AttackSpeed + " " + myData.Cost + myData.Health + " " + myData.Name);
    }
    private void FixedUpdate()
    {
        rayCastTarget();
        if (target == null)
        {
            moveLeft();
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0f);
            if (canAttack)
            {
                StartCoroutine(MAttack());

            }
        }
    }
    #region RayCast

    private void rayCastTarget()
    {
        int mask = (1 << 7) | (1 << 6);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, myData.AttackRange, mask);
        if (hit)
        {
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
        transform.Translate(Vector3.left * Time.deltaTime * myData.MoveSpeed);
        anim.SetFloat("MoveSpeed", 1f);
    }

    private IEnumerator MAttack()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        if (target.TryGetComponent(out IDamageable hits))
        {
            hits.Damage(myData.Damage);
        }
        yield return new WaitForSeconds(myData.AttackSpeed);
        canAttack = true;
    }
    #endregion
    public void Damage(float DamageAmount)
    {
        Healths -= DamageAmount;
        Debug.Log(gameObject.name + " has taken" + DamageAmount + "Damage" + Healths + "remainging");
        Dead();
    }
    public void Dead()
    {
        if (Healths <= 0)
        {
            Debug.Log(gameObject + ("is Dead"));
            Destroy(gameObject);
        }
    }
}
