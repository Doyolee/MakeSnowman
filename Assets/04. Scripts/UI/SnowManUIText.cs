using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowManUIText : MonoBehaviour
{
    SnowManUI ui;
    SnowMan snowMan;

    [Header("Texts")]
    public Text ATText;
    public Text ASText;
    public Text RangeText;
    public Text CRIText;
    public Text nameText;
    public Text abilityText;

    void Start()
    {
        ui=GetComponentInParent<SnowManUI>();

    }

    // Update is called once per frame
    void Update()
    {
        snowMan = ui.snowMan;

        ATText.text = snowMan.attackDamage.ToString();
        ASText.text = snowMan.attackSpeed.ToString();
        RangeText.text = snowMan.range.ToString();
        CRIText.text = snowMan.critical.ToString()+"%";
        nameText.text = snowMan.inGameName;
        abilityText.text = snowMan.ability;
    }
}
