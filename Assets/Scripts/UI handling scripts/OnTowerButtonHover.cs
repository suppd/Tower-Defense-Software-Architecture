
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Simple script that handles showing the towerinfo panels on build buttons
/// </summary>
public class OnTowerButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image towerPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        towerPanel.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        towerPanel.gameObject.SetActive(false);
    }
}
