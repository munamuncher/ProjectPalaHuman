using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AllyUnitMovement : MonoBehaviour , IDamageable
{
    private JsonReader.UnitData _myData;
    public JsonReader.UnitData _MyData
    {
        set
        {
            _myData = value;
        }
    }
    private JsonReader.UnitData unitData;
    private CapsuleCollider2D ccd;
    private Rigidbody2D rg;

    [SerializeField]
    private GameObject enemyTarget;
    [SerializeField]
    private float maxHealth;
    private bool canAttack = true;
    private Animator anim;
    [SerializeField]
    private Image AllyHPbar;

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
        maxHealth = _myData.Health;
        Healths = maxHealth;
        //Debug.Log("unit stats are" + " " + _myData.Damage + " " + _myData.AttackRange + " " + _myData.AttackSpeed + " " + _myData.Cost + _myData.Health + " " + _myData.Name);
    }
    private void FixedUpdate()
    {
        DetectTarget();
        if (enemyTarget == null)
        {
            MoveRight();

        }
        else
        {
            anim.SetFloat("Speeds", 0f);
            if (canAttack)
            {
                StartCoroutine(AttackEnemy());
            }
        }

    }
    private void DetectTarget()
    {
        int mask = (1 << 8)| (1<<9);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _myData.AttackRange * 2 , mask);
        Vector2 endPoint = hit.collider ? hit.point : (Vector2)transform.position + Vector2.right * _myData.AttackRange * 2;
        Debug.DrawLine(transform.position, endPoint, Color.red);
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
        transform.Translate(Vector2.left * (Time.deltaTime * _myData.MoveSpeed));
        anim.SetFloat("Speeds", 1f);
    }

    private IEnumerator AttackEnemy()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        if (enemyTarget.TryGetComponent(out IDamageable hits))
        {
            hits.Damage(_myData.Damage);
        }
        yield return new WaitForSeconds(_myData.AttackSpeed);        

        canAttack = true;

    }

    public void Damage(float DamageAmount)
    {
        Healths -= DamageAmount;
        Debug.Log(gameObject.name + " has taken" + DamageAmount + "Damage" + Healths + "remainging");
        ReduceMonHp(Healths, _myData.Health);
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

    private void ReduceMonHp(float currentHP, float maxHP)
    {
        if (currentHP > 0)
        {
            float healthPercentage = (float)currentHP / maxHP;
            AllyHPbar.fillAmount = healthPercentage;
            AllyHPbar.fillAmount = healthPercentage;

        }
        else
            AllyHPbar.fillAmount = 0;
    }
}
