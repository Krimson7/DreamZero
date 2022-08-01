using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setMaxMana(float max){
        slider.maxValue = max;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMana(float mana){
        slider.value = mana;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
