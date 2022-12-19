using UnityEngine;

public class EnemyBaseAct
{

    public virtual void Start(Enemy enemy) { }

    public virtual void Update(Enemy enemy) { }
}



class EnemyStopAct : EnemyBaseAct
{
    public override void Start(Enemy enemy)
    {
        enemy.animator.StartPlayback();
    }

    public override void Update(Enemy enemy)
    {

    }
}

class EnemyMoveAct : EnemyBaseAct
{
    public override void Start(Enemy enemy)
    {
        enemy.animator.StopPlayback();
    }
    public override void Update(Enemy enemy)
    {
        enemy.transform.position += Vector3.forward * Time.deltaTime * enemy.speed;
    }
}