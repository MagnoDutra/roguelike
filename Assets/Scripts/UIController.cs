using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{healthSlider.value} / {healthSlider.maxValue}";
    }
}
