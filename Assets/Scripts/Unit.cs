using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int health;
    public int max_health;
    public int armor;
    public int max_armor;
    public string Name;
    public Slider hp_slider;
    public Slider armor_slider;
    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
        hp_slider.maxValue = max_health;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            int spillover = 0;
            armor -= damage;
            if (armor < 0)
            {
                spillover = -armor;
                armor = 0;
            }
            health -= spillover;
        }
        else
        {
            health -= damage;
        }
        hp_slider.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        print("Dead");
    }

    public void Heal(int hp)
    {
        health += hp;
        if (health > max_health)
        {
            health = max_health;
        }
        hp_slider.value = health;
    }

    public void Guard(int value)
    {
        armor += value;
        if (armor > max_armor)
        {
            armor = max_armor;
        }
        armor_slider.value = armor;
    }
}
