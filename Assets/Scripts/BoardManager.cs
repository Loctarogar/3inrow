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

    private Crystal selectedCrystal;

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
                new Vector3(0.10f, 0.10f, 1f);

            SpriteRenderer renderer =
                crystal.GetComponent<SpriteRenderer>();

            renderer.sprite = GetCrystalSprite(type);

            Crystal crystalComponent = crystal.GetComponent<Crystal>();
            if (crystalComponent == null) 
            {
                    Debug.LogError($"{crystal.name}");
                    continue;
            }
            crystalComponent.Initialize(x, y, this);
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

            case CrystalType.Universal: 
                return universalCrystal;

            default:
                return redCrystal;
        }
    }

    public void SelectCrystal(Crystal crystal) 
    {
        if (selectedCrystal == null)
        {
            selectedCrystal = crystal;
            Debug.Log($"pierwszy krysztal");

            return;
        }

        if (selectedCrystal == crystal)
        {
            selectedCrystal = null;
            Debug.Log("Anulowano wybór");
            return;
        }

        int distanceX = Mathf.Abs(selectedCrystal.X - crystal.X);
        int distanceY = Mathf.Abs(selectedCrystal.Y - crystal.Y);

        if(distanceX + distanceY == 1)
        {
            SwapCrystals(selectedCrystal, crystal);
            Debug.Log($"Drugi krysztal");
            selectedCrystal = null;
        }
        else 
        {
            selectedCrystal = null;
        }
    }

    private void SwapCrystals(Crystal first, Crystal second)
    {
        int firstX = first.X;
        int firstY = first.Y;   

        int secondX = second.X;
        int secondY = second.Y;

        CrystalType temp = board[firstX, firstY];

        board[firstX, firstY] = board[secondX, secondY];
        board[secondX, secondY] = temp;

        Vector3 firstPosition = first.transform.position;
        first.transform.position = second.transform.position;
        second.transform.position = firstPosition;

        first.Initialize(secondX, secondY, this);
        second.Initialize(firstX, firstY, this);

        Debug.Log("Kryształy zamineniły się miejscami");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
