using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxCake;

public class MPalette : MonoBehaviour
{
    GameObject Image;
    GameObject Cursor;
    byte[,] Color;
    public Sprite sprite;
    
    int rows = 8;
    int columns = 32;

    private void Start()
    {
        DrawPalette();
        DrawCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(GoLeft());
            Debug.Log(UColor.CurrentColor);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(GoRight());
            Debug.Log(UColor.CurrentColor);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(GoDown());
            Debug.Log(UColor.CurrentColor);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(GoUp());
            Debug.Log(UColor.CurrentColor);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            StartCoroutine(GoTo());
        }
    }

    IEnumerator GoTo()
    {
        yield return new WaitForSeconds(0.1f);
        Cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(UColor.x * 16 + 8, UColor.y * -16 - 8);
    }

    IEnumerator GoLeft()
    {
        UColor.x--;
        if (UColor.x < 0)
        {
            UColor.x = rows - 1;
        }

        Cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(UColor.x * 16 + 8, UColor.y * -16 - 8);
        UColor.CurrentColor = Color[UColor.x, UColor.y];
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator GoRight()
    {
        UColor.x++;
        if (UColor.x > rows - 1)
        {
            UColor.x = 0;
        }
        Cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(UColor.x * 16 + 8, UColor.y * -16 -8);
        UColor.CurrentColor = Color[UColor.x, UColor.y];
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator GoDown()
    {
        UColor.y++;
        if (UColor.y > columns - 1)
        {
            UColor.y = 0;
        }
        
        Cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(UColor.x * 16 + 8, UColor.y * -16 - 8);
        UColor.CurrentColor = Color[UColor.x, UColor.y];
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator GoUp()
    {
        UColor.y--;
        if (UColor.y < 0)
        {
            UColor.y = columns - 1;
        }
        Cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(UColor.x * 16 + 8, UColor.y * -16 - 8);
        UColor.CurrentColor = Color[UColor.x, UColor.y];
        yield return new WaitForSeconds(0.5f);
    }



    private void DrawPalette()
    {
        Color = new byte[rows, columns];
        Image = new GameObject("Image");
        Image.transform.parent = gameObject.transform;
        Image.AddComponent<RectTransform>();
        Image.AddComponent<Image>();
        Image.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        Image.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        Image.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128/2, -GetComponent<RectTransform>().rect.height/2);
        Image.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width,
            GetComponent<RectTransform>().rect.height);

        Texture2D texture = new Texture2D(rows, columns);
        for (int index = 0; index < 256; index++)
        {
            int y = index % columns;
            int x = (index - y) / columns;
            texture.SetPixel(x, y, UColor.ByteToColor((byte)index));
            Color[x, columns - y - 1] = (byte)index;
        }
        texture.Apply(false);
        texture.filterMode = FilterMode.Point;

        Image.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
        UColor.CurrentColor = Color[UColor.x, UColor.y];
    }

    private void DrawCursor()
    {
        Cursor = new GameObject("Cursor");
        Cursor.transform.parent = Image.transform;
        Cursor.AddComponent<RectTransform>();
        Cursor.AddComponent<Image>();
        Cursor.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        Cursor.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        Cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(UColor.x * 16 + 8, UColor.y * -16 - 8);
        Cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);

        Cursor.GetComponent<Image>().sprite = sprite;
    }
}
