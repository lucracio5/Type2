using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockMarket : MonoBehaviour
{
    // Start is called before the first frame update

    public Variable_Tracker variableTracker; // Reference to the Variable_Tracker script
    [SerializeField] private Vector2 stockMarketFluctuationRatioRange = new Vector2(-10f, 11f); // Range for stock market fluctuations
    [SerializeField] private float fluctuationStrength = 1f;
    public int RegolithStartingPrice = 25;
    public int TitaniumStartingPrice = 25;
    public int LithiumStartingPrice = 25;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void UpdateRegolith()
    {
        // Generate a random fluctuation within the specified range
        float fluctuationFloat = Random.Range(stockMarketFluctuationRatioRange.x, stockMarketFluctuationRatioRange.y);
    }
}
