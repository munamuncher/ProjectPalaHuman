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

   //todo Animation tree and blend 지우고 모든 애니메이션을 메니져에서 관리하고 싱글톤으로 만들것
}
