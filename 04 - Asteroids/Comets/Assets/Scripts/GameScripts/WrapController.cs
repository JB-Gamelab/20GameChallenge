using UnityEngine;
using UnityEngine.UIElements;

public class WrapController : MonoBehaviour
{
    [SerializeField] private float worldWidth;
    [SerializeField] private float worldHeight;
    private float worldTop;
    private float worldBottom;
    private float worldLeft;
    private float worldRight;

    private void Start()
    {
        worldTop = worldHeight / 2;
        worldBottom = worldTop * -1;
        worldRight = worldWidth / 2;
        worldLeft = worldRight * -1;
    }

    public bool CheckPosition(Vector2 position)
    {
        float positionX = position.x;
        float positionY = position.y;
        bool leftArea = false;

        if (positionY > worldTop || positionY < worldBottom || positionX > worldRight || positionX < worldLeft)
        {
            leftArea = true;
        }

        return leftArea;
    }

    public Vector2 WarpPosition(Vector2 position)
    {
        float positionX = position.x;
        float positionY = position.y;
        Vector2 newPosition = new Vector2(positionX, positionY);

        if (positionY > worldTop)
        {
            newPosition = new Vector2(positionX, positionY - worldHeight);
        }
        if (positionY < worldBottom)
        {
            newPosition = new Vector2(positionX, positionY + worldHeight);
        }
        if (positionX > worldRight)
        {
            newPosition = new Vector2(positionX - worldWidth, positionY);
        }
        if (positionX < worldLeft)
        {
            newPosition = new Vector2(positionX + worldWidth, positionY);
        }

        return newPosition;
    }
}
