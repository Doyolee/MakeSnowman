using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDeck : MonoBehaviour
{
    public bool isFull=false;

    HeroUIManager heroUIManager;

    [HideInInspector]
    public Hero storedHero;
    void Start()
    {
        heroUIManager = FindObjectOfType<HeroUIManager>();
    }

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (isFull)
        {
            heroUIManager.currentDeck = this;
            heroUIManager.currentHero = storedHero;
            heroUIManager.heroInfoUI.SetActive(true);
            heroUIManager.useButton.SetActive(false);
            heroUIManager.dontUseButton.SetActive(true);
        }

    }
}
