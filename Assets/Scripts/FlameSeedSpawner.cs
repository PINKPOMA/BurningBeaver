using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlameSeedSpawner : MonoBehaviour
{
    [SerializeField]private Vector3 playerPos;
    [SerializeField]private GameObject flameSeed;
    [Header("World edge block")]
    [SerializeField]private float xMin;
    [SerializeField]private float yMin;
    [SerializeField]private float xMax;
    [SerializeField]private float yMax;
    [SerializeField] private PlayerController playerController;
    
    private void Start()
    {
        RandomPos();
        StartCoroutine(SeedSpawn());
    }

    IEnumerator SeedSpawn()
    {
        if (!playerController.IsDead)
        {
            Instantiate(flameSeed, playerPos, Quaternion.identity);
            yield return new WaitForSeconds(15f);
            RandomPos();
            StartCoroutine(SeedSpawn());
        }
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
        return Random.Range(0, 2) == 0 ? Random.Range(-3f, -5f) : Random.Range(3f, 5f);
    }
}
