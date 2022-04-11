using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CursorType
{
    None,
    Interactable,
    Doorway,
    HeldItem,
    Cursor
}

[RequireComponent(typeof(Image))]
public class Cursor : MonoBehaviour
{
    private Image _image;
    private Player _player;
    private CursorType _cursorType = CursorType.None;

    public Sprite InteractableSprite;
    public Sprite CursorSprite;
    public Sprite DoorwaySprite;
    public Sprite NoCursorSprite;

    public LayerMask InteractableLayerMask = new LayerMask();
    public LayerMask UILayerMask = new LayerMask();

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Hides the cursor when the mouse is in frame
        if (!Camera.current) return;
        var mouseLocation = Camera.current.ScreenToViewportPoint(Input.mousePosition);
        var outOfFrame = Mathf.Min(mouseLocation.x, mouseLocation.y) < 0 ||
                         Mathf.Max(mouseLocation.x, mouseLocation.y) > 1;
        UnityEngine.Cursor.visible = outOfFrame;

        Sprite heldSprite = _player.TryGetHeldSprite();
        if (heldSprite)
        {
            _image.sprite = heldSprite;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInteract, 38f,InteractableLayerMask))
            {
                Door door = hitInteract.transform.gameObject.GetComponent<Door>();
                if (door)
                {
                    if (door.Open) _image.sprite = DoorwaySprite;
                    else _image.sprite = InteractableSprite;
                }
                else
                {
                    _image.sprite = InteractableSprite;
                }
            }
            else if (Physics.Raycast(ray, out RaycastHit hitUI, 37f, UILayerMask))
            {
                _image.sprite = CursorSprite;
            }
            else
            {
                _image.sprite = NoCursorSprite;
            }
        }
    }
}
