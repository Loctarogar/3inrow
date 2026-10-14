using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab;

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
                GameObject crystal = Instantiate(
                crystalPrefab,
                new Vector3(
                    (x - (Width - 1) / 2f) * 1.2f,
                    (y - (Height - 1) / 2f) *1.2f
                    ),
                Quaternion.identity
                );

                crystal.transform.localScale = new Vector3(0.15f, 0.15f, 1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
