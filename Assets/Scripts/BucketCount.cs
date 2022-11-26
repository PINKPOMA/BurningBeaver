using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class BucketCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waterBucketCountText;

    public void ChangeWaterBucketCount(int count) => waterBucketCountText.text = "x" + count;
}
