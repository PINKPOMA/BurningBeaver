using System.Collections;
using UnityEngine;

public class Colliding : MonoBehaviour
{
    [SerializeField] private float scaleUpX = 0.6f;
    [SerializeField] private float scaleUpY = 0.6f;
    [SerializeField] private float waterDuration = 0.05f;
    
    private IEnumerator Start()
    {
        for (var i = 0; i < 10; i++)
        {
            transform.localScale += new Vector3(scaleUpX, scaleUpY, 0);
            yield return new WaitForSeconds(waterDuration);
        }

        transform.localScale = Vector3.zero;
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flame"))
        {
            var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            player.AddMoney(5);
            Destroy(col.gameObject);
        }
    }
}