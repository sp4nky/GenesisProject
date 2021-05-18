using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class hiddePanelUI : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panelToHidde;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(hiddeShow);
    }

    private void hiddeShow()
    {
        if (panelToHidde.activeSelf)
            panelToHidde.SetActive(false);
        else
            panelToHidde.SetActive(true);
            
    }

    public void Show()
    {
        panelToHidde.SetActive(true);

    }

    public void hidde()
    {
        panelToHidde.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelToHidde.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelToHidde.gameObject.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        panelToHidde.gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        panelToHidde.gameObject.SetActive(false);
    }

}
