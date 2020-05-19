using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField] public GameObject interactBox;
    [SerializeField] public TextMeshProUGUI interactText;
    [SerializeField] public string interactDialog;

    //Methods
    public void OpenShop() {
        if (Input.GetKeyDown(KeyCode.F) && !Shop.instance.shopMenu.gameObject.activeInHierarchy) {
            Shop.instance.OpenShop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenShop();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            interactText.text = interactDialog;
            interactBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            interactBox.SetActive(false);
            Shop.instance.CloseShop();
        }
    }
}
