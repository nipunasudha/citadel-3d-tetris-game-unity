using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject cubeInnerMesh;
    public float cubeInnerScale;

    void Start()
    {
        cubeInnerMesh.transform.localScale = new Vector3(cubeInnerScale, cubeInnerScale, cubeInnerScale);
    }

    void Update()
    {
    }
}