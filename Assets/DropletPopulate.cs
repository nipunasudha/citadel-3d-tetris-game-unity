using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CubePosition
{
    public int rightGap;
    public int upGap;
    public int forwardGap;

    CubePosition(int rightGap, int upGap, int forwardGap)
    {
        this.rightGap = rightGap;
        this.upGap = upGap;
        this.forwardGap = forwardGap;
    }

    public Vector3 GetRelativeVector(float cubesize)
    {
        return new Vector3(rightGap, upGap, forwardGap) * cubesize;
    }
}


public class DropletPopulate : MonoBehaviour
{
    public GameObject CubeUnit;
    public float CubeScale;
    public CubePosition[] cubePositionList;

    private GameObject originCube;

    void Start()
    {
        CreateOriginCube();
        PopulateDroplet(cubePositionList);
    }

    void Update()
    {
    }

    GameObject CreateOriginCube()
    {
        originCube = Instantiate(CubeUnit, Vector3.zero, Quaternion.identity);
        originCube.transform.localScale = new Vector3(CubeScale, CubeScale, CubeScale);
        originCube.transform.parent = gameObject.transform;
        return originCube;
    }

    void PopulateDroplet(CubePosition[] shape)
    {
        for (int i = 0; i < shape.Length; i++)
        {
            var cubeInstance = Instantiate(CubeUnit, shape[i].GetRelativeVector(CubeScale), Quaternion.identity);
            cubeInstance.transform.localScale = new Vector3(CubeScale, CubeScale, CubeScale);
            cubeInstance.transform.parent = gameObject.transform;
        }
    }

    Vector3 DecodeDirection(int directionCode)
    {
        Vector3 decodedDirection;
        switch (directionCode)
        {
            case 0:
                decodedDirection = transform.right;
                break;
            case 1:
                decodedDirection = transform.up;
                break;
            case 2:
                decodedDirection = transform.forward;
                break;
            case 3:
                decodedDirection = -transform.right;
                break;
            case 4:
                decodedDirection = -transform.up;
                break;
            case 5:
                decodedDirection = -transform.forward;
                break;
            default:
                return Vector3.zero;
        }

        return decodedDirection;
    }
}