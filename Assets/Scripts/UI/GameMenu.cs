using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour {
    public static GameMenu instance;
    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject[] windows;
    [SerializeField] public GameObject[] statusBtn;

    //Main Menu Elements
    [SerializeField] public TextMeshProUGUI[] nameText, hpText, mpText, expText, lvlText;
    [SerializeField] public Image[] charImage;
    [SerializeField] public Slider[] expSlider;
    [SerializeField] public GameObject[] charStatHolder;
    [SerializeField] public TextMeshProUGUI goldText;

    //Status window Elements

    [SerializeField] TextMeshProUGUI statusName, statusLvl, statusSP, statusHP, statusMP, statusStrength, statusIntelligence, statusDefense, statusEquipWpn, statusWpnPwer, statusEquipArmor, statusArmrPwer, statusExpToNext;
    [SerializeField] Image statusImage;

    //Inventory Elements
    [SerializeField] public ItemButton[] itemButtons;
    [SerializeField] public string selectedItem;
    [SerializeField] public Item activeItem;
    [SerializeField] public TextMeshProUGUI itemNameText, itemDescrptionText, useButtonText;

    //Use Panel Elements
    [SerializeField] public GameObject ItemCharacterChoiceMenu;
    [SerializeField] public TextMeshProUGUI[] itemCharacterChoiceNames;

    //Private
    private CharacterStats[] playerStats;

    //Methods
    public void OpenMenu() {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (!menu.activeInHierarchy) {
                Time.timeScale = 0;
                menu.SetActive(true);
                UpdateStats();
                PlayerController.instance.currentState = PlayerState.menu;
            } else {
                CloseMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            GameManager.instance.AddItem("Wooden Sword");
            GameManager.instance.RemoveItem("HP Potion");
        }
    }

    public void OpenCharacterChoice() {
        ItemCharacterChoiceMenu.SetActive(true);

        for(int i = 0; i < itemCharacterChoiceNames.Length; i++) {
            itemCharacterChoiceNames[i].text = GameManager.instance.playerStats[i].characterName;
            itemCharacterChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseCharacterChoice() {
        ItemCharacterChoiceMenu.SetActive(false);
    }

    public void UpdateStats() {
        playerStats = GameManager.instance.playerStats;
        for (int i = 0; i < playerStats.Length; i++) {
            if (playerStats[i].gameObject.activeInHierarchy) {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].characterName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLvl + "/" + playerStats[i].maxLvl;
                expText[i].text = playerStats[i].currentEXP + "/" + playerStats[i].expToNextLvl[playerStats[i].playerLvl];
                expSlider[i].maxValue = playerStats[i].expToNextLvl[playerStats[i].playerLvl];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].characterImage;
            } else {
                charStatHolder[i].SetActive(false);
            }
        }

        goldText.text = GameManager.instance.currentGold + "g";
    }

    public void ToggleWindow(int windowIndex) {
        for (int i = 0; i < windows.Length; i++) {
            if (i == windowIndex) {
                UpdateStats();
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else {
                windows[i].SetActive(false);
            }
        }
        ItemCharacterChoiceMenu.SetActive(false);
        //CloseCharacterChoice();
    }

    public void OpenStatus() {
        UpdateStats();
        StatusChar(0);
        for (int i = 0; i < statusBtn.Length; i++) {
            statusBtn[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusBtn[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].characterName;
        }
    }

    public void StatusChar(int selected) {
        statusName.text = playerStats[selected].characterName;
        statusLvl.text = playerStats[selected].playerLvl + "/" + playerStats[selected].maxLvl;
        statusSP.text = playerStats[selected].skillPoints.ToString();
        statusHP.text = playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStrength.text = playerStats[selected].strength.ToString();
        statusIntelligence.text = playerStats[selected].intelligence.ToString();
        statusDefense.text = playerStats[selected].defense.ToString();
        statusEquipWpn.text = (playerStats[selected].equippedWeapon != "") ? playerStats[selected].equippedWeapon : "None";
        statusWpnPwer.text = playerStats[selected].weaponPower.ToString();
        statusEquipArmor.text = (playerStats[selected].equippedArmor != "") ? playerStats[selected].equippedArmor : "None";
        statusArmrPwer.text = playerStats[selected].armorPower.ToString();
        statusExpToNext.text = playerStats[selected].currentEXP + "/" + playerStats[selected].expToNextLvl[playerStats[selected].playerLvl];
        statusImage.sprite = playerStats[selected].characterImage;
    }

    public void CloseMenu() {
        for (int i = 0; i < windows.Length; i++) {
            windows[i].SetActive(false);
        }

        Time.timeScale = 1;
        PlayerController.instance.currentState = PlayerState.walk;
        menu.SetActive(false);
        ItemCharacterChoiceMenu.SetActive(false);
    }

    public void ShowItems() {

        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++) {
            itemButtons[i].buttonValue = i;
            if (GameManager.instance.itemsHeld[i] != "") {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            } else {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item selectedItem) {
        activeItem = selectedItem;

        if (selectedItem.isItem) {
            useButtonText.text = "Use";
        }

        if(selectedItem.isWeapon || selectedItem.isArmour) {
            useButtonText.text = "Equip";
        }

        itemNameText.text = selectedItem.itemName;
        itemDescrptionText.text = selectedItem.description;
    }

    public void DiscardItem() {
        if(activeItem != null) {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void UseItem(int selectedCharacter) {
        Debug.Log("Triggered Use Item Func");
        activeItem.Use(selectedCharacter);
        CloseCharacterChoice();
    }

    // Start is called before the first frame update
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        OpenMenu();
    }
}
