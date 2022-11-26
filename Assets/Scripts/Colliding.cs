using System.Collections;
using UnityEngine;

public class Colliding : MonoBehaviour
{
    IEnumerator Start()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale += new Vector3(0.6f, 0.6f, 0);
            yield return new WaitForSeconds(0.05f);
        }

        transform.localScale = Vector3.zero;
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flame"))
        {
            //Debug.Log($"gameObject: {col.gameObject.name} is detected!");
            var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            user.SetMoney(10);
            Destroy(col.gameObject);
        }
    }
}