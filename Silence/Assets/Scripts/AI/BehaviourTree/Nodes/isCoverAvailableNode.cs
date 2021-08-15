using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCoverAvailableNode : Node
{
    Cover[] validCover;
    Transform target;

    public isCoverAvailableNode(Cover[] covers, Transform target, EnemyAI ai)
    {
        this.validCover = covers;
        this.target = target;
        this.ai = ai;
    }
    public override NodeState Evaluate()
    {
        ai.curNode = this;
        Transform bestCover = FindBestCoverPoint();
        ai.SetBestCover(bestCover);
        return bestCover != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestCoverPoint()
    {
        float minAngle = 90;
        Transform bestPoint = null;
        for (int i = 0; i < validCover.Length; i++)
        {
            Transform bestSpotInCover = FindBestSpotInCover(validCover[i], ref minAngle);
            if (bestSpotInCover != null)
            {
                bestPoint = bestSpotInCover;
            }
        }
        return bestPoint;
    }

    private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
    {
        Transform[] availablePoints = cover.GetCoverSpots();
        Transform bestSpot = null;
        for (int i = 0; i < availablePoints.Length; i++)
        {
            Vector3 dir = target.position - availablePoints[i].position;
            if (CheckIfCoverIsValid(availablePoints[i]))
            {
                float angle = Vector3.Angle(availablePoints[i].forward, dir);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    bestSpot = availablePoints[i];
                }
            }
        }
        return bestSpot;
    }

    private bool CheckIfCoverIsValid(Transform point)
    {
        RaycastHit hit;
        Vector3 dir = target.position - point.position;
        if(Physics.Raycast(point.position, dir, out hit))
        {
            if (hit.collider.transform != target)
            {
                return true;
            }
        }
        return false;
    }

}
