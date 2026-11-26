using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SavedHeroController : MonoBehaviour
{
    private int currentSlot = -1;
    public Image heroImage;
    public Sprite[] heroSprites;
    public TMP_Text heroNameText;

    public HeroData GetFirstHero()
    {
        for (int i = 0; i < 8; i++)
        {
            HeroData hero = SaveManager.LoadGame(i);

            if (hero != null)
            {
                currentSlot = i;
                return hero;
            }
        }

        currentSlot = -1;
        return null;
    }

    public HeroData GetNextHero()
    {
        if (currentSlot == 7)
            currentSlot = -1;

        for (int i = currentSlot + 1; i < 8; i++)
        {
            HeroData hero = SaveManager.LoadGame(i);

            if (hero != null)
            {
                Debug.Log("current hero is not null");
                currentSlot = i;
                return hero;
            }
        }

        return GetFirstHero();
        
    }

    public HeroData GetPreviousHero()
    {
        if (currentSlot == 0)
            currentSlot = 8;

        for (int i = currentSlot - 1; i >= 0; i--)
        {
            HeroData hero = SaveManager.LoadGame(i);

            if (hero != null)
            {
                Debug.Log("current hero is not null");
                currentSlot = i;
                return hero;
            }
        }

        return GetFirstHero();
        
    }

    public Sprite GetHeroSprite(string heroName)
    {
        switch (heroName.Trim())
        {
            case "Mage":
                return heroSprites[0];

            case "Paladin":
                return heroSprites[1];

            case "Rogue":
                return heroSprites[2];

            case "Druid":
                return heroSprites[3];

            case "Gunner":
                return heroSprites[4];

            case "Exosute Operator":
            case "ExosuitOperator":
                return heroSprites[5];

            case "Inquizitor":
            case "Inquisitor":
                return heroSprites[6];

            case "Mechanic":
                return heroSprites[7];

            default:
                Debug.LogError("Nie znaleziono portretu: " + heroName);
                return null;
        }
    }

    private void DisplayHero(HeroData hero)
    {
        if (hero == null)
        {
            heroImage.sprite = null;
            return;
        }

        heroImage.sprite = GetHeroSprite(hero.heroName);
        heroNameText.text = hero.heroName;
    }

    private void Start()
    {
        HeroData hero = GetFirstHero();

        if (hero == null)
        {
            Debug.Log("Nie znaleziono zapisanych bohaterów.");
        }
        else
        {
            Debug.Log("Wczytano bohatera: " + hero.heroName);
        }

        DisplayHero(hero);
    }

    public void NextHero()
    {
        DisplayHero(GetNextHero());
    }

    public void PreviousHero()
    {
        DisplayHero(GetPreviousHero());
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueGame()
    {
        if(currentSlot < 0 || currentSlot > 7)
        {
            return; 
        }

        HeroData loadHero = SaveManager.LoadGame(currentSlot);

        if(loadHero == null)
        {
            Debug.Log("Nie udało się załadować");

            return;
        }

        loadHero.heroImage = GetHeroSprite(loadHero.heroName);
        HeroData.instance = loadHero;

        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMapScene");
    }
}
