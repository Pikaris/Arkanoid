using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    public GameObject ball;
    public GameObject itemMegaBall;
    public GameObject itemDisruption;

    public GameObject GetBall(Vector3? position = null, float angleX = 0.0f, float angleY = 0.0f)
    {
        return Instantiate(ball, position.GetValueOrDefault(), Quaternion.Euler(angleX, angleY, 0));
    }
    public GameObject CopyBall(GameObject obj, Vector3? position = null, float angleX = 0.0f, float angleY = 0.0f)
    {
        return Instantiate(obj, position.GetValueOrDefault(), Quaternion.Euler(angleX, angleY, 0));
    }
    public GameObject GetMegaBall(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(itemMegaBall, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
    public GameObject GetDisruption(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(itemDisruption, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
}
