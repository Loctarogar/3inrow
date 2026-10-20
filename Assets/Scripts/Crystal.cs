using UnityEngine;
using UnityEngine.InputSystem;

public class Crystal : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }  

    private BoardManager boardManager;

    private Animator animator;

    public void Initialize(int x, int y, BoardManager manager)
    {
        X = x;
        Y = y;

        boardManager = manager;
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    public void PlayRedIdleAnimation(RuntimeAnimatorController controller)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play("RedCrystal_Idle", 0, 0f);
    }

    //// Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(worldPosition);

            if (hit != null && hit.gameObject == gameObject)
            {
                //Debug.Log("Kliknięto kryształ");
                if (boardManager!= null)
                {
                    boardManager.SelectCrystal(this);
                }
                else
                {
                    //Debug.LogError("boardManager nie został przypisany");
                }
            }
        }
    }
}
