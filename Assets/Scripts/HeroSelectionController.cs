using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Multiplayer.PlayMode;

public class HeroSelectionController : MonoBehaviour
{
    public HeroData[] eldoriaHeroes;
    public HeroData[] mechanisHeroes;
    private HeroData[] currentHeroes;
    private int currentHeroIndex = 0;

    public void NextHero()
    {
        if (currentHeroIndex >= 3)
        {
            currentHeroIndex = 0;
        }
        else
        {
            currentHeroIndex++;
        }

        SetHeroByIndeks(currentHeroIndex, currentHeroes);

        Debug.Log(currentHeroIndex);
    }
    public void PreviousHero()
    {
        if (currentHeroIndex <= 0)
        {
            currentHeroIndex = 3;
        }
        else
        {
            currentHeroIndex--;
        }
        SetHeroByIndeks(currentHeroIndex, currentHeroes);


        Debug.Log(currentHeroIndex);
    }

    public void SelectEldoria()
    {
        SetHeroByIndeks(0,eldoriaHeroes);
        
        Debug.Log(currentHeroes[currentHeroIndex].heroName);
    }

    public void SelectMechanis()
    {
        SetHeroByIndeks(0, mechanisHeroes);

        Debug.Log(currentHeroes[currentHeroIndex].heroName);
    }

    public void SetHeroByIndeks(int heroIndeks, HeroData[] heroes)
    {
        currentHeroes = heroes;
        currentHeroIndex = heroIndeks;

        GameObject.Find("HeroName").GetComponent<TMP_Text>().text = currentHeroes[currentHeroIndex].heroName;
        GameObject.Find("HeroHealth").GetComponent<TMP_Text>().text = (currentHeroes[currentHeroIndex].heroHealth).ToString();
        GameObject.Find("HeroAtack").GetComponent<TMP_Text>().text = (currentHeroes[currentHeroIndex].heroAttack).ToString();
        GameObject.Find("HeroProtection").GetComponent<TMP_Text>().text = (currentHeroes[currentHeroIndex].heroProtection).ToString();
        GameObject.Find("HeroMana").GetComponent<TMP_Text>().text = (currentHeroes[currentHeroIndex].heroMagic).ToString();
        GameObject.Find("HeroDescription").GetComponent<TMP_Text>().text = currentHeroes[currentHeroIndex].heroDescription;
        GameObject.Find("HeroPortrait").GetComponent<Image>().sprite = currentHeroes[currentHeroIndex].heroImage;
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
