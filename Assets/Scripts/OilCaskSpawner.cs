using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class OilCaskSpawner : MonoBehaviour
{
    [SerializeField]private Vector3 playerPos;
    [SerializeField] private Tilemap oilCaskTilemap;
    [Header("World edge block")]
    [SerializeField]private float xMin;
    [SerializeField]private float yMin;
    [SerializeField]private float xMax;
    [SerializeField]private float yMax;

    private void Start()
    {
        RandomPos();
        StartCoroutine(OilCaskSpawn());
    }

    IEnumerator OilCaskSpawn()
    {
        Instantiate(oilCaskTilemap, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(15f);
        RandomPos();
        StartCoroutine(OilCaskSpawn());
    }
    void RandomPos()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if (Random.Range(0, 2) == 0)
        {
            playerPos.x = ReturnAddPlayerPos();
            if (playerPos.x > xMax)
                playerPos.x = xMax;
            if (playerPos.x < xMin)
                playerPos.x = xMin;
        }
        else
        {
            playerPos.y += ReturnAddPlayerPos();
            if (playerPos.y > yMax)
                playerPos.y = yMax;
            if (playerPos.y < yMin)
                playerPos.y = yMin;
        }
        
    }
    float ReturnAddPlayerPos()
    {
        return Random.Range(0, 2) == 0 ? Random.Range(-3, -5) : Random.Range(3, 5);
    }
}
