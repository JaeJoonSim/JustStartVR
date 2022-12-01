using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLine : MonoBehaviour
{
    public Color color;
    public bool ShowInventoryItem = false, DrowLine = true;

    [SerializeField]
    string Tag;


    Transform TargetPos, itemPos;
    LineRenderer line;
    Renderer LineColor;

    bool firstGrab;
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
        if (!Drow||firstGrab)
        {
            line.enabled = false;
            return;
        }
        line.SetPosition(0, itemPos.position);
        line.SetPosition(1, TargetPos.position);
    }

    public void ItemDrop()
    {

        Drow = false;
        line.enabled = false;

        GameObject[] objects = GameObject.FindGameObjectsWithTag(Tag);
        if (objects[0] != null)
        {
            foreach (GameObject G in objects)
            {
                G.transform.Find(Tag).gameObject.SetActive(false);
            }
        }

    }
    public void DrowItemLine()
    {
        line.enabled = true;
        GameObject[] objects = GameObject.FindGameObjectsWithTag(Tag);

        if (objects[0] != null || Drow)
        {
            Drow = true;
            TargetPos = objects[0].transform;
        }
        else
        {
            Drow = false;
            line.enabled = false;
        }

        if (ShowInventoryItem) return;
        foreach (GameObject G in objects)
        {
            G.transform.Find(Tag).gameObject.SetActive(true);
        }
    }

    public void SetFirstGrab(bool value)
    {
        firstGrab = value;
    }
}
