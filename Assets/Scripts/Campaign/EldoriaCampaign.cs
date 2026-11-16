using UnityEngine;

public class EldoriaCampaign : MonoBehaviour
{
    public CampaignStage[] stages =
    {
        new CampaignStage
        {
            stageName = "Stage 1",
            cities = new[] {"Aetheris"},
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 2",
            cities = new[] {"Sylvaris", "Caelion", "Lunareth"},
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 3",
            cities = new[] {"Mystral", "Thalorien", "Eldwyn"},
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 4",
            cities = new[] {"Aranthil", "Nimbras", "Velmora", "Ilyndor"},
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 5",
            cities = new[] {"Tharion", "Kragar"},
            requiresOrder = true
        },
        new CampaignStage
        {
            stageName = "Stage 6",
            cities = new[] {"Eorvon", "Keldran", "Brannor", "Varkos"},
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 7",
            cities = new[] {"Zorvik", "Nerath", "Vulkor", "Gorath"},
            requiresOrder = true
        },
        new CampaignStage
        {
            stageName = "Stage 8",
            cities = new[] {"Caldor", "Thornix", "Orgrim", "Dredan"},
            requiresOrder = true
        },
        new CampaignStage
        {
            stageName = "Stage 8",
            cities = new[] {"Ferrum"},
            requiresOrder = false
        }

    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
