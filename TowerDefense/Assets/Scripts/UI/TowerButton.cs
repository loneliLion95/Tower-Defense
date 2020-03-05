using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Management for buttons in the tower menu
/// </summary>
public class TowerButton : MonoBehaviour
{
    /// <summary>
    /// Data of the tower that represent this tower button
    /// </summary>
    public TowerData towerData;
    /// <summary>
    /// Tower that will be built when clicking the buildButton
    /// </summary>
    public GameObject tower;

    /// <summary>
    /// Background of the tower button, that will be shine when button is selected
    /// </summary>
    public Image towerBlock;

    /// <summary>
    /// Tower button that was selected
    /// </summary>
    public static TowerButton selectedTowerButton;

    private TowerMenu towerMenu;
    private Balance balance;

    void Start()
    {
        towerMenu = TowerMenu.Instance;
        balance = Balance.Instance;
    }

    void Update()
    {
        // Glow and fade of towerButton when currencyAmount is enough for build the tower
        if (balance.currencyAmount >= towerData.buildPrice)
            GetComponent<Image>().color = Color.white;
        else
            GetComponent<Image>().color = Color.gray;

        // Glow and fade of towerBlock when tower button is selected or not
        if (selectedTowerButton == this)
            towerBlock.color = Color.yellow;
        else
            towerBlock.color = Color.gray;
    }

    /// <summary>
    /// Handler for clicking the tower button
    /// </summary>
    public void SelectButton()
    {
        if (selectedTowerButton != this)
            selectedTowerButton = this;
        
        towerMenu.buildingPricePanel.SetActive(true);
        towerMenu.buildingPriceText.text = towerData.buildPrice.ToString();

        if (balance.currencyAmount >= towerData.buildPrice && TowerMenu.selectedTower == null)
            towerMenu.buildButton.SetActive(true);
    }
}
