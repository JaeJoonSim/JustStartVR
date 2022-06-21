using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderUpdate : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    MeshCollider mcollider;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        mcollider = GetComponentInChildren<MeshCollider>();
    }

   
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.1f)
        {
            time = 0;
            UpdateCollider();
        }
    }



    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        mcollider.sharedMesh = null;
        mcollider.sharedMesh = colliderMesh;
    }
}