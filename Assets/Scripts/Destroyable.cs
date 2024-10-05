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
    }

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;

        if (HitPoints <= 0)
        {
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
        HitPoints += 10;

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


