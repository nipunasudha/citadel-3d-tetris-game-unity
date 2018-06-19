using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject TowerPrefab;
    private GameObject towerInstance;

    private void Awake()
    {
    }

    void Start()
    {
        var towerSize = new Vector3Int(3, 6, 3);

        towerInstance = Instantiate(TowerPrefab, Vector3.zero, Quaternion.identity);
        towerInstance.GetComponent<TowerController>().Initialize(
            towerSize,
            200f
        );
    }

    void Update()
    {
    }
}