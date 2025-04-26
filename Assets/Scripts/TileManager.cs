using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject mobSommoner;
    public SpriteRenderer spriteRenderer;
    Vector2 size = Vector2.zero;
    private void Start()
    {
        //tile = Resources.Load<Game
        spriteRenderer = GetComponent<SpriteRenderer>();
        size = spriteRenderer.size;
        SetTiles();
    }
    public void SetTiles()
    {
        for (int j = 0; j < size.y / 2; j++)
        {
            GameObject newob;
            for (int i = 0; i < size.x / 2; i++)
            {
                newob = Instantiate(tile);
                newob.transform.parent = this.transform.GetChild(0);
                newob.transform.localPosition = new Vector3(i * 2 - (size.x / 2 - 1), (size.y / 2 - 1) - j * 2, 0f);
                newob.transform.position = new Vector3(newob.transform.position.x, newob.transform.position.y, 0f);
                newob.SetActive(true);
            }
        }
    }
}