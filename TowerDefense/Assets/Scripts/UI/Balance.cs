using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Management of the most important data for the player
/// </summary>
public class Balance : Loader<Balance>
{
    public int startCurrencyAmount;
    
    private TextMeshProUGUI healthPointsText;
    private TextMeshProUGUI currencyAmountText;
    
    [HideInInspector]
    public TextMeshProUGUI wavesCount;
    [HideInInspector]
    public int healthPoints = 100;
    [HideInInspector]
    public int currencyAmount;

    void Start()
    {
        healthPointsText = GameObject.Find("healthPoints").GetComponent<TextMeshProUGUI>();
        currencyAmountText = GameObject.Find("currencyAmount").GetComponent<TextMeshProUGUI>();
        wavesCount = GameObject.Find("wavesCount").GetComponent<TextMeshProUGUI>();
        currencyAmount = startCurrencyAmount;
    }

    void Update()
    {
        healthPointsText.text = healthPoints.ToString();
        currencyAmountText.text = currencyAmount.ToString();

        if (healthPoints <= 0)
        {
            healthPoints = 0;
            // Load the losing scene
            SceneManager.LoadScene(2);  
        }
    }
}
