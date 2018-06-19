using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject DropletPrefab;

    // Use this for initialization
    void Start()
    {
        Instantiate(DropletPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}