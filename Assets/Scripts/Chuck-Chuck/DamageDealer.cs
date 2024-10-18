using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private Sprite AltSprite;
    [SerializeField] private int maxDamage = 8;
    [SerializeField] private int minDamage = 4;
    [SerializeField] private bool isPhysicBased = true;

    private Destroyable selfDestroyable;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Sprite OrigSprite;

    private void Awake()
    {
        selfDestroyable = GetComponent<Destroyable>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        OrigSprite = spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
             return;

        if(other.gameObject.TryGetComponent<Destroyable>(out Destroyable destroyable))
            DealDamage(destroyable);
    }

    void DealDamage(Destroyable target)
    {
        int damage = maxDamage;
        float speed = Mathf.Abs(rb.velocity.magnitude);

        if (isPhysicBased)
        {
            damage = (int) speed;

            if (damage < minDamage)
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
            Debug.Log(transform.name + speed.ToString() + "<color=green> speed converts to </color>" + damage.ToString() + "<color=red> damage to </color>" + target.name);

            target.TakeDamage(damage, gameObject.name);
            selfDestroyable.TakeDamage(damage, gameObject.name);
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
