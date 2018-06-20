using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject TowerPrefab;
    private GameObject towerInstance;
    private TowerController towerController;

    private void Awake()
    {
    }

    void Start()
    {
        var towerSize = new Vector3Int(4, 8, 4);

        towerInstance = Instantiate(TowerPrefab, Vector3.zero, Quaternion.identity);
        towerController = towerInstance.GetComponent<TowerController>();
        towerController.Initialize(
            towerSize,
            200f
        );

        Test();
    }

    void Test()
    {
        towerController.GetTower().CreateDroplet(
            new Vector3Int(3, 7, 3),
            Vector3Int.zero
        );
        towerController.GetTower().CreateDroplet(
            new Vector3Int(0, 0, 0),
            Vector3Int.zero
        );
    }

    void Update()
    {
    }
}