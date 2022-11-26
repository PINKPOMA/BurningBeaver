using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class BucketCount : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite emptyBucketSprite;
    [SerializeField] private Sprite fillBucketSprite;
    
    [SerializeField] private Image[] buckets;
    private int nextSpendWater;
    private int nextFillWater;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        nextSpendWater = buckets.Length - 1;
        nextFillWater = 0;
    }

    public void SpendWaterBucket()
    {
        buckets[nextSpendWater].sprite = emptyBucketSprite;
        nextFillWater = nextSpendWater;
        nextSpendWater--;
    }

    public void FillWaterBucket()
    {
        buckets[nextFillWater].sprite = fillBucketSprite;
        nextSpendWater = nextFillWater;
        nextFillWater++;
    }

    public void Init(int waterCapacity)
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i].enabled = false;
        }
        for (int i = 0; i < waterCapacity; i++)
        {
            buckets[i].enabled = true;
        }
    }
}
