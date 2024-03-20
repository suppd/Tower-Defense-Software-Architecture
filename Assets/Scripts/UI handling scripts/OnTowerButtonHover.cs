using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
