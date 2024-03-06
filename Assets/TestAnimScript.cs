using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimScript : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("TestAnimScript.cs - Awake() - Animator참조 실패");
        }
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
}
