using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class StockMarket : MonoBehaviour
{
    Variable_Tracker variableTracker;
    public GameObject graphHandlerObject;
    public GraphHandler graphHandler;
    public TMP_Text graphTitleText;
    public TMP_Text currentPriceText;
    
    [SerializeField] private Vector2 stockMarketFluctuationRatioRange = new Vector2(-10f, 11f);
    [SerializeField] private float fluctuationStrength = 1f;
    public int RegolithStartingPrice = 25;
    public int TitaniumStartingPrice = 40;
    public int LithiumStartingPrice = 80;
    public float GraphYStretchMultiplier = 1.2f;

    private int currentResourceIndex = 0;
    private string[] resourceNames = { "Regolith", "Titanium", "Lithium" };

    void Start()
    {
        variableTracker = GetComponent<Variable_Tracker>();

        for (int i = 0; i < 3; i++)
        {
            AddUpdatedPrices();
        }
    }

    int[] HighestPoints()
    {
        int[] highestPoints = new int[variableTracker.resourcePrices.Length];
        for (int i = 0; i < variableTracker.resourcePrices.Length; i++)
        {
            highestPoints[i] = variableTracker.resourcePrices[i].Max();
        }
        return highestPoints;
    }

    int[] LowestPoints()
    {
        int[] lowestPoints = new int[variableTracker.resourcePrices.Length];
        for (int i = 0; i < variableTracker.resourcePrices.Length; i++)
        {
            lowestPoints[i] = variableTracker.resourcePrices[i].Min();
        }
        return lowestPoints;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenGraph();
        }

        if (graphHandlerObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchToPreviousResource();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchToNextResource();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseGraph();
            }
        }
    }

    public void OpenGraph()
    {
        graphHandlerObject.SetActive(true);
        currentResourceIndex = 0;
        DisplayResource(currentResourceIndex);
    }

    public void CloseGraph()
    {
        graphHandlerObject.SetActive(false);
    }

    public void SwitchToNextResource()
    {
        currentResourceIndex = (currentResourceIndex + 1) % resourceNames.Length;
        DisplayResource(currentResourceIndex);
    }

    public void SwitchToPreviousResource()
    {
        currentResourceIndex--;
        if (currentResourceIndex < 0)
            currentResourceIndex = resourceNames.Length - 1;
        DisplayResource(currentResourceIndex);
    }

    public void DisplayResource(int resourceIndex)
    {
        if (graphHandler == null)
        {
            Debug.LogError("GraphHandler is null!");
            return;
        }

        if (resourceIndex < 0 || resourceIndex >= variableTracker.resourcePrices.Length)
        {
            Debug.LogError("Invalid resource index: " + resourceIndex);
            return;
        }

        graphHandler.ClearAllPoints();
        FormatGraph(resourceIndex);
        PlotPointsInResource(resourceIndex);
        UpdateUIText(resourceIndex);
    }

    void UpdateUIText(int resourceIndex)
    {
        string resourceName = resourceNames[resourceIndex];
        int currentPrice = GetCurrentPrice(resourceIndex);
        
        if (graphTitleText != null)
        {
            graphTitleText.text = resourceName + " Market Price History";
        }
        
        if (currentPriceText != null)
        {
            currentPriceText.text = "Current Price: $" + currentPrice;
        }
    }

    public int GetCurrentPrice(int resourceIndex)
    {
        if (resourceIndex < 0 || resourceIndex >= variableTracker.resourcePrices.Length)
            return 0;
        
        if (variableTracker.resourcePrices[resourceIndex].Length == 0)
            return 0;
            
        return variableTracker.resourcePrices[resourceIndex][variableTracker.resourcePrices[resourceIndex].Length - 1];
    }

    public void AddUpdatedPrices()
    {
        for (int i = 0; i < variableTracker.resourcePrices.Length; i++)
        {
            UpdateResource(i);
        }
        
        if (graphHandlerObject.activeSelf)
        {
            DisplayResource(currentResourceIndex);
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
        
        int lastPrice = variableTracker.resourcePrices[resourceIndex][variableTracker.resourcePrices[resourceIndex].Length - 1];
        int newPrice = Mathf.Max(1, lastPrice + resourcePriceChange);
        newPriceArray[newPriceArray.Length - 1] = newPrice;

        variableTracker.resourcePrices[resourceIndex] = newPriceArray;
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

    void FormatGraph(int resourceIndex)
    {
        int maxPrice = HighestPoints()[resourceIndex];
        int minPrice = LowestPoints()[resourceIndex];
        
        float yMin = Mathf.Max(0, minPrice * 0.8f);
        float yMax = maxPrice * GraphYStretchMultiplier;
        
        graphHandler.SetCornerValues(
            new Vector2(0f, yMin), 
            new Vector2(variableTracker.resourcePrices[resourceIndex].Length - 1, yMax)
        );
    }

    void PlotPointsInResource(int resourceIndex)
    {
        for (int i = 0; i < variableTracker.resourcePrices[resourceIndex].Length; i++)
        {
            Vector2 graphPointPosition = new Vector2(i, variableTracker.resourcePrices[resourceIndex][i]);
            graphHandler.CreatePoint(graphPointPosition);
        }
        graphHandler.UpdateGraph();
    }

    

}
