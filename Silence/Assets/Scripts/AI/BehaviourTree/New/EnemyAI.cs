using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public List<Node> nodeMemory = new List<Node>();

    private Material mat;

    private Node rootNode;

    public Node curNode;
    public Node storedNode;
    Text[] text = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mat = GetComponent<MeshRenderer>().material;
        text = GetComponentsInChildren<Text>();
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
        isCoveredNode coveredNode = new isCoveredNode(playerTransform, transform, this);
        chaseNode chase = new chaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chaseRange, playerTransform, transform, this);
        RangeNode shootingRangeNode = new RangeNode(shootRange, playerTransform, transform, this);
        shootNode shootNode = new shootNode(agent, this);

        Sequence chaseSeq = new Sequence(new List<Node> { chasingRangeNode, chase }, this, nodeMemory);
        Sequence shootSeq = new Sequence(new List<Node> { shootingRangeNode, shootNode }, this, nodeMemory);

        Sequence GoToCoverSeq = new Sequence(new List<Node> { coverAvailableNode, gotoCoverNode}, this, nodeMemory);
        Selector findCoverSel = new Selector(new List<Node> { GoToCoverSeq, chaseSeq}, this);
        Selector TryCoverSel = new Selector(new List<Node> { coveredNode, findCoverSel}, this);

        Sequence mainCoverSeq = new Sequence(new List<Node> { healthNode, TryCoverSel }, this, nodeMemory);
        rootNode = new Selector(new List<Node> { mainCoverSeq, shootSeq, chaseSeq }, this);
    }

    private void Update()
    {
        rootNode.Evaluate();
        if (storedNode != curNode)
        {
            storedNode = curNode;
            StoreState(curNode);

        }
        foreach(Node node in nodeMemory)
        {
            print(node);
            print(nodeMemory.Count);
        }

        if (rootNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.red);
        }
    }

    public void StoreState(Node nodeToStore)
    {
        nodeMemory.Add(nodeToStore);
        text[0].text = nodeMemory[0].ToString();
        text[1].text = nodeMemory[1].ToString();
        text[2].text = nodeMemory[2].ToString();
        if (nodeMemory.Count > 3)
        {
            nodeMemory.Remove(nodeMemory[0]);
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
