using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
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

    public void SaveData() {
        //Scene and player position
        PlayerPrefs.SetString("CURRENT_SCENE", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("CURRENT_GOLD", currentGold);
        PlayerPrefs.SetFloat("PLAYER_POSITION_X", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("PLAYER_POSITION_Y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("PLAYER_POSITION_Z", PlayerController.instance.transform.position.z);

        //save character info
        for (int i = 0; i < playerStats.Length; i++) {
            if (playerStats[i].gameObject.activeInHierarchy) {
                PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_ACTIVE",1);
            } else {
                PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_ACTIVE", 0);
            }

            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_LEVEL", playerStats[i].playerLvl);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_CURRENT_EXP", playerStats[i].currentEXP);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_CURRENT_HP", playerStats[i].currentHP);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_MAX_HP", playerStats[i].maxHP);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_CURRENT_MP", playerStats[i].currentMP);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_MAX_MP", playerStats[i].maxMP);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_STRENGTH", playerStats[i].strength);
            PlayerPrefs.SetInt($"PLAYER_{playerStats[i].characterName}_DEFENSE", playerStats[i].defense);
            PlayerPrefs.SetFloat($"PLAYER_{playerStats[i].characterName}_WEAPON_POWER", playerStats[i].weaponPower);
            PlayerPrefs.SetFloat($"PLAYER_{playerStats[i].characterName}_ARMOR_POWER", playerStats[i].armorPower);
            PlayerPrefs.SetString($"PLAYER_{playerStats[i].characterName}_EQUIPPED_WEAPON", playerStats[i].equippedWeapon);
            PlayerPrefs.SetString($"PLAYER_{playerStats[i].characterName}_EQUIPPED_ARMOR", playerStats[i].equippedArmor);
        }

        //store inventory data
        for (int i = 0; i < itemsHeld.Length; i++) {
            PlayerPrefs.SetString($"ITEM_IN_INVENTORY_{i}", itemsHeld[i]);
            PlayerPrefs.SetInt($"ITEM_AMOUNT_{i}", numberOfItems[i]);
        }
    } 

    public void LoadData() {
        SceneLoader.instance.LoadScene(PlayerPrefs.GetString("CURRENT_SCENE"));
        currentGold = PlayerPrefs.GetInt("CURRENT_GOLD");

        for(int i = 0; i < playerStats.Length; i++) {
            if(PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_ACTIVE") == 1) {
                playerStats[i].gameObject.SetActive(true);
            } else {
                playerStats[i].gameObject.SetActive(false);
            }

            playerStats[i].playerLvl = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_LEVEL");
            playerStats[i].currentEXP = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_CURRENT_EXP");
            playerStats[i].currentHP = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_CURRENT_HP");
            playerStats[i].maxHP = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_MAX_HP");
            playerStats[i].currentMP = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_CURRENT_MP");
            playerStats[i].maxMP = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_MAX_MP");
            playerStats[i].strength = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_STRENGTH");
            playerStats[i].defense = PlayerPrefs.GetInt($"PLAYER_{playerStats[i].characterName}_DEFENSE");
            playerStats[i].weaponPower = PlayerPrefs.GetFloat($"PLAYER_{playerStats[i].characterName}_WEAPON_POWER");
            playerStats[i].armorPower = PlayerPrefs.GetFloat($"PLAYER_{playerStats[i].characterName}_ARMOR_POWER");
            playerStats[i].equippedWeapon = PlayerPrefs.GetString($"PLAYER_{playerStats[i].characterName}_EQUIPPED_WEAPON");
            playerStats[i].equippedArmor = PlayerPrefs.GetString($"PLAYER_{playerStats[i].characterName}_EQUIPPED_ARMOR");
        }

        for(int i = 0; i < itemsHeld.Length; i++) {
            itemsHeld[i] = PlayerPrefs.GetString($"ITEM_IN_INVENTORY_{i}");
            numberOfItems[i] = PlayerPrefs.GetInt($"ITEM_AMOUNT_{i}");
        }
        
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("PLAYER_POSITION_X"), PlayerPrefs.GetFloat("PLAYER_POSITION_Y"), PlayerPrefs.GetFloat("PLAYER_POSITION_Z"));
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGamemanager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadData();
        }
    }
}
