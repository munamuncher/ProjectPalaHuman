using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public enum LevelType
{
    Skill01_Text = 0,
    Skill02_Text,
    Skill03_Text,
    Skill04_Text,
}


public class LevelUpPopUp : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI level01Text;
    [SerializeField] 
    private TextMeshProUGUI level02Text;
    [SerializeField] 
    private TextMeshProUGUI level03Text;


    public void SkillText_Update(LevelType lvlType, int amountofIncrease)
    {
        
        switch (lvlType) 
        {
            case LevelType.Skill01_Text:
                Debug.Log(" Earning spawn points from this want will increase by  " + " skill increase amount Function " + " Amount");
                break;
            case LevelType.Skill02_Text:
                Debug.Log(" Fire fire for 3 seconds and increase damage by  " + " skill increase amount Function " + " Amount");
                break;
            case LevelType.Skill03_Text:
                Debug.Log(" will grant Sheild to the ally around the Skill Area by  " + " skill increase amount Function " + " Amount");
                break;
            case LevelType.Skill04_Text:
                Debug.Log(" Drops a Meteor from the sky and Increase by  " + " skill increase amount Function " + " Amount");
                break;
        }
    }
    
    public void Displayskill()
    {

    }
}
