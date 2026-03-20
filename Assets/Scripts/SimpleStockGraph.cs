using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SimpleStockGraph : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform graphContainer;
    public GameObject pointPrefab;
    public GameObject linePrefab;
    public TMP_Text titleText;
    public TMP_Text currentPriceText;
    public TMP_Text highPriceText;
    public TMP_Text lowPriceText;
    
    [Header("Graph Settings")]
    public Color regolithColor = Color.gray;
    public Color titaniumColor = Color.blue;
    public Color lithiumColor = Color.green;
    public float pointSize = 8f;
    public float lineThickness = 2f;
    
    private List<GameObject> activePoints = new List<GameObject>();
    private List<GameObject> activeLines = new List<GameObject>();
    private string[] resourceNames = { "Regolith", "Titanium", "Lithium" };
    
    public void DisplayPriceHistory(int[] priceHistory, int resourceIndex)
    {
        if (priceHistory == null || priceHistory.Length == 0)
        {
            Debug.LogWarning("No price history to display");
            return;
        }
        
        ClearGraph();
        
        int minPrice = priceHistory.Min();
        int maxPrice = priceHistory.Max();
        int priceRange = Mathf.Max(1, maxPrice - minPrice);
        
        float graphWidth = graphContainer.rect.width;
        float graphHeight = graphContainer.rect.height;
        float padding = 20f;
        float usableWidth = graphWidth - (padding * 2);
        float usableHeight = graphHeight - (padding * 2);
        
        Color resourceColor = GetResourceColor(resourceIndex);
        
        Vector2 previousPoint = Vector2.zero;
        
        for (int i = 0; i < priceHistory.Length; i++)
        {
            float xPosition = padding + (i / (float)(priceHistory.Length - 1)) * usableWidth;
            float yPosition = padding + ((priceHistory[i] - minPrice) / (float)priceRange) * usableHeight;
            
            Vector2 currentPoint = new Vector2(xPosition - graphWidth / 2f, yPosition - graphHeight / 2f);
            
            CreatePoint(currentPoint, resourceColor);
            
            if (i > 0)
            {
                CreateLine(previousPoint, currentPoint, resourceColor);
            }
            
            previousPoint = currentPoint;
        }
        
        UpdateTextLabels(resourceIndex, priceHistory[priceHistory.Length - 1], maxPrice, minPrice);
    }
    
    void CreatePoint(Vector2 position, Color color)
    {
        GameObject point;
        
        if (pointPrefab != null)
        {
            point = Instantiate(pointPrefab, graphContainer);
        }
        else
        {
            point = new GameObject("Point");
            point.transform.SetParent(graphContainer);
            Image img = point.AddComponent<Image>();
            img.color = color;
        }
        
        RectTransform rectTransform = point.GetComponent<RectTransform>();
        if (rectTransform == null)
            rectTransform = point.AddComponent<RectTransform>();
            
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(pointSize, pointSize);
        rectTransform.localScale = Vector3.one;
        
        activePoints.Add(point);
    }
    
    void CreateLine(Vector2 startPos, Vector2 endPos, Color color)
    {
        GameObject line;
        
        if (linePrefab != null)
        {
            line = Instantiate(linePrefab, graphContainer);
        }
        else
        {
            line = new GameObject("Line");
            line.transform.SetParent(graphContainer);
            Image img = line.AddComponent<Image>();
            img.color = color;
        }
        
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        if (rectTransform == null)
            rectTransform = line.AddComponent<RectTransform>();
        
        Vector2 direction = endPos - startPos;
        float distance = direction.magnitude;
        
        rectTransform.anchoredPosition = startPos + direction / 2f;
        rectTransform.sizeDelta = new Vector2(distance, lineThickness);
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        
        activeLines.Add(line);
    }
    
    void ClearGraph()
    {
        foreach (GameObject point in activePoints)
        {
            if (point != null)
                Destroy(point);
        }
        activePoints.Clear();
        
        foreach (GameObject line in activeLines)
        {
            if (line != null)
                Destroy(line);
        }
        activeLines.Clear();
    }
    
    Color GetResourceColor(int resourceIndex)
    {
        switch (resourceIndex)
        {
            case 0: return regolithColor;
            case 1: return titaniumColor;
            case 2: return lithiumColor;
            default: return Color.white;
        }
    }
    
    void UpdateTextLabels(int resourceIndex, int currentPrice, int highPrice, int lowPrice)
    {
        if (titleText != null)
        {
            titleText.text = resourceNames[resourceIndex] + " Market";
        }
        
        if (currentPriceText != null)
        {
            currentPriceText.text = "Current: $" + currentPrice;
        }
        
        if (highPriceText != null)
        {
            highPriceText.text = "High: $" + highPrice;
        }
        
        if (lowPriceText != null)
        {
            lowPriceText.text = "Low: $" + lowPrice;
        }
    }
    
    void OnDestroy()
    {
        ClearGraph();
    }
}
