using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private void Start()
    {
        tilemap.SetTile(new(0, 0), null);
        tilemap.SetTile(new(4, 0), null);
        tilemap.SetTile(new(2, -1), null);
    }
}
