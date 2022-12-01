using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public bool stay = true;
    public GameObject FollowOBJ;

    private void Start()
    {
        if (!stay)
        {
            //transform.rotation = FollowOBJ.transform.rotation;
            transform.LookAt(FollowOBJ.transform);
            //transform.position = FollowOBJ.transform.position;
        }
    }
    private void Update()
    {
        if (!stay) return;
        transform.LookAt(FollowOBJ.transform);
    }
}
