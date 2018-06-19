using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cube
{
    public Cube OriginCube;
    public Vector3 FromDropletOrigin;
    public Vector3 FromTowerOrigin;

    public Cube(Cube originCube, Vector3 fromDropletOrigin)
    {
        OriginCube = originCube;
        FromDropletOrigin = fromDropletOrigin;
        FromTowerOrigin = new Vector3();
        UpdatePosition();
    }

    public Cube(Vector3 fromTowerOrigin)
    {
        OriginCube = this;
        FromDropletOrigin = Vector3.zero;
        FromTowerOrigin = fromTowerOrigin;
    }

    void UpdatePosition()
    {
        FromTowerOrigin = OriginCube.FromTowerOrigin + FromDropletOrigin;
    }
}

public class OriginCube : Cube
{
    public Vector3 Orientation;

    public OriginCube(Vector3 fromTowerOrigin, Vector3 orientation) : base(fromTowerOrigin)
    {
        Orientation = orientation;
    }

    public void SetPositionFromTowerOrigin(Vector3 fromTowerOrigin)
    {
        FromTowerOrigin = fromTowerOrigin;
    }
}

public class Droplet
{
    public Tower m_Tower;
    public OriginCube OriginCube;
    public float Speed;

    public Droplet(Tower m_Tower, Vector3 originPositionFromTowerOrigin, Vector3 orientation, float defaultSpeed)
    {
        this.m_Tower = m_Tower;
        Speed = defaultSpeed;
        OriginCube = new OriginCube(originPositionFromTowerOrigin, Vector3.zero);
        SetOrientation(orientation);
    }

    public void SetOrientation(Vector3 orientation)
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

    public void MoveToTowerPosition(Vector3 position)
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
    public GameObject TowerGameObject;

    public Tower(Vector3Int towerSize, int cubeSize, float defaultSpeed /*, GameObject towerPrefab*/)
    {
        cubePile = new Cube[towerSize.x, towerSize.y, towerSize.z];
        TowerSize = towerSize;
        DefaultSpeed = defaultSpeed;
        CubeSize = cubeSize;
//        TowerGameObject = CreateTowerGameObject(towerPrefab);
    }

/*    private GameObject CreateTowerGameObject(GameObject towerPrefab)
    {
        var towerScale = (Vector3) TowerSize * CubeSize;
      var towerInstance = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
       towerInstance.GetComponent<TowerController>().SetTowerSize(TowerSize, CubeSize);
        return towerInstance;
    }
*/
    public Droplet CreateDroplet(Vector3 position, Vector3 orientation)
    {
        Droplet newDroplet = new Droplet(this, position, orientation, DefaultSpeed);
        return newDroplet;
    }

    public Vector3 getSize()
    {
        return TowerSize;
    }

    public void Update(float deltaTime)
    {
    }

    //Tower dimention controller
}


public class TowerController : MonoBehaviour
{
    public GameObject TowerMesh;
    private Tower GameTower;

    void Start()
    {
        GameTower = new Tower(new Vector3Int(4, 8, 4), 1, 200f);
        SetTowerMesh();
    }

    void Update()
    {
    }

    public void SetTowerMesh()
    {
        TowerMesh.transform.localScale = (Vector3) GameTower.TowerSize * GameTower.CubeSize;
        var towerPosition = TowerMesh.transform.position;
        towerPosition.y = TowerMesh.transform.localScale.y / 2;
        TowerMesh.transform.position = towerPosition;
    }
}