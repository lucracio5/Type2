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
    public Variable_Tracker tracker;

    private List<int> passed = new List<int>();
    private bool justArrived = false;
    private int roverID;
    public int speed;
    public int baterey_life;
    public int mining_spped;
    public bool on_trip;

    void Start()
    {
        tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
        int RoverId = GetRoverID();
        speed = tracker.roverSlotStats[RoverId][0];
        baterey_life = tracker.roverSlotStats[RoverId][1];
        mining_spped = tracker.roverSlotStats[RoverId][2];
        active_target = 0;

    }

    //returns the rover ID from the GameObject's name, assuming it ends with a number (ex. Rover_2)
    int GetRoverID()
    {
        string roverName = this.gameObject.name;
        string number = System.Text.RegularExpressions.Regex.Match(roverName, @"\d+$").Value;
        return int.Parse(number);
    }
    public void start_trip()
    {
        on_trip = true;
        int num = Random.Range(0, 3);//Random to decide which direction
        if (num  == 0)
        {

        }
        else if(num == 1)
        {

        }
        else if (num == 2)
        {

        }
        else if (num == 3)
        {

        }
        agent.destination = targets[active_target].transform.position;

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
