using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Command : MonoBehaviour
{
    public float radiusMultiplier = 3f;
    public float particlesSetDestinationVerticalOffset = 0.1f;
    public float particlesSetDestinationLifeTime = 2f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private GameObject particlesSetDestination;
    GameManager gameManager;
    InputController inputController;
    AgentsSelector agentsSelector;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        agentsSelector = GetComponent<AgentsSelector>();
    }

    
    void Update()
    {
        if (inputController.rightMouseButtonUp)
        {
            var selectedUnits = agentsSelector.GetSelectedObjects();
            if (selectedUnits.Count > 0)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, groundLayer))
                {
                    // Debug.Log(raycastHit.transform.name);
                    GameObject particles = Instantiate(particlesSetDestination, raycastHit.point + Vector3.up * particlesSetDestinationVerticalOffset, Quaternion.LookRotation(raycastHit.normal));
                    Destroy(particles, particlesSetDestinationLifeTime);
                    GiveMoveCommand(raycastHit.point, selectedUnits);
                }
            }  
        }
    }

    void GiveMoveCommand(Vector3 destination, List<GameObject> selectedUnits)
    {
        NavMeshAgent leadersAgent = selectedUnits[0].GetComponent<NavMeshAgent>();
        float agentRadius = leadersAgent.radius;
        NavMeshPath path = new NavMeshPath();
        // Configuring direction
        Vector3 direction = Vector3.zero;
        if (leadersAgent.CalculatePath(destination, path))
        {
            Debug.Log("Partial pass succeded");
            direction = path.corners[path.corners.Length - 1] - path.corners[0];
            direction.y = 0;
        }
        if (direction == Vector3.zero)
        {
            direction = new Vector3 (1, 0, 0);
        }
        direction = direction.normalized;
        Vector3 rightDirection = Vector3.Cross(Vector3.up, direction).normalized;
        // Configuring finish

        int amountOfUnits = selectedUnits.Count;
        int squareSize = Mathf.CeilToInt(Mathf.Sqrt(amountOfUnits));
        int halfSize = squareSize / 2;

        for (int i = 0; i < amountOfUnits; i++)
        {
            int columnIndex = i % squareSize - halfSize;
            int lineIndex = i / squareSize - halfSize;

            NavMeshAgent objNavAgent = selectedUnits[i].GetComponent<NavMeshAgent>();
            objNavAgent.SetDestination(destination
                + direction * lineIndex * radiusMultiplier * agentRadius
                + rightDirection * columnIndex * radiusMultiplier * agentRadius);
            if (!CheckAgentAlreadyInPlace(objNavAgent))
            {
                Animator objAnimator = selectedUnits[i].GetComponent<Animator>();
                objAnimator.SetBool("MoveToNewDestination", true);
            }
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
