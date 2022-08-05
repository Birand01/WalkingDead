using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Ray ray;
    RaycastHit hitInfo;
    private void Update()
    {
        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;
        Physics.Raycast(ray, out hitInfo);
        transform.position = hitInfo.point;
    }
}
