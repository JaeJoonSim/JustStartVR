using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLine : MonoBehaviour
{
    public Color color;
    public bool ShowInventoryItem = false;

    [SerializeField]
    string Tag;


    Transform TargetPos, itemPos;
    LineRenderer line;
    Renderer LineColor;


    bool Drow = false;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        LineColor = GetComponent<Renderer>();

        LineColor.material.color = color;
        itemPos = transform;
        Drow = false;
    }
    private void Update()
    {
        if (Drow)
        {
            line.SetPosition(0, itemPos.position);
            line.SetPosition(1, TargetPos.position);
        }
        else
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1,Vector3.zero);
        }
    }

    public void ItemDrop()
    {
        Drow = false;
        if (TargetPos == null) return;
        if (ShowInventoryItem) return;
            
        GameObject gameObject = TargetPos.transform.Find(Tag).gameObject;
        if (gameObject == null) return;
        gameObject.SetActive(false);
    }

    public void DrowItemLine()
    {
        GameObject[] objects;
        objects = GameObject.FindGameObjectsWithTag(Tag);
        Debug.Log(objects[0]);
        if (objects[0] != null)
        {
            Drow = true;
            TargetPos = objects[0].transform;


            if (ShowInventoryItem) return;

            GameObject gameObject = TargetPos.transform.Find(Tag).gameObject;
            if (gameObject == null) return;
            gameObject.SetActive(true);
        }
        else
        {
            Drow = false;
            gameObject.SetActive(false);
        }
    }
}
