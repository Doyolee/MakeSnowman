using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUIText : MonoBehaviour
{
    [Header("HeroInfo")]
    public Text heroNameText;
    public Text heroLevel;
    public RawImage heroImage;
    public Text heroAbility;
    public SnowManData snowManData;

    [Header("SnowManInfo")]
    public Text snowManNameText;
    public Text ATText;
    public Text ASText;
    public Text RangeText;
    public Text AbilityText;
    public Text AbilityInfoText;
    public Image snowManCamera;
    
    HeroUIManager heroUIManager;
    private void Start()
    {
        heroUIManager = GetComponent<HeroUIManager>();
    }
    void Update()
    {
        if (!heroUIManager.currentHero) return; //heroSlot 클릭하기 전까지는 null값이므로 체크\

        heroNameText.text = heroUIManager.currentHero.heroData.heroName;
        heroLevel.text = heroUIManager.currentHero.heroData.heroLevel.ToString();
        heroImage.material = heroUIManager.currentHero.heroData.heroImage;
        heroAbility.text=heroUIManager.currentHero.heroData.heroAbility;
        snowManData = heroUIManager.currentHero.heroData.snowManData;

        snowManNameText.text = snowManData.inGameName;
        ATText.text = snowManData.damage.ToString();
        ASText.text = snowManData.AttackSpeed.ToString();
        RangeText.text = snowManData.range.ToString();
        AbilityInfoText.text=snowManData.abilityExplanation;
        snowManCamera.material = snowManData.snowManCamera;

    }
}
