using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    public SkillComponent[] skillComponents;
    [Header("Skill Inputs")]
    [SerializeField] private string skill01Input = "Skill01";
    [SerializeField] private string skill02Input = "Skill02";
    [SerializeField] private string skill03Input = "Skill03";
    [SerializeField] private string skill04Input = "Skill04";

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
        if (Input.GetButtonDown(skill01Input))
        {
            ActivateSkill<Skill01script>();
        }
        if (Input.GetButtonDown(skill02Input))
        {
            ActivateSkill<Skill02script>();
        }
        if (Input.GetButtonDown(skill03Input))
        {
            ActivateSkill<Skill03script>();
        }
        if (Input.GetButtonDown(skill04Input))
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
