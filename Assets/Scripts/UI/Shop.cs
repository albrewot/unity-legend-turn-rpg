using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour {
    [SerializeField] public static Shop instance;
    [SerializeField] public GameObject shopMenu;
    [SerializeField] public GameObject buyWindow;
    [SerializeField] public GameObject sellWindow;
    [SerializeField] public GameObject[] windows;
    [SerializeField] public TextMeshProUGUI goldText;
    [SerializeField] public string[] itemsForSale;
    [SerializeField] public float sellPercentage;
    [SerializeField] public ItemButton[] buyButtons;
    [SerializeField] public ItemButton[] sellButtons;

    [SerializeField] public Item selectedItem;
    [SerializeField] public TextMeshProUGUI buyItemName, buyItemDescription, buyItemValue;
    [SerializeField] public TextMeshProUGUI sellItemName, sellItemDescription, sellItemValue;

    //Methods
    public void OpenShop() {
        shopMenu.SetActive(true);
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        ToggleWindow(0);
        PlayerController.instance.currentState = PlayerState.menu;
    }

    public void CloseShop() {
        shopMenu.SetActive(false);
        PlayerController.instance.currentState = PlayerState.walk;
    }

    public void ShowItems(int windowIndex) {
        switch (windowIndex) {
            case 0:
                for (int i = 0; i < buyButtons.Length; i++) {
                    buyButtons[i].buttonValue = i;
                    if(itemsForSale[i] != "") {
                        buyButtons[i].buttonImage.gameObject.SetActive(true);
                        buyButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                        buyButtons[i].amountText.text = "";
                    } else {
                        buyButtons[i].buttonImage.gameObject.SetActive(false);
                        buyButtons[i].amountText.text = "";
                    }

                };
                break;
            case 1:
                GameManager.instance.SortItems();   
                for (int i = 0; i < sellButtons.Length; i++) {
                    sellButtons[i].buttonValue = i;
                    if (GameManager.instance.itemsHeld[i] != "") {
                        sellButtons[i].buttonImage.gameObject.SetActive(true);
                        sellButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                        sellButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
                    } else {
                        sellButtons[i].buttonImage.gameObject.SetActive(false);
                        sellButtons[i].amountText.text = "";
                    }
                };
                break;
        }
    }

    public void ToggleWindow(int windowIndex) {
        for (int i = 0; i < windows.Length; i++) {
            if (i == windowIndex) {
                ShowItems(windowIndex);
                windows[i].SetActive(true);
                switch (i) {
                    case 0: buyButtons[0].Press(); break;
                    case 1: sellButtons[0].Press(); break;
                };
            } else {
                windows[i].SetActive(false);
            }
        }
    }

    public void SelectBuyItem(Item buyItem) {
        selectedItem = buyItem;

        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = $"Value: {selectedItem.value}g";
    }

    public void SelectSellItem(Item sellItem) {
        selectedItem = sellItem;
        //Debug.Log($"Sell name: {selectedItem.itemName} | description: {selectedItem.description} | value: {selectedItem.value}");
        int sellValue = Mathf.FloorToInt(sellItem.value * sellPercentage);
        sellItemName.text = sellItem.itemName;
        sellItemDescription.text = sellItem.description;
        sellItemValue.text = $"Value: {sellValue}g";
    }

    public void makeAction(int action) {
        int sellValue = Mathf.FloorToInt(selectedItem.value * sellPercentage);

        if (selectedItem != null) {
            switch (action) {
                case 0: 
                    if(GameManager.instance.currentGold >= selectedItem.value) {
                        GameManager.instance.currentGold -= selectedItem.value;
                        GameManager.instance.AddItem(selectedItem.itemName);
                    }; 
                    break;
                case 1: 
                    GameManager.instance.currentGold += sellValue;
                    int isInInventory = GameManager.instance.RemoveItem(selectedItem.itemName);
                    if (isInInventory == 0) sellButtons[0].Press();
                    break;
            }
        }

        goldText.text = $"{GameManager.instance.currentGold}g";
    }
    public void BuyItem() {
        makeAction(0);
    }

    public void SellItem() {
        makeAction(1);
        ShowItems(1);
    }





    // Start is called before the first frame update
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
    }
}
