using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    float Healths { get; }

    void Damage(float Amount);

}
