using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Object
{
    public OriginCube OriginCube;
    public Vector3Int FromDropletOrigin;
    public Vector3Int FromTowerOrigin;

    public Cube(OriginCube originCube, Vector3Int fromDropletOrigin)
    {
        OriginCube = originCube;
        FromDropletOrigin = fromDropletOrigin;
        FromTowerOrigin = new Vector3Int();
    }

    private GameObject GetCubePrefab()
    {
        return OriginCube.CubeGameObject;
    }

    void UpdatePosition()
    {
        FromTowerOrigin = OriginCube.FromTowerOrigin + FromDropletOrigin;
    }
}

public class SlaveCube : Cube
{
    public SlaveCube(OriginCube originCube, Vector3Int fromDropletOrigin) : base(originCube, fromDropletOrigin)
    {
    }
}

public class OriginCube : Cube
{
    public Vector3Int Orientation;
    public GameObject CubeGameObject;
    public Droplet ParentDroplet;

    public OriginCube(Vector3Int fromTowerOrigin, Vector3Int orientation, Droplet parentDroplet) :
        base(null, fromTowerOrigin)
    {
        ParentDroplet = parentDroplet;
        CubeGameObject = Instantiate(parentDroplet.ParentTower.CubePrefab,
            parentDroplet.ParentTower.CubePositionToAbsolute(fromTowerOrigin), Quaternion.identity);
        Orientation = orientation;
        OriginCube = this;
    }

    public void SetPositionFromTowerOrigin(Vector3Int fromTowerOrigin)
    {
        FromTowerOrigin = fromTowerOrigin;
    }
}

public class Droplet
{
    public OriginCube OriginCube;
    public float Speed;
    public Tower ParentTower;

    public Droplet(Vector3Int originPositionFromTowerOrigin, Vector3Int orientation, float defaultSpeed,
        Tower parentTower)
    {
        ParentTower = parentTower;
        Speed = defaultSpeed;
        OriginCube = new OriginCube(originPositionFromTowerOrigin, Vector3Int.zero, this);
        SetOrientation(orientation);
    }

    public void SetOrientation(Vector3Int orientation)
    {
        OriginCube.Orientation = orientation;
    }

    public Vector3 GetOrientation()
    {
        return OriginCube.Orientation;
    }

    public Vector3 getOriginPosition()
    {
        return OriginCube.FromTowerOrigin;
    }

    public void MoveToTowerPosition(Vector3Int position)
    {
        //TODO Use delegates here. Ref: https://www.tutorialspoint.com/csharp/csharp_delegates.htm
        OriginCube.SetPositionFromTowerOrigin(position);
    }
}


public class Tower : Object
{
    private Cube[,,] cubePile;
    private List<Droplet> droplets;
    public float DefaultSpeed;
    public float CubeSize;
    public Vector3Int TowerSize;

    public GameObject CubePrefab;
//    public GameObject TowerGameObject;

    public Tower(Vector3Int towerSize, float cubeSize, float defaultSpeed, GameObject cubePrefab)
    {
        cubePile = new Cube[towerSize.x, towerSize.y, towerSize.z];
        TowerSize = towerSize;
        DefaultSpeed = defaultSpeed;
        CubeSize = cubeSize;
        CubePrefab = cubePrefab;
    }

/*    private GameObject CreateTowerGameObject(GameObject towerPrefab)
    {
        var towerScale = (Vector3) TowerSize * CubeSize;
      var towerInstance = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
       towerInstance.GetComponent<TowerController>().SetTowerSize(TowerSize, CubeSize);
        return towerInstance;
    }
*/
    public Droplet CreateDroplet(Vector3Int position, Vector3Int orientation)
    {
        Droplet newDroplet = new Droplet(position, orientation, DefaultSpeed, this);
        return newDroplet;
    }

    public Vector3Int GetSize()
    {
        return TowerSize;
    }

    public Vector3 CubePositionToAbsolute(Vector3Int cubePosition)
    {
        Vector3 cubeAbsolutePosition;
        cubeAbsolutePosition.x = CubeSize * cubePosition.x + CubeSize / 2 - CubeSize * GetSize().x / 2;
        cubeAbsolutePosition.y = CubeSize * cubePosition.y + CubeSize / 2;
        cubeAbsolutePosition.z = CubeSize * cubePosition.z + CubeSize / 2 - CubeSize * GetSize().z / 2;
        return cubeAbsolutePosition;
    }

    public void Update(float deltaTime)
    {
    }

    //Tower dimention controller
}


public class TowerController : MonoBehaviour
{
    public GameObject TowerMesh;
    public GameObject CubePrefab;
    private Tower GameTower;
    private bool isInitialized;

    void Start()
    {
    }

    public void Initialize(Vector3Int towerSize, float defaultSpeed)
    {
        if (IsInitialized())
        {
            Debug.Log("Tower Already Initialized.");
            return;
        }

        var cubeSize = 4f / Mathf.Max(towerSize.x, towerSize.z);
        GameTower = new Tower(towerSize, cubeSize, defaultSpeed, CubePrefab);
        SetTowerMesh();
        isInitialized = true;
    }

    public Tower GetTower()
    {
        return GameTower;
    }

    public bool IsInitialized()
    {
        return isInitialized;
    }

    void Update()
    {
    }

    private void SetTowerMesh()
    {
        TowerMesh.transform.localScale = (Vector3) GameTower.TowerSize * GameTower.CubeSize;
        var towerPosition = TowerMesh.transform.position;
        towerPosition.y = TowerMesh.transform.localScale.y / 2;
        TowerMesh.transform.position = towerPosition;
    }
}