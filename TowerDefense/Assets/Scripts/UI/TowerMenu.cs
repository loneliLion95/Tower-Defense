using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Description of tower menu actions
/// </summary>
public class TowerMenu : Loader<TowerMenu>
{
    public GameObject towerMenu;

    [HideInInspector]
    public GameObject buildingPricePanel;
    [HideInInspector]
    public TextMeshProUGUI buildingPriceText;
    [HideInInspector]
    public GameObject buildButton;
    [HideInInspector]
    public GameObject sellingPricePanel;
    [HideInInspector]
    public TextMeshProUGUI sellingPriceText;
    [HideInInspector]
    public GameObject sellButton;
    
    /// <summary>
    /// Tower that was selected
    /// </summary>
    public static Tower selectedTower;

    private Balance balance;
    private TowerSlot selectedSlot;

    void Start()
    {
        buildingPricePanel = towerMenu.transform.Find("buildingPricePanel").gameObject;
        buildingPriceText = buildingPricePanel.GetComponentInChildren<TextMeshProUGUI>();
        buildButton = towerMenu.transform.Find("buildButton").gameObject;
        sellingPricePanel = towerMenu.transform.Find("sellingPricePanel").gameObject;
        sellingPriceText = sellingPricePanel.GetComponentInChildren<TextMeshProUGUI>();
        sellButton = towerMenu.transform.Find("sellButton").gameObject;
        balance = Balance.Instance;
    }

    /// <summary>
    /// Handler for clicking on tower slot buttons
    /// </summary>
    public void OpenCloseTowerMenu()
    {
        Refresh();

        selectedSlot = TowerSlot.selectedSlot;
        selectedTower = null;

        if (!towerMenu.activeSelf)
        {
            towerMenu.SetActive(true);
            towerMenu.transform.position = selectedSlot.transform.position;
        }
        else
        {
            if (towerMenu.transform.position == selectedSlot.transform.position)
                towerMenu.SetActive(false);
            else
                towerMenu.transform.position = selectedSlot.transform.position;
        }

        if (selectedSlot.buildedTower != null)
        {
            selectedTower = selectedSlot.buildedTower;
            sellingPricePanel.SetActive(true);
            sellingPriceText.text = selectedTower.sellingPrice.ToString();
            sellButton.SetActive(true);
        }
    }

    /// <summary>
    /// Handler for clicking the build button
    /// </summary>
    public void BuildTower()
    {
        selectedSlot = TowerSlot.selectedSlot;
        TowerButton selectedTowerButton = TowerButton.selectedTowerButton;
        Instantiate(selectedTowerButton.tower, selectedSlot.transform.position, Quaternion.identity);
        towerMenu.SetActive(false);
        balance.currencyAmount -= selectedTowerButton.towerData.buildPrice;
    }

    /// <summary>
    /// Handler for clicking the selling button
    /// </summary>
    public void SellTower()
    {
        balance.currencyAmount += selectedTower.sellingPrice;
        Destroy(selectedTower.gameObject);
        towerMenu.SetActive(false);
    }

    /// <summary>
    /// Reseting the tower menu to initial state
    /// </summary>
    public void Refresh()
    {
        if (TowerButton.selectedTowerButton != null)
        {
            TowerButton.selectedTowerButton.towerBlock.color = Color.gray;
            TowerButton.selectedTowerButton = null;
        }
        
        buildingPricePanel.SetActive(false);
        buildButton.SetActive(false);
        sellingPricePanel.SetActive(false);
        sellButton.gameObject.SetActive(false);
    }
}
