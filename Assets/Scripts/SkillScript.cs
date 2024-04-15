using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    //대리자 , 대리기사 처럼 대신일을 해주는 대상이라고 보면 편하다
    //델리게이트는 함수에 대한 참조 느낌...
    //하나의 델리게이트로 여러 함수들에 접근해서 실행 할수있다
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
