using UnityEngine;
using TMPro;

public class HeroSelectionController : MonoBehaviour
{
    public HeroData[] eldoriaHeroes;
    public HeroData[] mechanisHeroes;
    private HeroData[] currentHeroes;
    private int currentHeroIndex = 0;
    public void PreviousHero()
    {
        Debug.Log("Previous Hero");
    }

    public void NextHero()
    {
        Debug.Log("Next Hero");
    }

    public void SelectEldoria()
    {
        currentHeroes = eldoriaHeroes;
        currentHeroIndex = 0;
        Debug.Log(currentHeroes[currentHeroIndex].heroName);
    }

    public void SelectMechanis()
    {
        currentHeroes = mechanisHeroes;
        currentHeroIndex = 0;
        Debug.Log(currentHeroes[currentHeroIndex].heroName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
