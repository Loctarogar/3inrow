using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private Sprite redCrystal;
    [SerializeField] private Sprite blueCrystal;
    [SerializeField] private Sprite greenCrystal;
    [SerializeField] private Sprite yellowCrystal;
    [SerializeField] private Sprite purpleCrystal;
    [SerializeField] private Sprite universalCrystal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public const int Width = 7;
    public const int Height = 7;

    public enum CrystalType
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
        Universal
    }

    private CrystalType[,] board = new CrystalType[Width, Height];

    void Start()
    {
        Debug.Log($"3inrow: plansza {Width}x{Height} gotowa");
        Debug.Log($"Liczba pól na planszy {board.Length}");
        CreateBoard();
    }

    private void CreateBoard()
{
    for (int x = 0; x < Width; x++)
    {
        for (int y = 0; y < Height; y++)
        {
            CrystalType type = (CrystalType)Random.Range(0, 6);

            board[x, y] = type;

            GameObject crystal = Instantiate(
                crystalPrefab,
                new Vector3(
                    (x - (Width - 1) / 2f) * 1.2f,
                    (y - (Height - 1) / 2f) * 1.2f,
                    0
                ),
                Quaternion.identity
            );

            crystal.transform.localScale =
                new Vector3(0.15f, 0.15f, 1f);

            SpriteRenderer renderer =
                crystal.GetComponent<SpriteRenderer>();

            renderer.sprite = GetCrystalSprite(type);
        }
    }
}

    private Sprite GetCrystalSprite(CrystalType type)
    {
        switch (type)
        {
            case CrystalType.Red:
                return redCrystal;

            case CrystalType.Blue:
                return blueCrystal;

            case CrystalType.Green:
                return greenCrystal;

            case CrystalType.Yellow:
                return yellowCrystal;

            case CrystalType.Purple:
                return purpleCrystal;

            case CrystalType: 
                return purpleCrystal;

            default:
                return redCrystal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
