using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUIButton : MonoBehaviour
{
    public HeroData heroData;
    Image heroImage;
    public Text levelText;
    void Start()
    {
        //heroImage=heroData.heroImage;
        GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
