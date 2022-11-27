using UnityEngine;

public class ShopGuide : MonoBehaviour
{
    [SerializeField] private Shop shop;

    public void OnClick()
    {
        shop.gameObject.SetActive(true);
    }
}
