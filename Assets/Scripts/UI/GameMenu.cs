using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour {
    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject[] windows;
    [SerializeField] public GameObject[] statusBtn;

    //Main Menu Elements
    [SerializeField] public TextMeshProUGUI[] nameText, hpText, mpText, expText, lvlText;
    [SerializeField] public Image[] charImage;
    [SerializeField] public Slider[] expSlider;
    [SerializeField] public GameObject[] charStatHolder;

    //Status window Elements
    [SerializeField] TextMeshProUGUI statusName, statusLvl, statusSP, statusHP, statusMP, statusStrength, statusIntelligence, statusDefense, statusEquipWpn, statusWpnPwer, statusEquipArmor, statusArmrPwer, statusExpToNext;
    [SerializeField] Image statusImage;

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
    }

    public void ToggleWindow(int windowIndex) {
        for(int i = 0; i < windows.Length; i++) {
            if(i == windowIndex) {
                UpdateStats();
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else {
                windows[i].SetActive(false);
            }
        }
    }

    public void OpenStatus() {
        UpdateStats();
        StatusChar(0);
        for(int i = 0; i < statusBtn.Length; i++) {
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
        statusEquipWpn.text = playerStats[selected].equippedWeapon;
        statusWpnPwer.text = playerStats[selected].weaponPower.ToString();
        statusEquipArmor.text = playerStats[selected].equippedArmor;
        statusArmrPwer.text = playerStats[selected].armorPower.ToString();
        statusExpToNext.text = playerStats[selected].currentEXP + "/" + playerStats[selected].expToNextLvl[playerStats[selected].playerLvl];
        statusImage.sprite = playerStats[selected].characterImage;
    }

    public void CloseMenu() {
        for(int i = 0; i < windows.Length; i++) {
            windows[i].SetActive(false);
        }

        Time.timeScale = 1;
        PlayerController.instance.currentState = PlayerState.walk;
        menu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        OpenMenu();
    }
}
