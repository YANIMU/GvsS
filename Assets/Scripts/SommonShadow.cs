using UnityEngine;

public class SommonShadow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isBrightTwinkle;
    public float TwinkleSpeed;
    public float minA;
    public float maxA;
    public float delayTime;
    public float d_Time;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        d_Time = delayTime;
    }
    private void Update()
    {
        spriteRenderer.color = Gradation();
        if (spriteRenderer.color.a <= minA || spriteRenderer.color.a >= maxA)
        {
            d_Time -= Time.deltaTime;
            if (d_Time <= 0f)
            {
                isBrightTwinkle = !isBrightTwinkle;
                d_Time = delayTime;
            }
        }
    }

    Color Gradation()
    {
        return new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Clamp(spriteRenderer.color.a + TwinkleSpeed * Time.deltaTime * (isBrightTwinkle ? 1 : -1), minA, maxA));
    }
}