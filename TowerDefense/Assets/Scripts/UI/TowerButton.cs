using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    /// Display panel for cost of the selected tower
    /// </summary>
    public GameObject costPanel;
    /// <summary>
    /// Button for building the tower
    /// </summary>
    public GameObject buildButton;
    /// <summary>
    /// Background of the tower button, that will be shine when button is selected
    /// </summary>
    public Image towerBlock;
    /// <summary>
    /// Tower that will be built when clicking the buildButton
    /// </summary>
    public GameObject tower;

    private Balance balance;

    /// <summary>
    /// Tower button that was pressed
    /// </summary>
    public static TowerButton selectedButton;
    
    void Start()
    {
        balance = Balance.Instance;
    }

    // Glow and fade when tower button is pressed
    void Update()
    {
        if (balance.currencyAmount >= towerData.buildPrice)
            GetComponent<Image>().color = Color.white;
        else
            GetComponent<Image>().color = Color.gray;

        if (selectedButton != null)
            selectedButton.towerBlock.color = Color.yellow;

        if (selectedButton != this)
            towerBlock.color = Color.gray;
    }

    /// <summary>
    /// Handler for clicking the tower button
    /// </summary>
    public void SelectButton()
    {
        if (selectedButton != this)
            selectedButton = this;

        costPanel.SetActive(true);
        costPanel.GetComponentInChildren<TextMeshProUGUI>().text = towerData.buildPrice.ToString();

        if (balance.currencyAmount >= towerData.buildPrice && TowerMenu.selectedTower == null)
            buildButton.SetActive(true);
        else
            buildButton.SetActive(false);
    }
}
