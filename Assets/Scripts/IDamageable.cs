using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    float Healths { get; set; }

    void Damage(float Amount);

}
