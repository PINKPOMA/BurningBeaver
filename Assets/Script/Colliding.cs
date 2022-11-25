
using System.Collections;
using UnityEngine;

public class Colliding : MonoBehaviour
{
   private bool _isPlaying;
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Spill());
   }

   IEnumerator Spill()
   {
      if (!_isPlaying)
      {
         _isPlaying = true;
         for (int i = 0; i < 10; i++)
         {
            transform.localScale += new Vector3(0.6f, 0.6f, 0);
            yield return new WaitForSeconds(0.05f);
         }
         transform.localScale = Vector3.zero;
         _isPlaying = false;
      }
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Flame"))
      {
         Debug.Log($"gameObject: {col.gameObject.name} is detected!");
         Destroy(col.gameObject);
      }
   }
}
