using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

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
    void Start()
    {

    }

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

    public void PlayGreenIdleAnimation(RuntimeAnimatorController controller)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play("GreenCrystalAnimation", 0, 0f);
    }

    public void PlayYellowIdleAnimation(RuntimeAnimatorController controller)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play("YellowCrystal_Idle", 0, 0f);
    }

    public void PlayUnicolorIdleAnimation(RuntimeAnimatorController controller)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play("UnicolorCrystal_Idle", 0, 0f);
    }

    public void PlayBlueIdleAnimation(RuntimeAnimatorController controller)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play("BlueCrystal_Idle", 0, 0f);
    }

    public void PlayPurpleIdleAnimation(RuntimeAnimatorController controller)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play("PurpleCrystal_Idle", 0, 0f);
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
                Debug.Log($"Kliknięto kryształ: X={X}, Y={Y}, obiekt={gameObject.name}");
                //Debug.Log("Kliknięto kryształ");
                if (boardManager!= null)
                {
                    boardManager.SelectCrystal(this);
                }
                else
                {
                    Debug.LogError("boardManager nie został przypisany");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Collider2D crystalCollider = GetComponent<Collider2D>();

        if (crystalCollider == null)
        {
            return;
        }

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(
            transform.position,
            new Vector3(0.2f, 0.2f, 0f)
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.05f);
    }
}
