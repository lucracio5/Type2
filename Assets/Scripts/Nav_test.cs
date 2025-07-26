using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav_test : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject home;
    public List<GameObject> targets;
    public int active_target = 0;
    public float min_distance = 5f;

    private List<int> passed = new List<int>();
    private bool justArrived = false;

    void Start()
    {
        active_target = 0;
        agent.destination = targets.Count > 0 ? targets[active_target].transform.position : home.transform.position;
    }

    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, agent.destination);

        if (dist < min_distance && !justArrived)
        {
            justArrived = true;

            if (!passed.Contains(active_target))
            {
                passed.Add(active_target);
            }

            active_target++;

            if (active_target < targets.Count)
            {
                agent.destination = targets[active_target].transform.position;
            }
            else
            {
                agent.destination = home.transform.position;
            }
        }

        // Reset arrival flag if we move away again (so it can trigger at next arrival)
        if (dist >= min_distance)
        {
            justArrived = false;
        }
    }
}
