using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockMarket : MonoBehaviour
{
    // Start is called before the first frame update

    Variable_Tracker variableTracker; //reference to the Variable_Tracker script
    public GraphHandler graphHandler;
    [SerializeField] private Vector2 stockMarketFluctuationRatioRange = new Vector2(-10f, 11f); // Range for stock market fluctuations
    [SerializeField] private float fluctuationStrength = 1f;
    public int RegolithStartingPrice = 25;
    public int TitaniumStartingPrice = 40;
    public int LithiumStartingPrice = 80;


    void Start()
    {
        variableTracker = GetComponent<Variable_Tracker>();


        for (int i = 0; i < 3; i++)
        {
            AddUpdatedPrices();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlotPointsInResource(0); //plot regolith
        }
    }

    //Will be called every "day" tick
    public void AddUpdatedPrices()
    {
        for (int i = 0; i < variableTracker.resourcePrices.Length; i++) //FOR EACH RESOURCE
        {
            UpdateResource(i); //Update each resource in variable tracker

        }
    }

    //Adds a new random value to any resource (given the index in the resourcePrices jagged array)
    void UpdateResource(int resourceIndex)
    {
        float fluctuationFloat = Random.Range(stockMarketFluctuationRatioRange.x, stockMarketFluctuationRatioRange.y);  //Generate a random fluctuation (positive or negative)
        int resourcePriceChange = Mathf.RoundToInt(fluctuationFloat * fluctuationStrength); //what the price affect will be (could be positive or negative)

        int[] newPriceArray = new int[variableTracker.resourcePrices[resourceIndex].Length + 1]; //make a new array that is 1 longer than it was before
        for (int i = 0; i < variableTracker.resourcePrices[resourceIndex].Length; i++) //for the length of the original array
        {
            newPriceArray[i] = variableTracker.resourcePrices[resourceIndex][i]; //copy over the original array
        }
        newPriceArray[newPriceArray.Length - 1] = newPriceArray[newPriceArray.Length - 2] + resourcePriceChange; //Add the new value on the end (based off the previous value) (-1 and -2 instead of 0 and -1 bc arrays are 0 indexed)

        variableTracker.resourcePrices[resourceIndex] = newPriceArray; //Apply the new array to variableTracker

        /* Debugging stuff
        string thisResourcePricesAsString = string.Empty;
        foreach (int price in variableTracker.resourcePrices[resourceIndex])
        {
            thisResourcePricesAsString += price.ToString() + ", ";
        }
        Debug.Log("Prices for index " + resourceIndex + ": " + thisResourcePricesAsString); */
    }


    void FormatGraph() //positions the graph so that 0, 0 is in the bottom left, it is a bit taller 
    {
        
    }

    void PlotPointsInResource(int resourceIndex)
    {
        for (int i = 0; i < variableTracker.resourcePrices[resourceIndex].Length; i++) //for each price point (i is the index of the specific point in the inner array)
        {
            Vector2 graphPointPosition = new Vector2(i, variableTracker.resourcePrices[resourceIndex][i]); //x is the index of the point in the array, y is the value of it
            Debug.Log("Plotting point at " + graphPointPosition);
            graphHandler.CreatePoint(graphPointPosition); //Plot the point
        }
        graphHandler.UpdateGraph(); //Update the graph
    }

    

}


/*for (float i = 0; i < 50; i += 0.2f)
        CreatePoint(new Vector2(i, 0.2f * i + Mathf.Sin(i)));
    UpdateGraph();


    

    float amountOFPoints = 100f;
    float curPrice = 25f;
    float highestPoint = 0f;
    for (int i = 0; i < amountOFPoints; i++)
    {
    //    CreatePoint(new Vector2(i, curPrice));

        curPrice *= UnityEngine.Random.Range(0.7f, 1.35f);
        if (curPrice > highestPoint) highestPoint = curPrice;
    }



    SetCornerValues(new Vector2(0f, 0f), new Vector2(amountOFPoints, highestPoint * 1.2f));

    UpdateGraph();*/