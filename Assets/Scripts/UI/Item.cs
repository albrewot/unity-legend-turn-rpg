using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    //Serialized fields
    [Header("Item Type")]
    [SerializeField] public bool isItem;
    [SerializeField] public bool isWeapon;
    [SerializeField] public bool isArmour;

    [Header("Item Details")]
    [SerializeField] public string itemName;
    [SerializeField] public string description;
    [SerializeField] public int value;
    [SerializeField] public Sprite itemSprite;

    [Header("Item Effects")]
    [SerializeField] public int amountToChange;
    [SerializeField] public bool affectHP, affectMP, affectSTR;

    [Header("Weapon/Armour Details")]
    [SerializeField] public int weaponStrength, armorStrength;

    //Methods
    public void Use(int characterToUseOn) {
        Debug.Log("Char to use on: " + characterToUseOn);
        Debug.Log(itemName);
        CharacterStats selectedCharacter = GameManager.instance.playerStats[characterToUseOn];

        if (isItem) {
            if (affectHP) {
                selectedCharacter.currentHP += amountToChange;
                if (selectedCharacter.currentHP > selectedCharacter.maxHP) {
                    selectedCharacter.currentHP = selectedCharacter.maxHP;
                }
            }
            if (affectMP) {
                selectedCharacter.currentMP += amountToChange;
                if (selectedCharacter.currentMP > selectedCharacter.maxMP) {
                    selectedCharacter.currentMP = selectedCharacter.maxMP;
                }
            }
            if (affectSTR) {
                selectedCharacter.strength += amountToChange;
            }
        }

        if (isWeapon) {

            if (selectedCharacter.equippedWeapon != "") {
                GameManager.instance.AddItem(selectedCharacter.equippedWeapon);
            }

            selectedCharacter.equippedWeapon = itemName;
            selectedCharacter.weaponPower = weaponStrength;
        }

        if (isArmour) {
            if (selectedCharacter.equippedArmor != "") {

                GameManager.instance.AddItem(selectedCharacter.equippedArmor);
            }

            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPower = armorStrength;
        }

        Debug.Log("Remove Item");
        GameManager.instance.RemoveItem(itemName);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
