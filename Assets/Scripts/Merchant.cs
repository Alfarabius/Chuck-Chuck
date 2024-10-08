using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    [SerializeField] int price = 20;
    [SerializeField] int addPrice = 30;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject GoodPrefab;
    [SerializeField] GameObject Text;
    UIManager uIManager;

    void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;

        Text.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;

        Text.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && uIManager.SpendCoins(price))
        {
            Instantiate(GoodPrefab, SpawnPoint.position, Quaternion.identity);
            price += addPrice;
            Text.GetComponentInChildren<TextMeshProUGUI>().text = "Crystal - " + price.ToString() + " coins \npress F to buy";
        }
    }
}
