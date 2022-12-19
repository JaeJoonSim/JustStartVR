using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : CharacterBaseScript
{
    private Dictionary<EnemyAct.Act, EnemyBaseAct> actionDict = new Dictionary<EnemyAct.Act, EnemyBaseAct>();


    public float speed;

    [HideInInspector]public EnemyAct.Act actionType;

    public Animator animator;

    [HideInInspector]public bool isDetected;        

    public override void Init()
    {
        isDetected = true;

        actionDict.Add(EnemyAct.Act.STOP, new EnemyStopAct());
        actionDict.Add(EnemyAct.Act.MOVE, new EnemyMoveAct());
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "XRRig")
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void ChangeEnemyAct(EnemyAct.Act type)
    {
        actionType = type;
        actionDict[actionType].Start(this);
    }

    public void Update()
    {
        actionDict[actionType].Update(this);
    }
}
