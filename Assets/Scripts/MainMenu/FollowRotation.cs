using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public GameObject FollowOBJ;

    private void Update()
    {
        transform.rotation = FollowOBJ.transform.rotation;
    }
}
