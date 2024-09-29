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

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;

        if (HitPoints <= 0)
        {
            Die();
        }
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
}


