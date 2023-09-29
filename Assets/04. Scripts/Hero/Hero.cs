using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public HeroData heroData;
    public string heroName { get; protected set; }
    public int heroLevel { get; protected set; }
    public string heroAbility { get; protected set; }

    public Material heroImage { get; protected set; }


    public SnowManData snowManData { get; protected set; }

    void Start()
    {
        
    }
    public void Setup(HeroData heroData)
    {
        heroName = heroData.heroName;
        heroLevel = heroData.heroLevel;
        heroAbility = heroData.heroAbility;
        heroImage = heroData.heroImage;
        snowManData=heroData.snowManData;

    }
}
