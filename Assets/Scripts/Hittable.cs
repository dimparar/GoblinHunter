using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hittable
{
    void TakeDamage(float amount);
    bool HasZeroHealth();

}
