using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

[Serializable]
public struct LocalizedText
{
    public string french;
    public string english;
}

public class Localization
{
    private Dictionary<string, LocalizedText> localization;

    private readonly string filepath = Path.Combine(Application.streamingAssetsPath, "localization.csv");

    public Localization()
    {
        ReadCSV();
    }

    public string GetText(string key)
    {
        if (!localization.ContainsKey(key))
        {
            string errorMessage = "[LOCALIZATION] Text with key \"" + key + "\" does not exist.";
            Debug.LogError(errorMessage);
            return errorMessage;
        }
        return localization[key].french;
    }

    void ReadCSV()
    {
        if (!File.Exists(filepath))
        {
            Debug.LogError("[LOCALIZATION] File \"" + filepath + "\" does not exist.");
            return;
        }

        localization = new Dictionary<string, LocalizedText>();
        var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan);
        var reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"));
        string line;
        while ((line = reader.ReadLine())!= null)
        {
            var fields = line.Split(';');
            LocalizedText text = new LocalizedText();
            text.french = fields[1];
            localization.TryAdd(fields[0], text);
        }
    }
}
