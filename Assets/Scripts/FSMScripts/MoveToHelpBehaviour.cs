using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToHelpBehaviour : NPCBaseFSM
{
    [SerializeField]
    LayerMask agentsMask;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAroundForEnemies(animator);
        if (!animator.GetBool("MoveToNewDestination"))
        {
            LookWhetherComradStillNeedsHelp(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("AttackEnemy"))
        {
            NPC.GetComponent<NavMeshAgent>().ResetPath();
        }
        animator.SetBool("ComradNeedsHelp", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void LookWhetherComradStillNeedsHelp(Animator animator)
    {
        if (NPC.GetComponent<AgentData>().comradToHelp == null
            || !NPC.GetComponent<AgentData>().comradToHelp.GetComponent<AgentData>().needHelp)
        {
            NPC.GetComponent<NavMeshAgent>().ResetPath();
            animator.SetBool("ComradNeedsHelp", false);
        }
        else
        {
            NPC.GetComponent<NavMeshAgent>().SetDestination(NPC.GetComponent<AgentData>().comradToHelp.GetComponent<AgentData>().targetPosition);
        }
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
