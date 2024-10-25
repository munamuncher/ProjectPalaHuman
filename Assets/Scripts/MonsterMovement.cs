using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Image enemyHPBar;
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
            ccd.size = new Vector2(0.5f, 3f);
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
        Vector2 endPoint = hit.collider ? hit.point : (Vector2)transform.position + Vector2.left * myData.AttackRange * 2;
        Debug.DrawLine(transform.position, endPoint, Color.red);
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
        yield return new WaitForSeconds(0.5f);
        if (target != null && target.TryGetComponent(out IDamageable hits)) //항상 null 확인
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
        ReduceMonHp(Healths, myData.Health);
        Dead();
    }
    public void Dead()
    {
        if (Healths < 0)
        {
            GameManager.Inst.DeathExpUp();
            Destroy(gameObject);
        }
    }

    private void ReduceMonHp(float currentHP, float maxHP)
    {
        if (currentHP > 0)
        {
            Debug.Log(currentHP);
            float healthPercentage = (float)currentHP / maxHP;
            enemyHPBar.fillAmount = healthPercentage;
            enemyHPBar.fillAmount = healthPercentage;

        }
        else
            enemyHPBar.fillAmount = 0;
    }
}
