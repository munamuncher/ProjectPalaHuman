using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    //�븮�� , �븮��� ó�� ������� ���ִ� ����̶�� ���� ���ϴ�
    //��������Ʈ�� �Լ��� ���� ���� ����...
    //�ϳ��� ��������Ʈ�� ���� �Լ��鿡 �����ؼ� ���� �Ҽ��ִ�
    public delegate void SkillFunction();



    private Dictionary<string, SkillFunction> skillDictionary;

    private void Start()
    {
        skillDictionary = new Dictionary<string, SkillFunction>();
        AddSkill("IncreaseSP", IncreaseSP);
        AddSkill("FlameThrow", FlameThrow);
        AddSkill("ShieldArea", ShieldArea);
        AddSkill("Armageddon", Armageddon);
    }


    public void AddSkill(string skill, SkillFunction skillFunction)
    {
        skillDictionary[skill] = skillFunction;
    }

    public void UseageSkill(string skillname)
    {

    }

    private void IncreaseSP()
    {

    }
    private void FlameThrow()
    {

    }

    private void ShieldArea()
    {

    }

    private void Armageddon()
    {

    }
}
