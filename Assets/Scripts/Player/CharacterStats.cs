using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //Attributes
    [SerializeField] public string characterName;
    [SerializeField] public int playerLvl;
    [SerializeField] public int currentEXP;

    [SerializeField] public int currentHP;
    [SerializeField] public int maxHP = 300;
    [SerializeField] public int currentMP;
    [SerializeField] public int maxMP = 30;
    [SerializeField] public int strength;
    [SerializeField] public int intelligence;
    [SerializeField] public int defense;
    [SerializeField] public float weaponPower;
    [SerializeField] public float armorPower;

    [SerializeField] public string equippedWeapon;
    [SerializeField] public string equippedArmor;

    [SerializeField] public Sprite characterImage;

    [SerializeField] public int maxLvl = 100;
    [SerializeField] public int baseExp = 1000;
    [SerializeField] public int[] expToNextLvl;
    [SerializeField] public float lvlUpMultiplier = 1.05f;
    [SerializeField] public float hpMultiplier = 1.1f;
    [SerializeField] public float mpMultiplier = 1.08f;
    [SerializeField] public int skillPoints;



    //Method
    public void InitializeLevelTable() {
        expToNextLvl = new int[maxLvl];
        expToNextLvl[1] = baseExp;
        for (int item = 2; item < expToNextLvl.Length; item++) {
            expToNextLvl[item] = Mathf.FloorToInt(expToNextLvl[item - 1] * lvlUpMultiplier);
        }
    }

    public void AddExp(int expToAdd) {
        currentEXP += expToAdd;
        if(playerLvl < maxLvl) {
            if(currentEXP >= expToNextLvl[playerLvl]) {
                currentEXP -= expToNextLvl[playerLvl];
                playerLvl++;
                //IncreaseHP
                maxHP = Mathf.FloorToInt(maxHP * hpMultiplier);
                currentHP = maxHP;
                maxMP = Mathf.FloorToInt(maxMP * mpMultiplier);
                currentMP = maxMP;
            }

            if(playerLvl%2 == 0) {
                skillPoints += 2;
            }
        } else {
            currentEXP = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevelTable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            AddExp(1001);
        }
    }
}
