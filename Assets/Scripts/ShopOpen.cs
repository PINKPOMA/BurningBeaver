using UnityEngine;

public class ShopOpen : MonoBehaviour
{
   [SerializeField] private GameObject shopUI;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.E))
      {
         shopUI.SetActive(true);
      }
   }
}
