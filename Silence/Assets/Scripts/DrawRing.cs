using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRing : MonoBehaviour
{
    [SerializeField]GameObject player = null;

    [Range(0,50)]
    public int segments = 50;

    [Range(0,25)]
    public float radius = 5;
    public float radiusMultiplier = 1.5f;

    LineRenderer line;
    [SerializeField] Material mat;
    [SerializeField] float widthVal = 0.2f;

    [SerializeField] LayerMask mask = 1 << 9;

    Vector3 playerOffset = Vector3.zero;

    public bool UpdateSizeOnCheck = false;
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material = mat;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.loop = true;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        line.receiveShadows = true;
        line.startWidth = widthVal;

        if (!UpdateSizeOnCheck)
        {
            EditPoints();
        }
    }

    public float storedRadius = 0f;
    private void Update()
    {
        transform.localPosition = new Vector3(0, -player.transform.position.y + 1, 0);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        playerOffset = transform.position;
        if (UpdateSizeOnCheck)
        {
            if (radius != storedRadius)
            {
                EditPoints();
                storedRadius = radius;
            }

            AdjustPointsOnCollider();

        }
    }
    void EditPoints()
    {
        line.useWorldSpace = false;
        float x;
        float z;

        float angle = 20f;

        for (int i = 0; i < segments+1; i++)
        {
            x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius);
            z = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius);

            line.SetPosition(i, new Vector3(x, FindYVal(x,z) - 0.8f, z));
            

            angle += (360f / segments);
        }
    }

    float FindYVal(float x, float z)
    {
        float val = 0;
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 100, z) + new Vector3(playerOffset.x, -playerOffset.y, playerOffset.z), Vector3.down * 2000, out hit, Mathf.Infinity, mask))
        {
            val = hit.point.y;

            return val;
        }
        return 0;
    }


    void AdjustPointsOnCollider()
    {
        Collider[] groundCols = Physics.OverlapSphere(transform.position, radius, mask);
        foreach (var col in groundCols)
        {
            if (col.tag != "Floor")
            {
                StartCoroutine(LateEditPoints());
            }
        }
    }

    IEnumerator LateEditPoints()
    {
        yield return new WaitForSeconds(0.05f);
        EditPoints();
    }

}
