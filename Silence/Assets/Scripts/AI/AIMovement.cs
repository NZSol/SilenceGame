using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public enum MoveState { Patrol, Audio, Sight, brokenLOS, Noise }
    public MoveState movement = MoveState.Patrol;
    [SerializeField] GameObject player = null;

    NavMeshAgent agent = null;
    Vector3 target = Vector3.zero;
    Vector3 brokenSightPos = Vector3.zero;
    
    #region Patrol Vars
    public bool PatrolCircuit = true;

    #endregion

    #region Audio Vars
    public bool InAudioRange = false;
    public float audioVal = 0f;
    public GameObject activeAudioCaster = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = PatrolPoints[patrolPntNumber].transform.position;
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (player.transform.position - gameObject.transform.position);
        extraAgentRotation();
        GetLOS(dir);
        if (InAudioRange)
        {

        }
        AudioDetect();
        switch (movement)
        {
            case MoveState.Patrol:
                Patrol();
                break;
            case MoveState.Audio:
                AudioFunc();
                break;
            case MoveState.Noise:

                break;
            case MoveState.Sight:
                SightsOn();
                break;
            case MoveState.brokenLOS:
                LOSLost();
                break;
        }
    }

    public float extraRotSpeed = 5f;
    void extraAgentRotation()
    {
        Vector3 lookRot = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRot), extraRotSpeed * Time.deltaTime);
    }

    public float DetectionRange = 30f, captureRange = 5f;
    float fovRange = 80f;
    void GetLOS(Vector3 direction)
    {
        float playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        RaycastHit hit;
        if (Vector3.Angle(direction, transform.forward) < fovRange)     //Allow raycast to find position in an angle
        {
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.transform.tag == "Player" && (playerDist <= DetectionRange && playerDist > captureRange))   //if player within range
                {
                    Debug.DrawRay(transform.position, direction, Color.green);
                    movement = MoveState.Sight;
                    return;
                }
                else if (hit.transform.tag == "Player" && (playerDist <= captureRange))     //If player within capture range
                {
                    Debug.DrawRay(transform.position, direction, Color.cyan);
                    movement = MoveState.Sight;
                    return;
                }
                else if (hit.transform.tag == "Player" && playerDist > DetectionRange)      //If player exceeds detection range
                {
                    Debug.DrawRay(transform.position, direction, Color.yellow);
                    if (movement == MoveState.Sight)
                    {
                        movement = MoveState.brokenLOS;
                    }
                    return;
                }
                else                                                                        //If player LOS is broken by Object
                {
                    Debug.DrawRay(transform.position, direction, Color.red);
                    if (movement == MoveState.Sight)
                    {
                        movement = MoveState.brokenLOS;
                    }
                    return;
                }
            }
        }
        Debug.DrawRay(transform.position, direction, Color.black);
    }


    #region Patrol Functions
    [SerializeField] GameObject[] PatrolPoints = null;
    [SerializeField] float maxRange = 0.5f;
    int patrolPntNumber = 0;
    bool adding = true;
    public void Patrol()
    {
        target = PatrolPoints[patrolPntNumber].transform.position;
        float Dist = Vector3.Distance(transform.position, target);
        switch (PatrolCircuit)
        {
            case true:
                break;
            case false:
                if (Dist < maxRange)
                {
                    if (patrolPntNumber == PatrolPoints.Length - 1)
                    {
                        adding = false;
                    }
                    else if (patrolPntNumber == 0)
                    {
                        adding = true;
                    }
                    if (adding)
                    {
                        patrolPntNumber++;
                    }
                    else
                    {
                        patrolPntNumber--;
                    }
                }
                break;
        }
        agent.SetDestination(target);

    }
    #endregion

    #region LOS Functions
    void SightsOn()
    {
        agent.SetDestination(player.transform.position);
    }
    #endregion
    #region LOSBroken
    bool targetSet = false;
    void LOSLost()
    {
        if (!targetSet)
        {
            targetSet = true;
            target = player.transform.position;
            agent.SetDestination(target);
        }
        print(agent.remainingDistance);
        if (agent.remainingDistance < 0.5f)
        {
            targetSet = false;
            movement = MoveState.Audio;
        }
    }
    #endregion

    #region audioFunctions

    float multipliedAudio;
    float drownoutThreshold = 4;
    void AudioFunc()
    {
        print(agent.pathStatus);
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            agent.ResetPath();
        }
        Vector3 dir = player.transform.position - transform.position;
        if (audioVal > 0)
        {
            multipliedAudio = audioVal * 1.5f;
        }
        if (audioVal > drownoutThreshold)
        {

            agent.Move(dir);
        }
    }

    void AudioDetect()
    {
        if (audioVal <= 0)
        {
            activeAudioCaster = null;
        }
    }
    #endregion

}
