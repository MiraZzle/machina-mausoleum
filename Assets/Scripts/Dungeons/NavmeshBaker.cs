using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshBaker : MonoBehaviour
{
    public NavMeshSurface Surface2D;

    void Start()
    {
        Invoke("BakeNavmesh", 1);

    }

    public void BakeNavmesh()
    {
        Surface2D.BuildNavMeshAsync();
    }
}
