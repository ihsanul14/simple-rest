using System;
using System.IO;
using System.Collections.Generic;

namespace simple_rest.framework.config;

public class Config{

    public static void Load(string filename){
        Dictionary<string, string> envVariables = new Dictionary<string, string>();
    string[] lines = File.ReadAllLines(filename);
    foreach (string line in lines)
    {
        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
        {
            string[] parts = line.Split('=', 2);
            if (parts.Length == 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim('"');
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
    }

}
