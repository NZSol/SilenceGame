using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePoints : MonoBehaviour
{
    [SerializeField] GameObject ObjectToCenterOn = null;
    public float range = 5;
    public LayerMask groundLayer = 1 << 8;

    Vector3 FindPos(Vector3 center)
    {
        Vector2 point = Random.insideUnitCircle * range;
        Vector3 RayStartPoint = new Vector3(point.x, 0, point.y);
        return point;
    }

    void RandomCast(Vector3 target)
    {
        RaycastHit hit;
        if (Physics.Raycast(target + Vector3.up * 100, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {

        }
    }


    void Start()
    {
        Vector3 target = FindPos(ObjectToCenterOn.transform.position);
        RandomCast(target);
    }

    void Update()
    {

    }
}
