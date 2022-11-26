using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public class OilCaskSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap oilCaskTilemap;
    [SerializeField] private Vector3Int[] oilCaskSpawnPos;

    private void Start()
    {
        StartCoroutine(OilCaskSpawn());
    }

    IEnumerator OilCaskSpawn()
    {
        Instantiate(oilCaskTilemap,oilCaskSpawnPos[Random.Range(0, 15)] , Quaternion.identity);
        yield return new WaitForSeconds(15f);
        StartCoroutine(OilCaskSpawn());
    }
}
