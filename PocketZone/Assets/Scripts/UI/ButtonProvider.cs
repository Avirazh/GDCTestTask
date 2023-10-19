using UnityEngine;
using UnityEngine.UI;

public class ButtonProvider : MonoBehaviour
{
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _inventoryButton;

    public Button ShootButton => _shootButton;
    public Button InventoryButton => _inventoryButton;
}
