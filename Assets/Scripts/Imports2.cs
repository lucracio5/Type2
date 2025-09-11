using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;


public class Imports2 : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text nextDeliveryText;
    public TMP_Text foodText;
    public TMP_Text waterText;
    public TMP_Text regolithText;
    public TMP_Text crewText;
    public TMP_Text lithium_text;
    public TMP_Text titanium_text;

    public TMP_Text importQueuetext;
    public TMP_Text exportQueuetext;

    public TMP_Text avalibleImportSlots;
    public TMP_Text avalibleExportSlots;


    public TMP_Text shipArrivalText;

    public float timer;
    public float deliveryInterval = 60f;

    public Variable_Tracker tracker;

    private List<ImportItem> importQueue = new();
    private List<OutputItem> outputQueue = new();
    Audio_manager audio_manager;

    public Animator shipAnimator;

    public int max_queue;

    private void Start()
    {
        shipAnimator = GameObject.Find("Rocket").GetComponent<Animator>();
        tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
        audio_manager = GameObject.Find("Game Manager").GetComponent<Audio_manager>();
        max_queue = 3;
    }

    private void Update()
    {
        timer += Time.deltaTime * tracker.speed;

        UpdateUIText();


        if (timer >= deliveryInterval)
        {
            timer = 0;
            ProcessShip();
        }
        else if (timer >= deliveryInterval - 1.5)
        {
            Arrive();
        }
    }

    public void Arrive()
    {
        shipAnimator.SetBool("Arriving", true);
        Invoke("PlayLanding", 8f);
        Invoke("NotArrving", 16f);
    }

    private void PlayLanding()
    {
        audio_manager.PlayLanding();
    }
    private void NotArrving()
    {
        shipAnimator.SetBool("Arriving", false);
    }
    public void addSlot()
    {
        if (tracker.money >= 200)
        {
            tracker.money -= 200;
            audio_manager.PlayUIclick();
            max_queue += 1;
            GetComponent<ToolTips>().DisplayMessage("1 Slot Added");
        }
        else
        {
            audio_manager.PlayFailedClick();
            GetComponent<ToolTips>().DisplayMessage("Not Enough Money to Purchase this item");
        }

    }

    private void UpdateUIText()
    {
        nextDeliveryText.text = $"Time Until Next Ship: {(int)(deliveryInterval - timer)}";
        foodText.text = $"Food at {tracker.food}%";
        waterText.text = $"Water at {tracker.water}%";
        regolithText.text = $"{tracker.regolith} Regolith";
        lithium_text.text = $"{tracker.lithium} Lithium";
        titanium_text.text = $"{tracker.titanium} Titanium";
        crewText.text = $"{tracker.population}/{tracker.max_population} population";

        importQueuetext.text = "";
        exportQueuetext.text = "";
        foreach (ImportItem item in importQueue)
        {
            importQueuetext.text = importQueuetext.text + " " + item.label + " - " + item.cost + "\n";
        }

        foreach (OutputItem item in outputQueue)
        {
            exportQueuetext.text = exportQueuetext.text + " " + item.label + " + " + item.value + "\n";
        }
        avalibleExportSlots.text = (max_queue - outputQueue.Count).ToString() +" Slots Avalible";
        avalibleImportSlots.text = (max_queue - importQueue.Count).ToString()+ " Slots Avalible";

    }



    public void QueueFoodImport()
    {
        if (tracker.money - GetQueuedImportCost() >= 20 && tracker.food+ GetQueuedAmount("Food Import", 10) < tracker.max_food && importQueue.Count < max_queue)
        {
            importQueue.Add(new ImportItem("Food Import", 20, () => tracker.food = Mathf.Min(tracker.food + 10, tracker.max_food)));
            audio_manager.PlayUIclick();
        }
        else if(!(tracker.money - GetQueuedImportCost() >= 20))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Money to Purchase this item");
            audio_manager.PlayFailedClick();
        }
        else if (!(tracker.food+ GetQueuedAmount("Food Import", 10) < tracker.max_food))
        {
            GetComponent<ToolTips>().DisplayMessage("Food is Full");
            audio_manager.PlayFailedClick();
        }
        else if (!(importQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }

    public void QueueWaterImport()
    {
        if (tracker.money - GetQueuedImportCost() >= 20 && tracker.water+GetQueuedAmount("Water Import",10) < tracker.max_water && importQueue.Count < max_queue)
        {
            importQueue.Add(new ImportItem("Water Import", 20, () => tracker.water = Mathf.Min(tracker.water + 10, tracker.max_water)));
            audio_manager.PlayUIclick();
        }
        else if (!(tracker.money - GetQueuedImportCost() >= 20))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Money to Purchase this item");
            audio_manager.PlayFailedClick();
        }
        else if (!(tracker.water+ GetQueuedAmount("Water Import", 10) < tracker.max_water))
        {
            GetComponent<ToolTips>().DisplayMessage("Water is Full");
            audio_manager.PlayFailedClick();
        }
        else if (!(importQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public void QueueFuelImport()
    {
        if (tracker.money- GetQueuedImportCost() >= 100 && tracker.fuel + GetQueuedAmount("Nuclear Fuel", 500) < tracker.max_fuel && importQueue.Count < max_queue)
        {
            importQueue.Add(new ImportItem("Nuclear Fuel", 100, () => tracker.fuel = Mathf.Min(tracker.fuel + 500, tracker.max_fuel)));
            audio_manager.PlayUIclick();
        }
        else if (!(tracker.money - GetQueuedImportCost() >= 100))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Money to Purchase this item");
            audio_manager.PlayFailedClick();
        }
        else if (!(tracker.water + GetQueuedAmount("Nuclear Fuel", 500) < tracker.max_fuel))
        {
            GetComponent<ToolTips>().DisplayMessage("Fuel is Full");
            audio_manager.PlayFailedClick();
        }
        else if (!(importQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public void QueueCrewImport()
    {
        if (tracker.money - GetQueuedImportCost() >= 100 && tracker.population + GetQueuedAmount("Crew", 10) < tracker.max_population && importQueue.Count < max_queue)
        {
            importQueue.Add(new ImportItem("Crew", 100, () => tracker.population = Mathf.Min(tracker.population + 10, tracker.max_population)));
            audio_manager.PlayUIclick();
        }
        else if (!(tracker.money - GetQueuedImportCost() >= 100))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Money to Purchase this item");
            audio_manager.PlayFailedClick();
        }
        else if (!(tracker.water + GetQueuedAmount("Crew", 10) < tracker.max_population))
        {
            GetComponent<ToolTips>().DisplayMessage("Population is Full");
            audio_manager.PlayFailedClick();
        }
        else if (!(importQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public void QueueRegoligthExport()
    {
        if (tracker.regolith - GetQueuedExports("Regolith Export",1) >= 1 && outputQueue.Count < max_queue)
        {
            outputQueue.Add(new OutputItem("Regolith Export", 25,() => tracker.regolith = Mathf.Min(tracker.regolith - 1, tracker.max_mining)));
            audio_manager.PlayUIclick();
        }
        else if(!(tracker.regolith - GetQueuedExports("Regolith Export", 1) >= 1))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Regolith");
            audio_manager.PlayFailedClick();
        }
        else if (!(outputQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public void QueueTitaniumExport()
    {
        if (tracker.titanium - GetQueuedExports("Titanium Export", 1) >= 1 && outputQueue.Count < max_queue)
        {
            outputQueue.Add(new OutputItem("Titanium Export", 40, () => tracker.titanium = Mathf.Min(tracker.titanium - 1, tracker.max_titanium)));
            audio_manager.PlayUIclick();
        }
        else if (!(tracker.titanium - GetQueuedExports("Titanium Export", 1) >= 1))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Titanium");
            audio_manager.PlayFailedClick();
        }
        else if (!(outputQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public void QueueLithiumExport()
    {
        if (tracker.lithium - GetQueuedExports("Lithium Export", 1) >= 1 && outputQueue.Count < max_queue)
        {
            outputQueue.Add(new OutputItem("Lithium Export", 80, () => tracker.lithium = Mathf.Min(tracker.lithium - 1, tracker.max_lithium)));
            audio_manager.PlayUIclick();
        }
        else if (!(tracker.lithium - GetQueuedExports("Lithium Export", 1) >= 1))
        {
            GetComponent<ToolTips>().DisplayMessage("Not Enough Lithium");
            audio_manager.PlayFailedClick();
        }
        else if (!(outputQueue.Count < max_queue))
        {
            GetComponent<ToolTips>().DisplayMessage("Queue is Full");
            audio_manager.PlayFailedClick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public int GetQueuedAmount(string label, int perImportAmount)
    {
        int total = 0;
        foreach (var item in importQueue)
        {
            if (item.label == label)
                total += perImportAmount;
        }
        return total;
    }
    private int GetQueuedExports(string label, int perExportAmount)
    {
        int total = 0;
        foreach (var item in outputQueue)
        {
            if (item.label == label)
                total += perExportAmount;
        }
        return total;
    }
    private int GetQueuedImportCost()
    {
        int total = 0;
        foreach (var item in importQueue)
        {
            total += item.cost;
        }
        return total;
    }
    private void ProcessShip()
    {
        audio_manager.PlayLanding();
        StartCoroutine(ShowShipArrivalMessage());


        foreach (var item in importQueue)
        {
            if (tracker.money >= item.cost)
            {
                tracker.money -= item.cost;
                item.Apply();
            }
        }
        foreach (var item in outputQueue)
        {
            if ((item.label == "Regolith Export") && (tracker.regolith >= 1))
            {
                tracker.money += item.value;
                item.Apply();
            }
            else if ((item.label == "Titanium Export") && (tracker.titanium >= 1))
            {
                tracker.money += item.value;
                item.Apply();
            }
            else if ((item.label == "Lithium Export") && (tracker.lithium >= 1))
            {
                tracker.money += item.value;
                item.Apply();
            }

        }

        // Clear both queues
        importQueue.Clear();
        outputQueue.Clear(); // You can add logic here to sell items, etc.
        
        
        //exportQueuetext;

    }
    private IEnumerator ShowShipArrivalMessage()
    {

        shipArrivalText.text = "A new shipment has arrived!";
        for (int i = 0; i <= 4; i++)
        {
            shipArrivalText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            shipArrivalText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}


public class ImportItem
{
    public string label;
    public int cost;
    private System.Action applyEffect;

    public ImportItem(string label, int cost, System.Action applyEffect)
    {
        this.label = label;
        this.cost = cost;
        this.applyEffect = applyEffect;
    }

    public void Apply() => applyEffect?.Invoke();
}

public class OutputItem
{
    public string label;
    public int value;
    private System.Action applyEffect;

    public OutputItem(string label, int value, System.Action applyEffect)
    {
        this.label = label;
        this.value = value;
        this.applyEffect = applyEffect;
    }
    public void Apply() => applyEffect?.Invoke();
}

