using UnityEngine;
using System.Linq;
using System.Collections;

public class SimpleStockMarket : MonoBehaviour
{
    [Header("References")]
    public Variable_Tracker variableTracker;
    public GameObject graphPanel;
    public SimpleStockGraph stockGraph;
    
    [Header("Price Settings")]
    public int RegolithStartingPrice = 25;
    public int TitaniumStartingPrice = 40;
    public int LithiumStartingPrice = 80;
    
    [Header("Market Settings")]
    [SerializeField] private Vector2 stockMarketFluctuationRatioRange = new Vector2(-10f, 11f);
    [SerializeField] private float fluctuationStrength = 1f;

    private int currentResourceIndex = 0;
    private string[] resourceNames = { "Regolith", "Titanium", "Lithium" };
    float timer;
    void Start()
    {
        if (variableTracker == null)
            variableTracker = GetComponent<Variable_Tracker>();
        
        if (variableTracker == null)
        {
            Debug.LogError("SimpleStockMarket: Variable_Tracker not found!");
            return;
        }

        StartCoroutine(InitializeAfterVariableTracker());
        
        if (graphPanel != null)
            graphPanel.SetActive(false);
    }
    
    IEnumerator InitializeAfterVariableTracker()
    {
        yield return new WaitForEndOfFrame();
        
        if (variableTracker != null && variableTracker.resourcePrices != null)
        {
            for (int i = 0; i < 3; i++)
            {
                AddUpdatedPrices();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenGraph();
        }

        if (graphPanel != null && graphPanel.activeSelf)
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
        timer += Time.deltaTime * variableTracker.speed;

        if (timer >= 10)
        {
            timer = 0;
            AddUpdatedPrices();
        }
    }

    public void OpenGraph()
    {
        if (graphPanel != null)
            graphPanel.SetActive(true);
        
        currentResourceIndex = 0;
        DisplayCurrentResource();
    }

    public void CloseGraph()
    {
        if (graphPanel != null)
            graphPanel.SetActive(false);
    }

    public void SwitchToNextResource()
    {
        currentResourceIndex = (currentResourceIndex + 1) % resourceNames.Length;
        DisplayCurrentResource();
    }

    public void SwitchToPreviousResource()
    {
        currentResourceIndex--;
        if (currentResourceIndex < 0)
            currentResourceIndex = resourceNames.Length - 1;
        DisplayCurrentResource();
    }

    void DisplayCurrentResource()
    {
        if (stockGraph == null)
        {
            Debug.LogError("Stock Graph reference is missing!");
            return;
        }
        
        if (variableTracker == null)
        {
            Debug.LogError("Variable Tracker is missing!");
            return;
        }

        if (currentResourceIndex < 0 || currentResourceIndex >= variableTracker.resourcePrices.Length)
        {
            Debug.LogError("Invalid resource index: " + currentResourceIndex);
            return;
        }

        stockGraph.DisplayPriceHistory(variableTracker.resourcePrices[currentResourceIndex], currentResourceIndex);
    }

    public void AddUpdatedPrices()
    {
        if (variableTracker == null || variableTracker.resourcePrices == null)
        {
            Debug.LogWarning("Cannot update prices - Variable Tracker or resourcePrices is null");
            return;
        }
        
        for (int i = 0; i < variableTracker.resourcePrices.Length; i++)
        {
            UpdateResource(i);
        }
        
        if (graphPanel != null && graphPanel.activeSelf)
        {
            DisplayCurrentResource();
        }
    }

    void UpdateResource(int resourceIndex)
    {
        float fluctuationFloat = Random.Range(stockMarketFluctuationRatioRange.x, stockMarketFluctuationRatioRange.y);
        int resourcePriceChange = Mathf.RoundToInt(fluctuationFloat * fluctuationStrength);

        int[] newPriceArray = new int[variableTracker.resourcePrices[resourceIndex].Length + 1];
        for (int i = 0; i < variableTracker.resourcePrices[resourceIndex].Length; i++)
        {
            newPriceArray[i] = variableTracker.resourcePrices[resourceIndex][i];
        }
        
        int lastPrice = variableTracker.resourcePrices[resourceIndex][variableTracker.resourcePrices[resourceIndex].Length - 1];
        int newPrice = Mathf.Max(1, lastPrice + resourcePriceChange);
        newPriceArray[newPriceArray.Length - 1] = newPrice;

        variableTracker.resourcePrices[resourceIndex] = newPriceArray;
    }

    public int GetCurrentPrice(int resourceIndex)
    {
        if (resourceIndex < 0 || resourceIndex >= variableTracker.resourcePrices.Length)
            return 0;
        
        if (variableTracker.resourcePrices[resourceIndex].Length == 0)
            return 0;
            
        return variableTracker.resourcePrices[resourceIndex][variableTracker.resourcePrices[resourceIndex].Length - 1];
    }
}
