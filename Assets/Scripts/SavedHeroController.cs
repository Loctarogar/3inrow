using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                currentSlot = i;
                return hero;
            }
        }

        return null;
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
                currentSlot = i;
                return hero;
            }
        }

        return null;
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
        DisplayHero(GetFirstHero());
    }

    public void NextHero()
    {
        DisplayHero(GetNextHero());
    }

    public void PreviousHero()
    {
        DisplayHero(GetPreviousHero());
    }
}
