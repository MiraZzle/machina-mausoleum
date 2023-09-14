using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshBaker : MonoBehaviour
{
    // Time delay before baking the NavMesh.
    [SerializeField] private float bakingTime = 0.8f;

    public NavMeshSurface Surface2D;

    void Start()
    {
        Invoke("BakeNavmesh", bakingTime);
    }

    // Trigger the asynchronous NavMesh baking process using the NavMeshSurface component
    public void BakeNavmesh()
    {
        Surface2D.BuildNavMeshAsync();
    }
}
