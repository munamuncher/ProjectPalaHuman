using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterMovement : MonoBehaviour
{
    private Rigidbody2D rg;
    private CapsuleCollider2D ccd;
    private float moveSpeed = 2f;
    //private bool isSpawned;
    private Animator anim;

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

        if(!TryGetComponent<CapsuleCollider2D>(out ccd))
        {
            Debug.Log("MonsterMovement.cs - Awake() - CapsuleCOllider2D참조 실패");
        }
        else
        {
            ccd.isTrigger = true;
            ccd.size = new Vector2(0.5f, 1.5f);
        }
        //isSpawned = false;
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("MonsterMovement.cs - Awake() - Animator참조 실패");
        }
    }

    private void Update()
    {
        //if(isSpawned)
        moveLeft();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetFloat("MoveSpeed", 0f);
        }
    }

    private void moveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        anim.SetFloat("MoveSpeed", 1f);
    }


}
