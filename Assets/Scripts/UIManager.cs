using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public SimpleHealthBar manaBar;

    public int maxHealth;
    public int maxMana;
    private int currentHealth;
    private int currentMana;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

        healthBar.UpdateBar(currentHealth, maxHealth);
        manaBar.UpdateBar(currentMana, maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(int currentHealth)
    {
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void updateMana(int currentMana)
    {
        manaBar.UpdateBar(currentMana, maxMana);
    }
}
