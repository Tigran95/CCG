using UnityEngine;
public class DropPanel : MonoBehaviour
{
   
    [SerializeField] RectTransform _rt;
    private Vector3[] _wCorners;

    public static DropPanel Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _wCorners = new Vector3[4];
        _rt.GetWorldCorners(_wCorners);
    }

    public bool IsCardInRT(Vector2 point)
    {
        if (point.x >= _wCorners[0].x &&
            point.x <= _wCorners[2].x &&
            point.y >= _wCorners[0].y &&
            point.y <= _wCorners[2].y)
        {
            Debug.Log("InPlane");
            return true;
        }
        return false;
    }

    
}
