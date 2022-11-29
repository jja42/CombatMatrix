using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public GameObject Loss_UI;
    public GameObject Win_UI;
    public GameObject Pause_UI;
    public GameObject Chip_Menu;
    public GameObject AvailableChips;
    public GameObject SelectedChips;
    public TextMeshProUGUI Chip_text;
    public Image Chip_image;
    
    public static UI_Manager instance;

    public GameObject Chips_Ready;
    public Slider Chip_Slider;
    public float chip_charge_rate;
    bool can_charge;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        can_charge = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Chip_Slider.value < 100 && can_charge)
        {
            Chip_Slider.value += Time.deltaTime * chip_charge_rate;
            Chips_Ready.SetActive(false);
        }
        else
        {
            Chips_Ready.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Return) && Chip_Slider.value >= 100)
        {
            Chip_Slider.value = 0;
            can_charge = false;
            ChipMenuOpen();
        }
    }
    
    void ChipMenuOpen()
    {
        GameManager.instance.paused = true;
        GameManager.instance.menu = true;
        Chip_Menu.SetActive(true);
        ChipManager.instance.GenerateAvailableChips();
        ClearSelectedChips();
        Image[] icons = AvailableChips.GetComponentsInChildren<Image>();
        Chip_UI[] chips = AvailableChips.GetComponentsInChildren<Chip_UI>();
        string path = "";
        for(int i =0; i<icons.Length; i++)
        {
            path = ChipManager.instance.AvailableChips[i].chip_id.ToString();
            chips[i].chip_id = ChipManager.instance.AvailableChips[i].chip_id;
            icons[i].sprite = Resources.Load<Sprite>(path);
        }
    }
    public void ChipMenuClose()
    {
        if (ChipManager.instance.SelectedChips.Count == 3)
        {
            Chip_Menu.SetActive(false);
            can_charge = true;
        }
        GameManager.instance.paused = false;
        GameManager.instance.menu = false;
    }

    public void PopupChip(int chip_id)
    {
        string path = chip_id.ToString();
        Chip_image.sprite = Resources.Load<Sprite>(path);
        ChipManager.Chip chip = ChipManager.instance.GetChipByID(chip_id);
        Chip_text.text = chip.nickname + " " + chip.damage;
    }

    public void UpdateSelectedChips()
    {
        Image[] icons = SelectedChips.GetComponentsInChildren<Image>();
        string path = "";
        for (int i = 0; i < ChipManager.instance.SelectedChips.Count; i++)
        {
            path = ChipManager.instance.SelectedChips[i].chip_id.ToString();
            icons[i].sprite = Resources.Load<Sprite>(path);
        }
    }
    void ClearSelectedChips()
    {
        Image[] icons = SelectedChips.GetComponentsInChildren<Image>();
        for(int i =0; i<icons.Length; i++)
        {
            icons[i].sprite = null;
        }
    }
}
