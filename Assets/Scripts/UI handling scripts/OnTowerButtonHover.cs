using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnTowerButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image TowerPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TowerPanel.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TowerPanel.gameObject.SetActive(false);
    }
}
