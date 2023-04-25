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

public enum Language
{
    French,
    English
}

public class Localization
{
    private Dictionary<string, LocalizedText> localization;

    private readonly string filepath = Path.Combine(Application.streamingAssetsPath, "localization.csv");

    private readonly string startToken = "<loc>";

    public Localization()
    {
        ReadCSV();
    }

    public string GetText(string key)
    {
        key = key.Trim();
        if (!localization.TryGetValue(key, out var text))
        {
            string errorMessage = "[LOCALIZATION] Text with key \"" + key + "\" does not exist.";
            Debug.LogError(errorMessage);
            return errorMessage;
        }
        Language language = (Language)PlayerPrefs.GetInt(PauseMenu.language, 0);
        switch (language)
        {
            case Language.French:
                return text.french;
            case Language.English:
                return text.english;
            default:
                return text.english;
        }
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
        string entry = "";
        while ((line = reader.ReadLine()) != null)
        {
            if (line.StartsWith(startToken))
            {
                // process previous entry
                var fields = entry.Split(';');
                if (fields.Length > 2)
                {
                    var text = new LocalizedText();
                    text.french = fields[2].Trim('\"');
                    text.french = text.french.Replace('’', '\'');
                    if (fields.Length >= 4)
                    {
                        text.english = fields[3].Trim('\"');
                        text.english = text.english.Replace('’', '\'');
                    }
                    localization.TryAdd(fields[1], text);
                }
                entry = "";
            }
            entry += line + "\n";
        }
    }

    private void Log()
    {
        foreach (var key in localization.Keys)
        {
            Debug.Log(key + " : " + localization[key].french);
        }
    }
}
