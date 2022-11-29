using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chip_UI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public int chip_id;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(ChipManager.instance.SelectedChips.Count < 3)
        {
            ChipManager.instance.SelectedChips.Add(ChipManager.instance.GetChipByID(chip_id));
            UI_Manager.instance.UpdateSelectedChips();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_Manager.instance.PopupChip(chip_id);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
