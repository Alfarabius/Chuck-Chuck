using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private int HitPoints = 10;
    [SerializeField] private int MaxHitPoints = 10;
    [SerializeField] private GameObject DeathEffect;
    [SerializeField] private UnityEvent OnDestroy;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (gameObject.CompareTag("Ghost"))
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        var audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        OnDestroy.AddListener(audioManager.PlayHit);
    }

    public void TakeDamage(int amount, string dealer)
    {
        HitPoints -= amount;

        Debug.Log(gameObject.name + " take " + amount.ToString() + "<color=red> damage </color>from " + dealer);

        if (HitPoints <= 0)
        {
            Debug.Log(gameObject.name + "<color=blue> Killed by </color>" + dealer);
            HitPoints = 0;
            Die();
        }
        spriteRenderer.enabled = false;
        StartCoroutine(TurnOnSprite(0.15f));
    }

    public void Heal(int amount)
    {
        HitPoints += amount;

        if (HitPoints > MaxHitPoints)
        {
            HitPoints = MaxHitPoints;
        }
    }

    void Die()
    {
        if (DeathEffect != null)
        {
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        OnDestroy.Invoke();
    }

    public void Heal10()
    {
        HitPoints += 20;

        if (HitPoints > MaxHitPoints)
        {
            HitPoints = MaxHitPoints;
        }
    }

    private IEnumerator TurnOnSprite(float time)
    {
        yield return new WaitForSeconds(time);

        spriteRenderer.enabled = true;
    }

    public string GetHitPoints()
    {
        return HitPoints.ToString() + "/" + MaxHitPoints.ToString();
    }
}


