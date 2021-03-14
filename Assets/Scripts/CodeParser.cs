using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CodeParser
{
    static Dictionary<string, Func<DrawingQuery, string[], DrawingQuery>> commands = new Dictionary<string, Func<DrawingQuery, string[], DrawingQuery>>()
    {
        {"fill", Fill},
        {"hline", HLine},
        {"vline", VLine}
    };

    static Dictionary<string, Color> colors = new Dictionary<string, Color>()
    {
        {"blue", Color.blue},
        {"red", Color.red},
        {"green", Color.green},
        {"black", Color.black},
        {"white", Color.white}
    };

    public static Texture2D Parse(string code)
    {
        DrawingQuery drawingQuery = DrawingQuery.New();

        List<string> lines = code
            .Split('\n')
            .Where(s => !string.IsNullOrWhiteSpace(s) && !s.StartsWith("#"))
            .ToList();

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            string command = words[0];
            string[] args = words.Skip(1).ToArray();

            drawingQuery = commands[command](drawingQuery, args);
        }

        return drawingQuery.Apply();
    }

    static DrawingQuery Fill(DrawingQuery query, string[] args)
    {
        if (args.Count() > 1)
            throw new ArgumentException("Command [FILL]: Too many arguments.");
        
        if (args.Count() == 0)
            throw new ArgumentException("Command [FILL]: Too few arguments.");

        string color = args[0];

        if (!colors.ContainsKey(color))
            throw new ArgumentException("Command [FILL]: Color is not valid.");

        query.Fill(colors[color]);

        return query;
    }

    static DrawingQuery HLine(DrawingQuery query, string[] args)
    {
        if (args.Count() > 2)
            throw new ArgumentException("Command [HLINE]: Too many arguments.");
        
        if (args.Count() < 2)
            throw new ArgumentException("Command [HLINE]: Too few arguments.");

        string color = args[0];

        if (!colors.ContainsKey(color))
            throw new ArgumentException("Command [HLINE]: Color is not valid.");

        string positionStr = args[1];
        int position;

        if (!int.TryParse(positionStr, out position))
            throw new ArgumentException("Command [HLINE]: Position is not valid.");

        query.HorizontalLine(colors[color], position);

        return query;
    }

    static DrawingQuery VLine(DrawingQuery query, string[] args)
    {
        if (args.Count() > 2)
            throw new ArgumentException("Command [VLINE]: Too many arguments.");
        
        if (args.Count() < 2)
            throw new ArgumentException("Command [VLINE]: Too few arguments.");

        string color = args[0];

        if (!colors.ContainsKey(color))
            throw new ArgumentException("Command [VLINE]: Color is not valid.");

        string positionStr = args[1];
        int position;

        if (!int.TryParse(positionStr, out position))
            throw new ArgumentException("Command [VLINE]: Position is not valid.");

        query.VerticalLine(colors[color], position);

        return query;
    }
}
