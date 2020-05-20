using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Attributes
    [SerializeField] public static GameManager instance;
    [SerializeField] public CharacterStats[] playerStats;
    [SerializeField] public string[] itemsHeld;
    [SerializeField] public int[] numberOfItems;
    [SerializeField] public Item[] referencedItems;
    [SerializeField] public int currentGold;

    //Methods
    public void InitializeGamemanager() {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SortItems();
    }

    public Item GetItemDetails(string itemToGrab) {
        for(int i = 0; i < referencedItems.Length; i++) {
            if(referencedItems[i].itemName == itemToGrab) {
                return referencedItems[i];
            }
        }

        return null;
    }

    public void SortItems() {
        bool itemAfterSpace = true;
        while(itemAfterSpace){
            itemAfterSpace = false;
            for(int i=0; i <itemsHeld.Length - 1; i++) {
                if(itemsHeld[i] == "") {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "") {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd) {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i  = 0; i < itemsHeld.Length; i++) {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd) {
                newItemPosition = i;
                foundSpace = true;
                break;
            }
        }

        if (foundSpace) {
            bool itemExists = false;
            for(int i = 0; i < referencedItems.Length; i++) {
                if(referencedItems[i].itemName == itemToAdd) {
                    itemExists = true;
                    break;
                }
            }

            if (itemExists) {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            } else {
                Debug.LogError(itemToAdd + " Does not exists");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public int RemoveItem(string itemToRemove) {
        int itemPosition = 0;
        bool foundItem = false;

        for(int i = 0; i < itemsHeld.Length; i++) {
            if(itemsHeld[i] == itemToRemove) {
                foundItem = true;
                itemPosition = i;
                break;
            }
        }

        if (foundItem) {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition] <= 0) {
                itemsHeld[itemPosition] = "";
                GameMenu.instance.ShowItems();
                return 0;
            }

            GameMenu.instance.ShowItems();
            return 1;
        } else {
            Debug.LogError(itemToRemove + " is not there");
            return 0;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        InitializeGamemanager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
