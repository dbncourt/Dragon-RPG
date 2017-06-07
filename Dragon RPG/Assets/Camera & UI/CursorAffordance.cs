using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
    [SerializeField]
    private Texture2D walkCursor;

    [SerializeField]
    private Texture2D targetCursor;

    [SerializeField]
    private Texture2D unknownCursor;

    [SerializeField]
    private Vector2 cursorHotspot;

    private CameraRaycaster cameraRaycaster;

    public CursorAffordance()
    {
        walkCursor = null;
        targetCursor = null;
        unknownCursor = null;
        cursorHotspot = new Vector2(96, 96);
    }
    
    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                break;
        }
    }
}