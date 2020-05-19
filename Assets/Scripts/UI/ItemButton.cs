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
        if(GameManager.instance.itemsHeld[buttonValue] != "") {
            GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
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
