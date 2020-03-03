using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Description of tower menu actions
/// </summary>
public class TowerMenu : Loader<TowerMenu>
{
    public GameObject towerMenu;
    /// <summary>
    /// Panel with selling price of the selected tower
    /// </summary>
    public GameObject sellingPricePanel;
    /// <summary>
    /// Button for selling the tower
    /// </summary>
    public Button sellingButton;

    private Balance balance;
    private TextMeshProUGUI sellingPriceText;

    /// <summary>
    /// Tower that was builded on this tower slot
    /// </summary>
    [HideInInspector]
    public Tower buildedTower;

    /// <summary>
    /// Tower that was selected
    /// </summary>
    public static Tower selectedTower;

    void Start()
    {
        balance = Balance.Instance;
        sellingPriceText = sellingPricePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Handler for clicking on tower slots
    /// </summary>
    public void OpenCloseTowerMenu()
    {
        Refresh();

        selectedTower = null;

        if (!towerMenu.activeSelf)
        {
            towerMenu.SetActive(true);
            towerMenu.transform.position = transform.position;
        }
        else
        {
            if (towerMenu.transform.position == transform.position)
                towerMenu.SetActive(false);
            else
                towerMenu.transform.position = transform.position;
        }

        if (buildedTower != null)
        {
            selectedTower = buildedTower;
            sellingPricePanel.SetActive(true);
            sellingPriceText.text = buildedTower.sellingPrice.ToString();
            sellingButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Handler for clicking the build button
    /// </summary>
    public void BuildTower()
    {
        Instantiate(TowerButton.selectedButton.tower, transform.position, Quaternion.identity);
        towerMenu.SetActive(false);
        balance.currencyAmount -= TowerButton.selectedButton.towerData.buildPrice;
        Refresh();
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
    private void Refresh()
    {
        TowerButton selectedButton = TowerButton.selectedButton;

        if (selectedButton != null)
        {
            selectedButton.costPanel.SetActive(false);
            selectedButton.buildButton.SetActive(false);
            selectedButton.towerBlock.color = Color.gray;
            TowerButton.selectedButton = null;
        }

        sellingPricePanel.SetActive(false);
        sellingButton.gameObject.SetActive(false);
    }
}
