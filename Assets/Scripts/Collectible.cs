using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    [SerializeField] UnityEvent OnCollected;

    private void Awake()
    {
        GameObject GameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        if(gameObject.CompareTag("Coin"))
            OnCollected.AddListener(GameManager.GetComponent<AudioManager>().PlayCoin);
        else if (gameObject.CompareTag("Potion"))
        {
            OnCollected.AddListener(GameManager.GetComponent<AudioManager>().PlayHeal);
            OnCollected.AddListener(Player.GetComponent<Destroyable>().Heal10);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
