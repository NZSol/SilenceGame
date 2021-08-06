using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float range = 0.5f;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, wallLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * range, Color.green);
        }
    }
}
