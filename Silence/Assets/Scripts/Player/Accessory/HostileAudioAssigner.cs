using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAudioAssigner : MonoBehaviour
{
    [SerializeField]
    GameObject player = null;
    DrawRing ring = null;
    float radius = 5;

    float audioDisplay = 4f;


    float audioLevelSource;

    [SerializeField] AnimationCurve audioFalloff = new AnimationCurve();
    GameObject audioMask = null;
    
    void Start()
    {
        ring = GetComponentInChildren<DrawRing>();
        audioMask = GetComponent<DrawRing>().AudioMask;
    }

    // Update is called once per frame
    void Update()
    {
        radius = ring.radius;
        GetHostilesInArea();
        audioDisplay = Mathf.Clamp(audioDisplay, 1, 4);
        audioMask.GetComponent<Renderer>().material.SetFloat("GradientScale", Random.Range(1,2));

    }

    public Collider[] hitCols;
    public LayerMask enemyMask;
    void GetHostilesInArea()
    {
        hitCols = Physics.OverlapSphere(transform.position, radius, enemyMask);
        foreach (var col in hitCols)
        {
            //col.gameObject.GetComponent<FiniteStateMachine>().InAudioRange = true;
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

            //FiniteStateMachine machine = col.gameObject.GetComponent<FiniteStateMachine>();
            //if (machine.activeAudioCaster != this.gameObject)
            //{
            //    if (machine.incomingAudioLevel <= curAudioLevel)
            //    {
            //        machine.incomingAudioLevel = curAudioLevel;
            //        machine.activeAudioCaster = this.gameObject;
            //    }
            //}
            //else
            //{
            //  machine.incomingAudioLevel = curAudioLevel;
            //}
        }
    }
}
