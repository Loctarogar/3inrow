using UnityEngine;
using System.Collections.Generic;

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
        Universal,
        Empty
    }

    private CrystalType[,] board = new CrystalType[Width, Height];
    private Crystal[,] crystals = new Crystal[Width, Height];
    private bool[,] matched = new bool[Width, Height];
    private HashSet<Vector2Int> matchedPositions = new HashSet<Vector2Int>();

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
            crystals[x, y] = crystalComponent;
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

        // Zamiana typów kryształów na planszy.
        CrystalType tempType = board[firstX, firstY];
        board[firstX, firstY] = board[secondX, secondY];
        board[secondX, secondY] = tempType;

        // Zamiana referencji do obiektów kryształów.
        Crystal tempCrystal = crystals[firstX, firstY];
        crystals[firstX, firstY] = crystals[secondX, secondY];
        crystals[secondX, secondY] = tempCrystal;

        // Zamiana pozycji graficznych.
        Vector3 firstPosition = first.transform.position;
        first.transform.position = second.transform.position;
        second.transform.position = firstPosition;

        // Aktualizacja współrzędnych kryształów.
        first.Initialize(secondX, secondY, this);
        second.Initialize(firstX, firstY, this);

        Debug.Log("Kryształy zamieniły się miejscami");

        // Sprawdzamy pierwszą klikniętą figurę w nowej pozycji.
        if (!FindAllMatchesAt(secondX, secondY))
        {
            // Brak dopasowania: cofamy zamianę.
            tempType = board[firstX, firstY];
            board[firstX, firstY] = board[secondX, secondY];
            board[secondX, secondY] = tempType;

            tempCrystal = crystals[firstX, firstY];
            crystals[firstX, firstY] = crystals[secondX, secondY];
            crystals[secondX, secondY] = tempCrystal;

            firstPosition = first.transform.position;
            first.transform.position = second.transform.position;
            second.transform.position = firstPosition;

            first.Initialize(firstX, firstY, this);
            second.Initialize(secondX, secondY, this);

            matchedPositions.Clear();

            Debug.Log("Brak dopasowania. Cofnięto zamianę.");
            return;
        }

        // Szukamy połączonych dopasowań, w tym T i L.
        ExpandMatches();

        // Usuwamy zapisane kryształy.
        foreach (Vector2Int position in matchedPositions)
        {
            Crystal crystal = crystals[position.x, position.y];

            if (crystal != null)
            {
                Destroy(crystal.gameObject);
            }

            crystals[position.x, position.y] = null;
            board[position.x, position.y] = CrystalType.Empty;

            Debug.Log(
                $"Usunięto kryształ: X={position.x}, Y={position.y}"
            );
        }

        matchedPositions.Clear();
    }


    private void FindMatchesHorizontal()
    {
        for (int y = 0; y < Height; y++)
        {
            for(int x = 0; x < Width - 2; x++)
            {
                if (board[x, y] != CrystalType.Empty &&
                    board[x, y] != CrystalType.Universal &&
                    board[x, y] == board[x + 1, y] &&
                    board[x, y] == board[x + 2, y])
                {
                    matched[x, y] = true;
                    matched[x + 1, y] = true;
                    matched[x + 2, y] = true;
                    Debug.Log(
                        $"Dopasowanie poziome od X={x}, Y={y}: " +
                        $"{board[x, y]}"
                        );
                }
            }
        }
    }

    private void FindMatchesVertical()
    {
        for(int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height - 2; y++)
            {
                if (board[x, y] != CrystalType.Empty &&
                    board[x, y] != CrystalType.Universal &&
                    board[x, y] == board[x, y + 1] &&
                    board[x, y] == board[x, y + 2])
                {
                    matched[x, y] = true;
                    matched[x, y + 1] = true;
                    matched[x, y + 2] = true;
                    Debug.Log(
                        $"Dopasowanie pionowe od X={x}, Y={y}: " +
                        $"{board[x, y]}"
                    );
                }
            }
        }
    }

    private void ResetMatches()
    {
        for(int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                matched[x, y] = false;
            }
        }
    }

    private void RemoveMatches()
    {
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height;y++)
            {
                if (matched[x, y])
                {
                    Debug.Log(
                        $"Usuwanie kryształu: X={x}, Y={y}, typ={board[x, y]}"
);
                    Destroy(crystals[x, y].gameObject);

                    crystals[x, y] = null;
                    board[x, y] = CrystalType.Empty;
                    matched[x, y] = false;
                }
            }
        }
    }

    private bool HasMatchAt(int x, int y)
    {
        CrystalType type = board[x, y];

        if (type == CrystalType.Empty ||
            type == CrystalType.Universal)
        {
            return false;
        }

        int horizontalCount = 1;

        for (int i = x - 1; i >= 0 && board[i, y] == type; i--)
        {
            horizontalCount++;
        }

        for (int i = x + 1; i < Width && board[i, y] == type; i++)
        {
            horizontalCount++;
        }

        if (horizontalCount >= 3)
        {
            return true;
        }

        int verticalCount = 1;

        for (int i = y - 1; i >= 0 && board[x, i] == type; i--)
        {
            verticalCount++;
        }

        for (int i = y + 1; i < Height && board[x, i] == type; i++)
        {
            verticalCount++;
        }

        return verticalCount >= 3;
    }

    private void ResetMatchedPositions()
    {
        matchedPositions.Clear();
    }

    private bool FindMatchesAt(int x, int y, bool horizontal)
    {
        CrystalType type = board[x, y];

        if (type == CrystalType.Empty ||
            type == CrystalType.Universal)
        {
            return false;
        }

        int count = 1;

        int limit = horizontal ? Width : Height;
        int start = horizontal ? x : y;

        for (int i = start - 1; i >= 0; i--)
        {
            int checkX = horizontal ? i : x;
            int checkY = horizontal ? y : i;

            if (board[checkX, checkY] != type)
                break;

            count++;
        }

        for (int i = start + 1; i < limit; i++)
        {
            int checkX = horizontal ? i : x;
            int checkY = horizontal ? y : i;

            if (board[checkX, checkY] != type)
                break;

            count++;
        }

        if (count < 3)
            return false;

        int min = start;
        int max = start;

        while (min > 0)
        {
            int checkX = horizontal ? min - 1 : x;
            int checkY = horizontal ? y : min - 1;

            if (board[checkX, checkY] != type)
                break;

            min--;
        }

        while (max < limit - 1)
        {
            int checkX = horizontal ? max + 1 : x;
            int checkY = horizontal ? y : max + 1;

            if (board[checkX, checkY] != type)
                break;

            max++;
        }

        for (int i = min; i <= max; i++)
        {
            int matchX = horizontal ? i : x;
            int matchY = horizontal ? y : i;

            matchedPositions.Add(new Vector2Int(matchX, matchY));
        }

        return true;
    }

    private bool FindAllMatchesAt(int x, int y)
    {
        matchedPositions.Clear();

        bool horizontalMatch = FindMatchesAt(x, y, true);
        bool verticalMatch = FindMatchesAt(x, y, false);

        return horizontalMatch || verticalMatch;
    }


    private void ExpandMatches()
    {
        HashSet<Vector2Int> checkedPositions = new HashSet<Vector2Int>();
        Queue<Vector2Int> positionsToCheck = new Queue<Vector2Int>();

        foreach (Vector2Int position in matchedPositions)
        {
            positionsToCheck.Enqueue(position);
        }

        while (positionsToCheck.Count > 0)
        {
            Vector2Int position = positionsToCheck.Dequeue();

            if (!checkedPositions.Add(position))
            {
                continue;
            }

            int previousCount = matchedPositions.Count;

            FindMatchesAt(position.x, position.y, true);
            FindMatchesAt(position.x, position.y, false);

            if (matchedPositions.Count > previousCount)
            {
                foreach (Vector2Int newPosition in matchedPositions)
                {
                    if (!checkedPositions.Contains(newPosition))
                    {
                        positionsToCheck.Enqueue(newPosition);
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
