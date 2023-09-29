using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroUIManager : MonoBehaviour
{
    [Header("heroInfoUI")]
    public GameObject heroInfoUI;
    public GameObject useButton;
    public GameObject dontUseButton;

    public HeroDeck[] decks;

    public GameObject popUpPanel;
    public Text popUpPanelText;

    public List<Hero> selectedHeros;

    [HideInInspector]
    public Hero currentHero;

    [HideInInspector]
    public HeroDeck currentDeck;

    private void Awake()
    {
        selectedHeros = new List<Hero>();
    }
    public void ClickUse()
    {


        if (selectedHeros.Contains(currentHero))
        {
            popUpPanel.gameObject.SetActive(true);
            popUpPanelText.text = "¿ÃπÃ º±≈√µ» øµøı¿‘¥œ¥Ÿ.";
            return;
        }
        selectedHeros.Add(currentHero);


        for (int i=0;i< decks.Length;i++)
        {

            if (!decks[i].isFull)
            {
                decks[i].storedHero=currentHero;

                Hero hero=Instantiate(currentHero, decks[i].transform.position+Vector3.forward*0.5f, decks[i].transform.rotation*Quaternion.Euler(0,135,0));
                hero.transform.parent = decks[i].transform;
                decks[i].isFull = true;
                return;
            }
            if (i == decks.Length - 1)
            {
                popUpPanel.gameObject.SetActive(true);
                popUpPanelText.text = "µ¶¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ.";
            }
        }
    }

    public void ClickDontUse()
    {
        currentDeck.isFull = false;

        selectedHeros.Remove(currentHero);
        Hero hero=currentDeck.GetComponentInChildren<Hero>();

        Destroy(hero.gameObject);
    }

}
