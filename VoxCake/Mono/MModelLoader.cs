using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MModelLoader : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshFilter>().mesh = UModelMesh.Get("CHeadSniper", 0);
    }
}
