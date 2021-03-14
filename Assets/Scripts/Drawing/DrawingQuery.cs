using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingQuery
{
    Texture2D texture;

    public static DrawingQuery New()
    {
        DrawingQuery query = new DrawingQuery()
        {
            texture = new Texture2D(16, 16)
        };

        query.texture.filterMode = FilterMode.Point;

        return query;
    }

    public DrawingQuery Fill(Color color)
    {
        Color[] resetPixels = texture.GetPixels();

        for (int i = 0; i < resetPixels.Length; i++)
        {
            resetPixels[i] = color;
        }

        texture.SetPixels(resetPixels);

        return this;
    }

    public DrawingQuery HorizontalLine(Color color, int height)
    {
        if (height >= texture.height)
            throw new ArgumentException("Command [HLINE]: Position is too high.");

        if (height < 0)
            throw new ArgumentException("Command [HLINE]: Position is too low.");

        Color32[] pixels = texture.GetPixels32();
        Color32 color32 = color;
        
        for (int i = height * texture.width; i < ((height+1) * texture.width); i++)
        {
            pixels[i] = color32;
        }

        texture.SetPixels32(pixels);

        return this;
    }

    public DrawingQuery VerticalLine(Color color, int width)
    {
        if (width >= texture.width)
            throw new ArgumentException("Command [VLINE]: Position is too far right.");

        if (width < 0)
            throw new ArgumentException("Command [VLINE]: Position is too far left.");

        Color32[] pixels = texture.GetPixels32();
        Color32 color32 = color;
        
        for (int i = width; i < width + (texture.width * texture.height); i += texture.width)
        {
            pixels[i] = color32;
        }

        texture.SetPixels32(pixels);

        return this;
    }

    public Texture2D Apply()
    {
        texture.Apply();

        return texture;
    }
}
