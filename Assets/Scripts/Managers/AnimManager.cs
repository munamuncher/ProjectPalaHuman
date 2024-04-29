using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    
    private AnimManager instance;
    public AnimManager _Ainstance
    {
        get
        {
            if(instance ==null)
            {
                instance = FindObjectOfType<AnimManager>();
                if(instance == null )
                {
                    GameObject obj = new GameObject();
                    obj.name = "AnimationManager";
                    instance = obj.AddComponent<AnimManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject Player;

   //todo Animation tree and blend ����� ��� �ִϸ��̼��� �޴������� �����ϰ� �̱������� �����
}
