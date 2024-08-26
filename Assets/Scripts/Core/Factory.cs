using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    public GameObject itemDisruption;

    public GameObject GetDisruption(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(itemDisruption, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
}
