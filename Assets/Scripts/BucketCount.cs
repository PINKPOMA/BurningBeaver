using UnityEngine;
using UnityEngine.UI;

public class BucketCount : MonoBehaviour
{
    [SerializeField] private Sprite emptyBucketSprite;
    [SerializeField] private Sprite fillBucketSprite;
    
    [SerializeField] private Image[] buckets;
    private int _nextSpendWater;
    private int _nextFillWater;

    private void Start()
    {
        _nextSpendWater = buckets.Length - 1;
        _nextFillWater = 0;
    }

    public void SpendWaterBucket()
    {
        buckets[_nextSpendWater].sprite = emptyBucketSprite;
        _nextFillWater = _nextSpendWater;
        _nextSpendWater--;
    }

    public void FillWaterBucket()
    {
        buckets[_nextFillWater].sprite = fillBucketSprite;
        _nextSpendWater = _nextFillWater;
        _nextFillWater++;
    }

    public void Init(int waterCapacity)
    {
        foreach (var bucket in buckets)
        {
            bucket.enabled = false;
        }

        for (var i = 0; i < waterCapacity; i++)
        {
            buckets[i].enabled = true;
        }
    }
}
