using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject light;
    public Enemy enemyScript;

    Dictionary<bool, EnemyAct.Act> actDict = new Dictionary<bool, EnemyAct.Act>();

    public void Start()
    {
        actDict.Add(true, EnemyAct.Act.STOP);
        actDict.Add(false, EnemyAct.Act.MOVE);
    }

    int index;
    public void StartOnOff(int value)
    {
        StartCoroutine(OnOffLight(value));
    }

    bool isActive = true;
    IEnumerator OnOffLight(int value)
    {
        for(index = 0; index < value; index++)
        {
            yield return new WaitForSeconds(0.2f);
            isActive = isActive == false ? true : false;
            light.SetActive(isActive);
            enemyScript.ChangeEnemyAct(actDict[isActive]);
        }
    }
}
