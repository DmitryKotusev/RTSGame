using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToDestinationBehaviour : NPCBaseFSM
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NavMeshAgent objNavAgent = NPC.GetComponent<NavMeshAgent>();
        if (CheckAgentAlreadyInPlace(objNavAgent))
        {
            animator.SetBool("MoveToNewDestination", false);
        }
    }

    private bool CheckAgentAlreadyInPlace(NavMeshAgent objNavAgent)
    {
        if (!objNavAgent.pathPending)
        {
            if (objNavAgent.remainingDistance <= objNavAgent.stoppingDistance)
            {
                if (!objNavAgent.hasPath || objNavAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
