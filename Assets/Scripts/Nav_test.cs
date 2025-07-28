using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav_test : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject home;
    
    public int active_target = 0;
    public float min_distance = 5f;
    public Variable_Tracker tracker;

    private List<int> passed = new List<int>();
    private bool justArrived = false;
    private int roverID;
    public int speed;
    public int batterey_life;
    public int mining_spped;
    public bool on_trip;


    List<GameObject> south_targets;
    List<GameObject> east_targets;
    List<GameObject> west_targets;
    List<GameObject> north_targets;
    float timer;
    int charge_time;



    void Start()
    {
        tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
        int RoverId = GetRoverID();
        speed = tracker.roverSlotStats[RoverId][0];
        batterey_life = tracker.roverSlotStats[RoverId][1];
        mining_spped = tracker.roverSlotStats[RoverId][2];
        active_target = 0;
        south_targets = tracker.south_points;
        east_targets = tracker.east_points;
        west_targets = tracker.west_points;
        north_targets = tracker.north_points;
        home = GameObject.Find("Rover Hub point");

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
            agent.destination = south_targets[batterey_life].transform.position;
        }
        else if(num == 1)
        {
            agent.destination = east_targets[batterey_life].transform.position;
        }
        else if (num == 2)
        {
            agent.destination = west_targets[batterey_life].transform.position;
        }
        else
        {
            agent.destination = north_targets[batterey_life].transform.position;
        }
        agent.speed = 7 + (speed * 3);
        

    }
    void arriving()
    {
        if (tracker.charge_speed == 0)
        {
            charge_time = 45;
        }
        else if (tracker.charge_speed == 1)
        {
            charge_time = 30;
        }
        else if (tracker.charge_speed == 2)
        {
            charge_time = 15;
        }

    }
    void Update()
    {
        timer += Time.deltaTime * tracker.speed;
        if (on_trip)
        {
            float dist = Vector3.Distance(this.transform.position, agent.destination);
            if (dist < min_distance) //if it is close to its target
            {
                if (agent.destination == home.transform.position) //If it is ariving at the base
                {
                    on_trip = false;
                    arriving();
                }
                else
                {
                    agent.destination = home.transform.position; //If it is not ariving at home go home
                }
            }
        }
        if (!on_trip)
        {

            timer = 0;
            
        }
        
        
      
    }
}
/*
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
*/