using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public TextMeshProUGUI TextChrono;
    public Image EnergyFilling;
   

    public  static UiManager Instance;
  

    // Start is called before the first frame update
    void Start()
    {
        Instance = Instance ?? this;        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateChrono(float chrono)
    {
       TextChrono.text=  TimeSpan.FromSeconds(chrono).ToString(@"mm\:ss");
    }

    public void UpdateEnergy(float ratio)
    {
        EnergyFilling.fillAmount = ratio;
    }
}
