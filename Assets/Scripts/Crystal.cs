
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Crystal : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }

    private BoardManager boardManager;
    private Animator animator;

    private static Crystal dragSource;
    private static Vector2 dragStartPosition;
    private static bool dragMoved;

    private const float DragThreshold = 20f;

    public void Initialize(int x, int y, BoardManager manager)
    {
        X = x;
        Y = y;

        boardManager = manager;
        animator = GetComponent<Animator>();
    }

    public void PlayRedIdleAnimation(RuntimeAnimatorController controller)
    {
        PlayIdleAnimation(controller, "RedCrystal_Idle");
    }

    public void PlayGreenIdleAnimation(RuntimeAnimatorController controller)
    {
        PlayIdleAnimation(controller, "GreenCrystalAnimation");
    }

    public void PlayYellowIdleAnimation(RuntimeAnimatorController controller)
    {
        PlayIdleAnimation(controller, "YellowCrystal_Idle");
    }

    public void PlayUnicolorIdleAnimation(RuntimeAnimatorController controller)
    {
        PlayIdleAnimation(controller, "UnicolorCrystal_Idle");
    }

    public void PlayBlueIdleAnimation(RuntimeAnimatorController controller)
    {
        PlayIdleAnimation(controller, "BlueCrystal_Idle");
    }

    public void PlayPurpleIdleAnimation(RuntimeAnimatorController controller)
    {
        PlayIdleAnimation(controller, "PurpleCrystal_Idle");
    }

    private void PlayIdleAnimation(
        RuntimeAnimatorController controller,
        string animationName)
    {
        animator = GetComponent<Animator>();

        if (animator == null || controller == null)
        {
            return;
        }

        animator.runtimeAnimatorController = controller;
        animator.enabled = true;
        animator.Play(animationName, 0, 0f);
    }

    private void Update()
    {
        if (GetPointerDown(out Vector2 pointerPosition))
        {
            HandlePointerDown(pointerPosition);
        }

        if (dragSource == this && GetPointerHeld(out pointerPosition))
        {
            if (Vector2.Distance(dragStartPosition, pointerPosition) > DragThreshold)
            {
                dragMoved = true;
            }
        }

        if (dragSource == this && GetPointerUp(out pointerPosition))
        {
            HandlePointerUp(pointerPosition);
        }
    }

    private void HandlePointerDown(Vector2 screenPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit == null || hit.gameObject != gameObject)
        {
            return;
        }

        dragSource = this;
        dragStartPosition = screenPosition;
        dragMoved = false;
    }

    private void HandlePointerUp(Vector2 screenPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        Crystal targetCrystal = null;

        if (hit != null)
        {
            targetCrystal = hit.GetComponent<Crystal>();
        }

        if (dragMoved)
        {
            if (targetCrystal != null && targetCrystal != this)
            {
                if (boardManager != null)
                {
                    boardManager.SwapByDrag(this, targetCrystal);
                }
            }
        }
        else
        {
            if (boardManager != null)
            {
                boardManager.SelectCrystal(this);
            }
        }

        dragSource = null;
        dragMoved = false;
    }

    private bool GetPointerDown(out Vector2 position)
    {
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            position = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            position = Mouse.current.position.ReadValue();
            return true;
        }

        position = Vector2.zero;
        return false;
    }

    private bool GetPointerHeld(out Vector2 position)
    {
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            position = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.isPressed)
        {
            position = Mouse.current.position.ReadValue();
            return true;
        }

        position = Vector2.zero;
        return false;
    }

    private bool GetPointerUp(out Vector2 position)
    {
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            position = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasReleasedThisFrame)
        {
            position = Mouse.current.position.ReadValue();
            return true;
        }

        position = Vector2.zero;
        return false;
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
