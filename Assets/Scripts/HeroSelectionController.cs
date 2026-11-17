using System.Collections.Generic;
using TMPro;
using Unity.Multiplayer.PlayMode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroSelectionController : MonoBehaviour
{
    public HeroData[] eldoriaHeroes;
    public HeroData[] mechanisHeroes;
    private HeroData[] currentHeroes;
    private int currentHeroIndex = 0;
    public TMP_Text heroExistsMessage;

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
        GameObject.Find("Background").GetComponent<CountryBackground>().SetCountry("Eldoria");
        SetHeroByIndeks(0,eldoriaHeroes);
        foreach (HeroData hero in eldoriaHeroes)
        {
            hero.heroCountry = "Eldoria";
        }

        Debug.Log(currentHeroes[currentHeroIndex].heroName);
    }

    public void SelectMechanis()
    {
        GameObject.Find("Background").GetComponent<CountryBackground>().SetCountry("Mechanis");
        SetHeroByIndeks(0, mechanisHeroes);
        foreach (HeroData hero in mechanisHeroes)
        {
            hero.heroCountry = "Mechanis";
        }

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

        string selectedHeroName = currentHeroes[currentHeroIndex].heroName;

        heroExistsMessage.text = SaveManager.IsHeroCreated(selectedHeroName)
            ? "Ten bohater został już utworzony. Wybierz innego."
            : "";
    }

    public void ConfirmHero()
    {
        if (SaveManager.IsHeroCreated(currentHeroes[currentHeroIndex].heroName))
        {
            heroExistsMessage.text = "Ten bohater został już utworzony. Wybierz innego.";
            return;
        }

        HeroData.instance = new HeroData
        {
            heroName = currentHeroes[currentHeroIndex].heroName,
            heroDescription = currentHeroes[currentHeroIndex].heroDescription,
            heroHealth = currentHeroes[currentHeroIndex].heroHealth,
            heroAttack = currentHeroes[currentHeroIndex].heroAttack,
            heroDamage = currentHeroes[currentHeroIndex].heroDamage,
            heroMagic = currentHeroes[currentHeroIndex].heroMagic,
            heroProtection = currentHeroes[currentHeroIndex].heroProtection,
            heroImage = currentHeroes[currentHeroIndex].heroImage,
            heroCountry = currentHeroes[currentHeroIndex].heroCountry,
            completedBattles = new List<string>()
        };

        StoryManager.ShowFinalImage = true;
        //SceneManager.LoadScene("StoryScene");
        SceneManager.LoadScene("WorldMapScene");
        
    }

    public HeroData GetSelectedHero()
    {
        return currentHeroes[currentHeroIndex];
    }

    public Sprite GetHeroSprite(string heroName)
    {
        foreach (HeroData hero in eldoriaHeroes)
        {
            if (hero.heroName == heroName)
                return hero.heroImage;
        }

        foreach (HeroData hero in mechanisHeroes)
        {
            if (hero.heroName == heroName)
                return hero.heroImage;
        }

        Debug.LogError("Hero sprite not found: " + heroName);
        return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectMechanis();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
