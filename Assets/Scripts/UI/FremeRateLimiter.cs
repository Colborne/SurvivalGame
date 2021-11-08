using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FremeRateLimiter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          QualitySettings.vSyncCount = 0;
          Application.targetFrameRate = 60;
    }
}
