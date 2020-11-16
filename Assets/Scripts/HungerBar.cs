using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHunger(int hunger){
        slider.maxValue = hunger;
        slider.value = hunger;
    }

    public void SetHunger(int hunger){
        slider.value = hunger;
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
