using UnityEngine;

public class BoardManager : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
