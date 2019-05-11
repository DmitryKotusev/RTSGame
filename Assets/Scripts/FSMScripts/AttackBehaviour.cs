using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : NPCBaseFSM
{
    [SerializeField]
    LayerMask agentsMask;

    Collider currentTarget;
    Quaternion weaponNormalRotation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // weaponNormalRotation = NPC.GetComponent<AgentData>().weapon.transform.localRotation;
        // Debug.Log(weaponNormalRotation);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // NPC.GetComponent<AgentData>().weapon.transform.localRotation = weaponNormalRotation;
        // Debug.Log(NPC.GetComponent<AgentData>().weapon.transform.rotation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        List<Collider> visibleTargets = LookAround();
        if (visibleTargets.Count == 0)
        {
            // Set flag!!
            animator.SetBool("AttackEnemy", false);
            return;
        }
        currentTarget = ChooseTarget(visibleTargets);
        if (LockOnTarget())
        {
            LockWeaponOnTarget();
            TryShoot();
        }
    }

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.SetLookAtWeight(1);
    //    animator.SetLookAtPosition(currentTarget.transform.position);
    //    animator.SetIKRotation();
    //}

    private void LockWeaponOnTarget()
    {
        NPC.GetComponent<AgentData>().weapon.transform.parent.LookAt(currentTarget.transform);
    }

    private void TryShoot()
    {
        NPC.GetComponent<AgentData>().weapon.GetComponent<WeaponScript>().TryFire();
    }

    private bool LockOnTarget()
    {
        Vector3 direction = currentTarget.transform.position - NPC.transform.position;
        direction.y = 0;
        NPC.transform.rotation = Quaternion.LookRotation(direction);
        return true;
    }

    private Collider ChooseTarget(List<Collider> visibleTargets)
    {
        visibleTargets.Sort((agent1, agent2) =>
        {
            return agent2.GetComponent<AgentData>().GetPriorityСoefficient(Vector3.Distance(NPC.transform.position, agent2.transform.position))
            .CompareTo(agent1.GetComponent<AgentData>().GetPriorityСoefficient(Vector3.Distance(NPC.transform.position, agent1.transform.position)));
        });
        return visibleTargets[0];
    }

    List<Collider> LookAround()
    {
        Collider npcsCollider = NPC.GetComponent<Collider>();
        npcsCollider.enabled = false;
        Collider[] agentsColliders = Physics.OverlapSphere(NPC.transform.position, NPC.GetComponent<AgentData>().lookDistance, agentsMask);

        List<Collider> visibleTargets = new List<Collider>();
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
                        visibleTargets.Add(agentsColliders[i]);
                    }
                }
            }
        }
        npcsCollider.enabled = true;
        return visibleTargets;
    }
}
