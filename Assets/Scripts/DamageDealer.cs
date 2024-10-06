using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private Sprite AltSprite;
    [SerializeField] private int maxDamage = 5;
    [SerializeField] private bool isPhysicBased = true;

    private Destroyable selfDestroyable;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Sprite OrigSprite;

    private void Awake()
    {
        selfDestroyable = GetComponent<Destroyable>();

        if (isPhysicBased)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        OrigSprite = spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            return;

        Destroyable destroyable = other.gameObject.GetComponent<Destroyable>();

        DealDamage(destroyable);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Finish"))
            return;

        Destroyable destroyable = other.GetComponent<Destroyable>();

        DealDamage(destroyable);
    }

    void DealDamage(Destroyable target)
    {
        int damage = maxDamage;

        if (isPhysicBased)
        {
            float speed = Mathf.Abs(rb.velocity.x);

            damage = (int) speed;

            if (damage < 4)
            {
                return;
            }

            if (damage > maxDamage)
            {
                damage = maxDamage;
            }
        }

        if (target != null)
        {
            target.TakeDamage(damage);
            selfDestroyable.TakeDamage(damage);
        }
    }

    public void Select()
    {
        spriteRenderer.sprite = AltSprite;
    }

    public void Unselect()
    {
        spriteRenderer.sprite = OrigSprite;
    }
}
