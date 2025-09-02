using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    public int battery_life;
    public int mining_speed;
    public bool on_trip;
    public int mining_time;


    List<GameObject> south_targets;
    List<GameObject> east_targets;
    List<GameObject> west_targets;
    List<GameObject> north_targets;
    float charging_timer;
    float mining_timer;
    int charge_time;
    bool is_mining = false;
    int colected_regoltih;




    void Start()
    {
        tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
        int RoverId = GetRoverID()-1;
        speed = tracker.roverSlotStats[RoverId][0];
        battery_life = tracker.roverSlotStats[RoverId][1];
        mining_speed = tracker.roverSlotStats[RoverId][2];

        //speed = 3;
        //battery_life = 4;
        //mining_speed = 5;

        active_target = 0;
        south_targets = tracker.south_points;
        east_targets = tracker.east_points;
        west_targets = tracker.west_points;
        north_targets = tracker.north_points;
        home = GameObject.Find("Rover Hub point");
        charge_time = 60 - ((tracker.charge_speed + 1) * 15);
        charging_timer = 0;
        Debug.Log(speed);
        Debug.Log(battery_life);
        Debug.Log(mining_speed);

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
            agent.destination = south_targets[battery_life-1].transform.position; 
        }
        else if(num == 1)
        {
            agent.destination = east_targets[battery_life-1].transform.position;
        }
        else if (num == 2)
        {
            agent.destination = west_targets[battery_life - 1].transform.position;
        }
        else
        {
            agent.destination = north_targets[battery_life - 1].transform.position;
        }
        agent.speed = 8 + (speed * 2);
        Debug.Log("Batterey life is: "+battery_life);

    }
    void arriving()
    {
       charge_time = 60-((tracker.charge_speed+1)*15);
       charging_timer = 0;
       tracker.regolith += colected_regoltih;
       Debug.Log("Arriving");
    }
    void mining()
    {
        
        is_mining = true;
        mining_time = 15 - mining_speed;
        mining_timer = 0;



         //If it is not ariving at home go home

    }
    void Update()
    {
        //Debug.Log(on_trip);
        //Debug.Log(is_mining);
        //Debug.Log(agent.destination);



        
        if (on_trip)
        {
            //Debug.Log("Start: "+agent.destination);
            float dist = Vector3.Distance(this.transform.position, agent.destination);
            if (dist < min_distance) //if it is close to its target
            {
                Debug.Log("Destination: " + agent.destination + " Home is at: " + home.transform.position + " This evaluates to " + (agent.destination == home.transform.position));
                if ((agent.destination.x == home.transform.position.x)&& (agent.destination.z ==home.transform.position.z)) //If it is ariving at the base
                {
                    //Debug.Log("Arriving");
                    on_trip = false;
                    arriving();
                }
                else if(!is_mining)
                {
                    //Debug.Log("Starting to Mine");
                    mining();
                }
            }
        }
        if (!on_trip)//If it is not on a trip and if it is passed its charge time start a new trip
        {
            charging_timer += Time.deltaTime * tracker.speed;


            if (charging_timer > charge_time)
            {
                start_trip();
                on_trip=true;
                charging_timer=0;
            }
        }
        if(is_mining)
        {
            mining_timer += Time.deltaTime * tracker.speed;
            if(mining_timer > mining_time)
            {
                agent.destination = home.transform.position;
                is_mining = false;
                float luckFactor = Random.Range(0.8f, 1.2f); //varies slightly
                colected_regoltih = Mathf.RoundToInt((mining_speed + battery_life)*luckFactor);
            }
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