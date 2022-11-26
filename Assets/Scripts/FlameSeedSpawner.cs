using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlameSeedSpawner : MonoBehaviour
{
    [SerializeField]private Vector3 playerPos;
    [SerializeField]private GameObject flameSeed;
    [SerializeField]private float seedSpeed;
    [Header("World edge block")]
    [SerializeField]private float xMin;
    [SerializeField]private float yMin;
    [SerializeField]private float xMax;
    [SerializeField]private float yMax;
    [SerializeField] private PlayerController playerController;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(10.0f);
        RandomPos();
        StartCoroutine(SeedSpawn());
        StartCoroutine(EndReady());
    }

    IEnumerator EndReady()
    {
        yield return new WaitForSeconds(180f);
        Destroy(gameObject.GetComponent<FlameSeedSpawner>());
    }
    IEnumerator SeedSpawn()
    {
        if (!playerController.IsDead)
        {
            Instantiate(flameSeed, playerPos, Quaternion.identity);
            yield return new WaitForSeconds(seedSpeed);
            seedSpeed /= 100;
            if (seedSpeed <= 3)
                seedSpeed = 3;
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
