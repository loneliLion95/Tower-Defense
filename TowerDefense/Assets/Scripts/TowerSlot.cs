using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    /// <summary>
    /// Tower that was builded on this tower slot
    /// </summary>
    [HideInInspector]
    public Tower buildedTower;

    public static TowerSlot selectedSlot;

    public void SelectTowerSlot()
    {
        if (selectedSlot != this)
            selectedSlot = this;
            
        TowerMenu.Instance.OpenCloseTowerMenu();
    }
}
