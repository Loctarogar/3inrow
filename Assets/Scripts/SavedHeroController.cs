using UnityEngine;

public class SavedHeroController : MonoBehaviour
{
    private int currentSlot = -1;

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
}
