using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    [SerializeField] public Image buttonImage;
    [SerializeField] public TextMeshProUGUI amountText;
    [SerializeField] public int buttonValue;

    //Methods
    public void Press() {

        if (GameMenu.instance.menu.activeInHierarchy) {
            if(GameManager.instance.itemsHeld[buttonValue] != "") {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }

        if (Shop.instance.shopMenu.activeInHierarchy) {
            for(int i = 0; i < Shop.instance.windows.Length; i++) {
                if (Shop.instance.windows[i].activeInHierarchy) {
                    switch (i) {
                        case 0: Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue])); break;
                        case 1: Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue])); break;
                    }
                }
            }
        }

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
