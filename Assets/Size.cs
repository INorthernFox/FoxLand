using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Size : MonoBehaviour
{
    [Button()]
    public void GetSize()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        Vector3 pointZA = meshCollider.ClosestPoint(transform.position + Vector3.forward * 100);
        Vector3 pointZB = meshCollider.ClosestPoint(transform.position - Vector3.forward * 100);
        
        Vector3 pointXA = meshCollider.ClosestPoint(transform.position + Vector3.right * 100);
        Vector3 pointXB = meshCollider.ClosestPoint(transform.position - Vector3.right * 100);

        Vector2 size = new Vector2(pointXA.x - pointXB.x, pointZA.z - pointZB.z);
        
        Debug.Log(size);
    }
}
