using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct TileInfo
{
    public Tile.Type tileType;
    public GameObject obj;
}
public class Enemy : CharacterBaseScript
{
    [SerializeField]List<TileInfo> tileList;
    [SerializeField] GameObject gameOverObj;

    private Dictionary<Tile.Type, GameObject> tileDict = new Dictionary<Tile.Type, GameObject>();
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

        for (int i = 0; i < tileList.Count; i++)
        {
            tileDict.Add(tileList[i].tileType, tileList[i].obj);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "XRRig")
        {
            gameOverObj.SetActive(true);
        }
    }

    public void ChangeEnemyAct(EnemyAct.Act type)
    {
        if (isDetected == true && type == EnemyAct.Act.STOP)
            return;
        actionType = type;
        actionDict[actionType].Start(this);
    }

    public void Update()
    {
        actionDict[actionType].Update(this);
    }
}
