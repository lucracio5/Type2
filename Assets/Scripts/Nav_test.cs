using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Nav_test : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject home;
    public List <GameObject> targets;
    public int active_target = 0;
    int min_distance = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = home.transform.position;
        if (Vector3.Distance(this.transform.position, agent.destination) < min_distance) //if it is within 5 units of destination
        {
            active_target += 1; //Next target
            if (active_target <= targets.Count) 
            {
                agent.destination = targets[active_target].transform.position;
            }
            else
            {
                agent.destination = home.transform.position;
            }
            
        }
    }
}
