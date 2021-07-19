using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAudioAssigner : MonoBehaviour
{
    DrawRing ring = null;
    float radius = 5;


    float audioLevelSource;
    float audioLevelEnd = 0;

    [SerializeField] AnimationCurve audioFalloff;

    
    void Start()
    {
        ring = GetComponentInChildren<DrawRing>();
    }

    // Update is called once per frame
    void Update()
    {
        radius = ring.radius;
        GetHostilesInArea();
    }

    public Collider[] hitCols;
    public LayerMask enemyMask;
    void GetHostilesInArea()
    {
        hitCols = Physics.OverlapSphere(transform.position, radius, enemyMask);
        foreach (var col in hitCols)
        {
            col.gameObject.GetComponent<AIMovement>().InAudioRange = true;
            TrackHostiles();
        }
    }
    public void TrackHostiles()
    {
        audioLevelSource = radius;
        foreach (Collider col in hitCols)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            float curAudioLevel = audioFalloff.Evaluate(dist / audioLevelSource);
            //float curAudioLevel = Mathf.Lerp(audioLevelSource, audioLevelEnd, dist / audioLevelSource);
            AIMovement move = col.gameObject.GetComponent<AIMovement>();
            if (move.activeAudioCaster != this.gameObject)
            {
                if (move.audioVal <= curAudioLevel)
                {
                    move.audioVal = curAudioLevel;
                    move.activeAudioCaster = this.gameObject;
                }
            }
            else
            {
                move.audioVal = curAudioLevel;
            }
        }
    }
}
