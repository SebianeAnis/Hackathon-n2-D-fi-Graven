using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    [Header("Shield")]
    [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;

    [Header("Settings")]
    [SerializeField] private bool destroyObject;

    private Character character;
    private Collider2D collider2D;
    private CharacterController controller;
    private SpriteRenderer spriteRenderer;

    private bool shieldBroken;

    public float CurrentHealth { get; set; }
    public float CurrentShield { get; set; }



    private void Awake()
    {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        CurrentHealth = initialHealth;
        CurrentShield = initialShield;
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }

    }




    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        if (!shieldBroken && character != null )
        {
            CurrentShield -= damage;
            UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield);
            if(CurrentShield <= 0)
            {
                shieldBroken = true;
            }
            return;
        }

        CurrentHealth -= damage;
        UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield);
        
        if (CurrentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {

        if (character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            character.enabled = false;
            controller.enabled = false;

        }

        if (destroyObject)
        {
            DestroyObject();
        }
    }

    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;
            character.enabled = true;
            controller.enabled = true;
        }

        gameObject.SetActive(true);
        CurrentHealth = initialHealth;
        CurrentShield = initialShield;
        UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield);

    }

    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }

}
