using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleBehaviour : NPCBaseFSM
{
    [SerializeField]
    LayerMask agentsMask;

    // AgentTeam agentTeam;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // agentTeam = NPC.GetComponent<AgentData>().agentTeam;
        // NPC.GetComponent<AgentData>().lookDistance;
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAroundForEnemies(animator);
        LookAroundForHelpRequest(animator);
    }

    private void LookAroundForHelpRequest(Animator animator)
    {
        Collider npcsCollider = NPC.GetComponent<Collider>();
        npcsCollider.enabled = false;
        Collider[] agentsColliders = Physics.OverlapSphere(NPC.transform.position, NPC.GetComponent<AgentData>().lookDistance, agentsMask);

        List<Collider> comradsHelpNeeded = new List<Collider>();
        for (int i = 0; i < agentsColliders.Length; i++)
        {
            // Enemy agent detected
            if (agentsColliders[i].GetComponent<AgentData>().agentTeam == NPC.GetComponent<AgentData>().agentTeam)
            {
                if (agentsColliders[i].GetComponent<AgentData>().needHelp)
                {
                    comradsHelpNeeded.Add(agentsColliders[i]);
                }
            }
        }
        if (comradsHelpNeeded.Count == 0)
        {
            npcsCollider.enabled = true;
            return;
        }

        if (!animator.GetBool("AttackEnemy") && !animator.GetBool("MoveToNewDestination"))
        {
            comradsHelpNeeded.Sort((agent1, agent2) =>
            {
                return Vector3.Distance(agent2.transform.position, NPC.transform.position)
                .CompareTo(Vector3.Distance(agent1.transform.position, NPC.transform.position));
            });
            // Set destination
            NPC.GetComponent<NavMeshAgent>()
                .SetDestination(comradsHelpNeeded[0].GetComponent<AgentData>().targetPosition);
            NPC.GetComponent<AgentData>().comradToHelp = comradsHelpNeeded[0].gameObject;
            animator.SetBool("ComradNeedsHelp", true);
        }
        

        npcsCollider.enabled = true;
    }

    void LookAroundForEnemies(Animator animator)
    {
        Collider npcsCollider = NPC.GetComponent<Collider>();
        npcsCollider.enabled = false;
        Collider[] agentsColliders = Physics.OverlapSphere(NPC.transform.position, NPC.GetComponent<AgentData>().lookDistance, agentsMask);

        for (int i = 0; i < agentsColliders.Length; i++)
        {
            // Enemy agent detected
            if (agentsColliders[i].GetComponent<AgentData>().agentTeam != NPC.GetComponent<AgentData>().agentTeam)
            {
                // Check with raycast
                RaycastHit raycastHit;
                if (Physics.Raycast(NPC.transform.position, agentsColliders[i].transform.position - NPC.transform.position, out raycastHit,
                    Vector3.Distance(NPC.transform.position, agentsColliders[i].transform.position)))
                {
                    if (raycastHit.transform.gameObject == agentsColliders[i].transform.gameObject)
                    {
                        animator.SetBool("AttackEnemy", true);
                    }
                }
            }
        }
        npcsCollider.enabled = true;
    }
}
