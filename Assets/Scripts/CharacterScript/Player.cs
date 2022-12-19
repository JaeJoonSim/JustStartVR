using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBaseScript
{
    [SerializeField]private Enemy enemyScript;

    [SerializeField] GameObject closeEyeObj;
    [SerializeField] float closeEyeTime;
    private float curTime;

    IEnumerator CloseEye()
    {
        while(curTime < closeEyeTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        curTime = 0.0f;
        closeEyeObj.SetActive(true);
        enemyScript.ChangeEnemyAct(EnemyAct.Act.MOVE);
        StartCoroutine(OpenEye());
    }

    IEnumerator OpenEye()
    {
        while (curTime < 0.5f)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        curTime = 0.0f;
        closeEyeObj.SetActive(false);
        if(enemyScript.isDetected == true)
        enemyScript.ChangeEnemyAct(EnemyAct.Act.STOP);
        StartCoroutine(CloseEye());
    }

    public override void Init()
    {
        curTime = 0.0f;
        //StartCoroutine(CloseEye());
    }
}
