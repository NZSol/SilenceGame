using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float initialHealth;
    [SerializeField] float _curHealth;
    public float curHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, initialHealth); }
    }
    [SerializeField] float lowHealthThreshhold;
    [SerializeField] float healthRestoreRate;


    [SerializeField] float shootRange;
    [SerializeField] float chaseRange;

    [SerializeField] Transform playerTransform;

    [SerializeField] Cover[] availableCover;
    Transform bestCoverPoint;
    NavMeshAgent agent;

    private Material mat;

    private Node rootNode;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        _curHealth = initialHealth;
        ConstructBehaviorTree();
    }

    void ConstructBehaviorTree()
    {
        isCoverAvailableNode coverAvailableNode = new isCoverAvailableNode(availableCover, playerTransform, this);
        GoToCoverNode gotoCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshhold);
        isCoveredNode coveredNode = new isCoveredNode(playerTransform, transform);
        chaseNode chase = new chaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chaseRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootRange, playerTransform, transform);
        shootNode shootNode = new shootNode(agent, this);

        Sequence chaseSeq = new Sequence(new List<Node> { chasingRangeNode, chase });
        Sequence shootSeq = new Sequence(new List<Node> { shootingRangeNode, shootNode });

        Sequence GoToCoverSeq = new Sequence(new List<Node> { coverAvailableNode, gotoCoverNode});
        Selector findCoverSel = new Selector(new List<Node> { GoToCoverSeq, chaseSeq});
        Selector TryCoverSel = new Selector(new List<Node> { coveredNode, findCoverSel});

        Sequence mainCoverSeq = new Sequence(new List<Node> { healthNode, TryCoverSel });
        rootNode = new Selector(new List<Node> { mainCoverSeq, shootSeq, chaseSeq });
    }

    private void Update()
    {
        rootNode.Evaluate();
        if (rootNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.red);
        }
    }


    public void SetColor(Color color)
    {
        mat.color = color;
    }

    public void SetBestCover(Transform bestCoverPoint)
    {
        this.bestCoverPoint = bestCoverPoint;
    }

    public Transform GetBestCover()
    {
        return bestCoverPoint;
    }
}
