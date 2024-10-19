using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    SpriteRenderer sprite;
    Vector2 direction;
    public Color[] colors;

    public float minSize = 0.1f;
    public float maxSize = 0.3f;

    public float moveSpeed = 0.1f;
    public float sizeSpeed = 1.0f;
    public float colorSpeed = 5.0f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        direction = new Vector2 (Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);

        sprite.color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed);
        //transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);
        transform.localScale = Vector3.one;

        Color color = sprite.color;
        //color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * colorSpeed);
        color.a = 1;
        sprite.color = color;

        if (sprite.color.a <= 0.01f){ Destroy(gameObject); }
    }
}
