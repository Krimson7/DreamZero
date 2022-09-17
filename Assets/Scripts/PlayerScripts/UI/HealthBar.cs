using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, I_HpListener
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public PlayerStats ps;

    public void setMaxHealth(float max){
        slider.maxValue = max;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setHealth(float health){
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void OnEnable()
    { 
        ps.AddHpListener(this); 
        setMaxHealth(ps.getMaxHp());
        setHealth(ps.getHp());
    }

    public void OnDisable()
    { 
        ps.RemoveHpListener(this); 
    }

    public void OnHpChanged()
    {
        setHealth(ps.getHp());
    }
}
