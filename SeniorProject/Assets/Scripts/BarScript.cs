using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    private float fillAmount;

    [SerializeField] private float lerpSpeed;

    [SerializeField] private UnityEngine.UI.Image content;

    [SerializeField] private TMP_Text valueText;

    [SerializeField] private Color fullHealthColor;

    [SerializeField] private Color lowHealthColor; 

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0,MaxValue,0,1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            // content.fillAmount = fillAmount;
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }

        content.color = Color.Lerp(lowHealthColor, fullHealthColor, fillAmount);
    }

    private float Map(float value, float minHealthValue, float maxHealthValue, float outMin, float outMax)
    {
        // this is the math to calculate the health damage and reflect it to the healthbar 
        return (value - minHealthValue) * (outMax - outMin) / (maxHealthValue - minHealthValue) + outMin;
    }
}
