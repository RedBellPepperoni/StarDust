using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPostionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;

        return vec;
    }

    public static Vector3 GetMouseWorldPostionWithZ()
    {
        return GetMouseWorldPostionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPostionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPostionWithZ(Input.mousePosition, worldCamera);

    }

    public static Vector3 GetMouseWorldPostionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;

    }
}
