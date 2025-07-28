using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockMarket : MonoBehaviour
{
    // Start is called before the first frame update

    public Variable_Tracker variableTracker; // Reference to the Variable_Tracker script
    public Vector2 stockMarketRange = new Vector2(-10f, 11f); // Range for stock market fluctuations

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void UpdateStockMarket()
    {
        // Generate a random fluctuation within the specified range
        float fluctuation = Random.Range(stockMarketRange.x, stockMarketRange.y);
    }
}
