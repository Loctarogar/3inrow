using UnityEngine;

public class MechanisCampaign : MonoBehaviour
{
    public CampaignStage[] stages =
    {
        new CampaignStage
        {
            stageName = "Stage 1",
            cities = new[] {"Ferrum"},
            requiresOrder = false,
        },
        new CampaignStage
        {
            stageName = "Stage 2",
            cities=new[]
            {
                "Brannor", "Varkos", "Caldor", "Thornix", "Orgrim", "Dredan"
            },
            requiresOrder = false,
        },
        new CampaignStage
        {
            stageName = "Stage 3",
            cities = new []
            {
                "Keldran", "Eorvon", "Zorvik", "Nerath", "Vulkor", "Gorath"
            },
            requiresOrder = true
        },
        new CampaignStage
        {
            stageName = "Stage 4",
            cities = new []
            {
                "Kragar", "Tharion"
            },
            requiresOrder = true
        },
        new CampaignStage
        {
            stageName = "Stage 5",
            cities = new []
            {
                "Eldwyn", "Thalorien", "Aranthil"
            },
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 6",
            cities = new []
            {
                "Nimbras", "Velmora"
            },
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 7",
            cities = new []
            {
                "Ilyndor"
            },
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 8",
            cities = new []
            {
                "Mystral", "Caelion", "Lunareth"
            },
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 9",
            cities = new []
            {
                "Sylvaris"
            },
            requiresOrder = false
        },
        new CampaignStage
        {
            stageName = "Stage 10",
            cities = new []
            {
                "Aetheris"
            },
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
