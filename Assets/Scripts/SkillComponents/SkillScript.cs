using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillScript : MonoBehaviour
{
    public SkillComponent[] skillComponents;
    private void Start()
    {
        InitializeSkillComponents();
        
        
    }

    private void Update()
    {
        CheckSkillActivation();
    }


    private void InitializeSkillComponents()
    {
        foreach (SkillComponent component in skillComponents)
        {
            component.Activate();
        }
    }
    private void CheckSkillActivation()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            ActivateSkill<Skill01script>();
        }
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            ActivateSkill<Skill02script>();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            ActivateSkill<Skill03script>();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ActivateSkill<Skill04script>();
        }
    }
    // 스킬 사용 계선 필요
    private void ActivateSkill<T>() where T : SkillComponent
    {
        T[] skillComponentsOfType = FindObjectsOfType<T>();

        foreach (T component in skillComponentsOfType)
        {
            component.Activate();
        }
    }
}
