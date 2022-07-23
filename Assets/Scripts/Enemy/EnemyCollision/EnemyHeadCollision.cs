using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCollision : EnemyBaseCollision
{
    public override void setDamage()
    {
        switch (Es.EnemyType)
        {
            case 1:
                damage = 2f;
                break;
            case 2:
                damage = 2f;
                break;
            case 3:
                damage = 0.5f;
                break;
            case 4:
                damage = 0f;
                break;
            default:
                damage = 1;
                break;
        }
    }
}
