using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void Begin(EnemyBaseFSMMgr mgr);
    public abstract void Update(EnemyBaseFSMMgr mgr);
    public abstract void End(EnemyBaseFSMMgr mgr);
}