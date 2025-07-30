using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StockMarket : MonoBehaviour
{
    // Start is called before the first frame update

    Variable_Tracker variableTracker; //reference to the Variable_Tracker script
    public GameObject graphHandlerObject;
    public GraphHandler graphHandler;
    [SerializeField] private Vector2 stockMarketFluctuationRatioRange = new Vector2(-10f, 11f); // Range for stock market fluctuations
    [SerializeField] private float fluctuationStrength = 1f;
    public int RegolithStartingPrice = 25;
    public int TitaniumStartingPrice = 40;
    public int LithiumStartingPrice = 80;
    public float GraphYStretchMultiplier = 1.2f; //how much taller the graphh will be relative to the highest point

    void Start()
    {
        variableTracker = GetComponent<Variable_Tracker>();

        for (int i = 0; i < 3; i++)
        {
            AddUpdatedPrices();
        }
    }

    //Returns an array where the index represents the resource, and the value represents the highets price found in that resources history
    int[] HighestPoints()
    {
        //Create highest points array
        int[] highestPoints = new int[variableTracker.resourcePrices.Length]; //it is however long there are resources
        for (int i = 0; i < variableTracker.resourcePrices.Length; i++)
        {
            highestPoints[i] = variableTracker.resourcePrices[i].Max();
        }
        Debug.Log("highestPoints: " + highestPoints);
        return highestPoints;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenGraph();
        }
    }


    public void OpenGraph()
    {
        graphHandlerObject.SetActive(true); //activate the graph handler
        FormatGraph(0);
        PlotPointsInResource(0); //plot regolith
    }

    //Will be called every "day" tick to add a new random value in variable tracker
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
        newPriceArray[newPriceArray.Length - 1] = resourcePriceChange; //Add the new value on the end

        Debug.Log("New array for index " + resourceIndex + ": " + ArrayToString(newPriceArray));
        variableTracker.resourcePrices[resourceIndex] = newPriceArray; //Apply the new array to variableTracker

        /* Debugging stuff
        string String = string.Empty;
        foreach (int price in variableTracker.resourcePrices[resourceIndex])
        {
            thisResourcePricesAsString += price.ToString() + ", ";
        }
        Debug.Log("Prices for index " + resourceIndex + ": " + thisResourcePricesAsString); */
    }


    string ArrayToString(int[] arr)
    {
        string String = string.Empty;
        foreach (int element in arr)
        {
            String += element.ToString() + ", ";
        }
        return String;
    }

    void FormatGraph(int resourceIndex) //positions the graph so that 0, 0 is in the bottom left, top right is a bit taller 
    {
        graphHandler.SetCornerValues(new Vector2(0f, 0f), new Vector2(variableTracker.resourcePrices[resourceIndex].Length, HighestPoints()[resourceIndex] * 1.2f)); //top right x is the amount of points (days passed), and the height is the tallest point multiplied by a constant (maybe 1.2)
        Debug.Log("Graph Formatted");
    }

    //Plots any given resource
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