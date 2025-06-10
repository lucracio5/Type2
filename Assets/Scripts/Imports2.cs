using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Imports2: MonoBehaviour
{
    public GameObject panel;
    public TMP_Text nextDeliveryText;
    public TMP_Text foodText;
    public TMP_Text waterText;
    public TMP_Text regolithText;
    public TMP_Text import1;
    public TMP_Text import2;
    public TMP_Text import3;
    public TMP_Text output1;
    public TMP_Text output2;
    public TMP_Text output3;

    public TMP_Text shipArrivalText;

    public float timer;
    public float deliveryInterval = 60f;

    public Variable_Tracker tracker;

    private List<ImportItem> importQueue = new();
    private List<OutputItem> outputQueue = new();
    Audio_manager audio_manager;

    public Animator shipAnimator;

    private void Start()
    {
        shipAnimator = GameObject.Find("Rocket").GetComponent<Animator>();
        tracker = GameObject.Find("Game Manager").GetComponent<Variable_Tracker>();
        audio_manager = GameObject.Find("Game Manager").GetComponent<Audio_manager>();
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
        else if (timer >= deliveryInterval-1.5)
        {
            shipAnimator.SetBool("Arriving",true);
            Invoke("PlayLanding", 8f);
            Invoke("NotArrving", 16f);
        }
    }
    private void PlayLanding()
    {
        audio_manager.PlayLanding();
    }
    private void NotArrving()
    {
        shipAnimator.SetBool("Arriving", false);
    }

    private void UpdateUIText()
    {
        nextDeliveryText.text = $"Time Until Next Ship: {(int)(deliveryInterval - timer)}";
        foodText.text = $"Food at {tracker.food}%";
        waterText.text = $"Water at {tracker.water}%";
        regolithText.text = $"You have {tracker.regolith} Regolith";

        import1.text = importQueue.Count > 0 ? $"{importQueue[0].label} - ${importQueue[0].cost}" : "";
        import2.text = importQueue.Count > 1 ? $"{importQueue[1].label} - ${importQueue[1].cost}" : "";
        import3.text = importQueue.Count > 2 ? $"{importQueue[2].label} - ${importQueue[2].cost}" : "";

        output1.text = outputQueue.Count > 0 ? $"{outputQueue[0].label} - ${outputQueue[0].value}" : "";
        output2.text = outputQueue.Count > 1 ? $"{outputQueue[1].label} - ${outputQueue[1].value}" : "";
        output3.text = outputQueue.Count > 2 ? $"{outputQueue[2].label} - ${outputQueue[2].value}" : "";
    }


    public void QueueFoodImport()
    {
        if (tracker.money >= 20 && tracker.food < tracker.max_food && importQueue.Count < 3)
        {
            importQueue.Add(new ImportItem("Food Import", 20, () => tracker.food = Mathf.Min(tracker.food + 10, tracker.max_food)));
            audio_manager.PlayUIclick();
        }
        else 
            audio_manager.PlayFailedClick();
    }

    public void QueueWaterImport()
    {
        if (tracker.money >= 20 && tracker.water < tracker.max_water && importQueue.Count < 3)
        {
            importQueue.Add(new ImportItem("Water Import", 20, () => tracker.water = Mathf.Min(tracker.water + 10, tracker.max_water)));
            audio_manager.PlayUIclick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    public void QueueRegoligthExport()
    {
        if (tracker.regolith - outputQueue.Count >= 1 && outputQueue.Count < 3)
        {
            outputQueue.Add(new OutputItem("Regolith Export", 25));
            audio_manager.PlayUIclick();
        }
        else
            audio_manager.PlayFailedClick();
    }
    private void ProcessShip()
    {
        Debug.Log("Ship has arrived!");
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
            if (tracker.regolith >= 1)
            {
                tracker.money += item.value;
                tracker.regolith -= 1;
            }
        }

        // Clear both queues
        importQueue.Clear();
        outputQueue.Clear(); // You can add logic here to sell items, etc.
    }
    private IEnumerator ShowShipArrivalMessage()
    {
        
        shipArrivalText.text = "A new shipment has arrived!";
        for(int i = 0;i <= 4; i++)
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

    public OutputItem(string label, int value)
    {
        this.label = label;
        this.value = value;
    }
}

