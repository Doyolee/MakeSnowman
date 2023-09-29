using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSlot : MonoBehaviour
{
    HeroUIManager heroUIManager;

    public Hero hero;
    public HeroData heroData;

    void Start()
    {
        heroUIManager=FindObjectOfType<HeroUIManager>();
    }


    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        heroUIManager.currentHero = hero;

        heroUIManager.heroInfoUI.SetActive(true);
        heroUIManager.useButton.SetActive(true);
        heroUIManager.dontUseButton.SetActive(false);

    }
}
