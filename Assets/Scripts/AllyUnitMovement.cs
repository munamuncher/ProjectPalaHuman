using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AllyUnitMovement : MonoBehaviour
{
    private CapsuleCollider2D ccd;
    private Rigidbody2D rg;
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
        else
        {
            ccd.isTrigger = true;
            ccd.offset = new Vector2(0, 0.8f);
            ccd.size = new Vector2(0.5f, 3f);
        }
    }
}
