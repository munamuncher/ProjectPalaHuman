using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    public SkillComponent[] skillComponents;

    public KeyCode Skill01 = KeyCode.H;
    public KeyCode Skill02 = KeyCode.J;
    public KeyCode Skill03 = KeyCode.K;
    public KeyCode Skill04 = KeyCode.L;

    private void Start()
    {
        foreach(SkillComponent component in skillComponents)
        {
            component.Activate();
        }    
    }

    private void Update()
    {       
        if(Input.GetKeyDown(Skill01))
        {
            ActivateSkill<Skill01script>();
        }
        if (Input.GetKeyDown(Skill02))
        {
            //activate flamethrower skill
        }
        if (Input.GetKeyDown(Skill03))
        {
            //activate shield skill
        }
        if (Input.GetKeyDown(Skill04))
        {
            //activate meteorite skill
        }
    }

    void ActivateSkill<T>() where T : SkillComponent
    {
        T[] skillComponents = FindObjectsOfType<T>();

        Debug.Log("Skill components found: " + skillComponents.Length);

        foreach (T component in skillComponents)
        {
            if (component is T)
            { 
                T typedComponent = component as T;
                typedComponent.Activate();
            }
        }
    }
}
