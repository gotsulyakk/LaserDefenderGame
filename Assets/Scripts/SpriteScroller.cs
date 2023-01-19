using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;

    Material material;
    Vector2 offset;
    
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        offset = new Vector2(0f, Time.time * scrollSpeed);
        material.mainTextureOffset = offset;
    }
}
