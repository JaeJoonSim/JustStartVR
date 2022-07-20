using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public bool stay = true;
    public GameObject FollowOBJ;

    private void Start()
    {
        if(!stay)
            transform.rotation = FollowOBJ.transform.rotation;
    }
    private void Update()
    {
        if(stay)
        transform.rotation = FollowOBJ.transform.rotation;
    }
}
