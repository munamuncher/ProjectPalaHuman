using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : SubjectScript , IDamageable 
{
    private Rigidbody2D rg;
    private CapsuleCollider2D ccd;
    private SpriteRenderer sr;
    public Animator anim;
    Vector3 Move;
    private float moveSpeed =5f;
    //[SerializeField]
    //private float moveDir;
    public float maxHP;
    public float HP;
    private bool isDead;


    public float Healths { get; set; }
    #region Awake
    private void Awake()
    {

        if (!TryGetComponent<Rigidbody2D>(out rg))
        {
            Debug.Log("PlayerMovement.cs - Awake() - RigidBody참조 실패");
        }
        else
        {
            rg.simulated = true;
            rg.gravityScale = 0f;
        }

        if(!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.Log("PlayerMovement.cs - Awake() - CapsuleCollider2D참조 실패");
        }
        else
        {
            ccd.enabled = true;
            ccd.isTrigger = true;
            ccd.size = new Vector2(0.5f, 2f);
        }
        if(!TryGetComponent<SpriteRenderer>(out sr))
        {
            Debug.Log("PlayerMovement.cs - Awake() - SpriteRenderer참조 실패");
        }
        if (!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("PlayerMovement.cs - Awake() - Animator참조 실패");
        }
        transform.position = new Vector3(-19f, 1f, 0f);
    }
    #endregion



    private void Start()
    {
        maxHP = 200f;
        HP = maxHP;
        Healths = HP; 
        isDead = false;
        

    }



    private void OnPlayerAction(InputValue value)
    {
        Vector2 moveDir = value.Get<Vector2>();
        Move = new Vector3(moveDir.x, 0f,0f);
        Move.x = Mathf.Clamp(Move.x, -19.15f, 17.15f);

        if (moveDir.x < 0f)
        {
            anim.SetFloat("Moving", 1f);
            sr.flipX = true;
        }
        else if (moveDir.x > 0f)
        {
            anim.SetFloat("Moving", 1f);
            sr.flipX = false;
        }
        else if (moveDir.x == 0f)
        {
            anim.SetFloat("Moving", 0f);
        }
    }
    private void FixedUpdate()
    { 
        Vector3 newPosition = transform.position + new Vector3(Move.x * (Time.deltaTime * moveSpeed), 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, -19.15f, 17.15f);
        transform.position = newPosition;
    }
    public void Damage(float DamageAmount)
    {
        if (!isDead)
        {
            //anim.SetTrigger("HasBeenHit");
            Healths -= DamageAmount;
            HP = Healths;
            Debug.Log(gameObject.name + " has taken" + DamageAmount + "Damage" + HP + "remainging");
            notifyObservers();
            
            if (Healths <= 0)
            {
                isDead = true;
            }
        }
        else
        {
            playerDeath();
        }
    }

    private void playerDeath()
    {
            ccd.enabled = false;
            Debug.Log(gameObject + ("is Dead"));
            anim.SetTrigger("Death");
            Invoke("death", 3.5f);

    }
    private void death()
    {
        GameManager.Inst.StateOfGame(GameState.GameOver);
    }
}
