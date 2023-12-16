using DynamicCode;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class TokenManager
{
    private static List<string[]> tokensList = new List<string[]>();
    private static Dictionary<string, string> tokenCode = new Dictionary<string, string>();
    private static Script tokenSkrip;
    private static CultureInfo culturInfo = new CultureInfo("en-US");
    private static List<string> codeUsings = new List<string>();


    public static void LoadTokensFromFile(string filePath)
    {
        string jsonContent = "";
        try
        {
            jsonContent = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: File wanst Found");
            return;
        }
        try
        {
            List<List<string>> tList = new List<List<string>>();
            JObject json = JObject.Parse(jsonContent);
            if (!json.ContainsKey("tokens"))
            {
                Console.WriteLine("Error: Token file is not valid!");
                return;
            }
            JArray tokens = (JArray)json["tokens"];
            tokenCode = new Dictionary<string, string>();
            foreach (JToken tokenToken in tokens)
            {
                JObject tokenObj = (JObject)tokenToken;
                string token = (string)tokenObj["token"];
                int priority = (int)tokenObj["priority"];
                string code = (string)tokenObj["code"];
                if (tList.Count < priority + 1)
                {
                    int d = (priority + 1 - tList.Count);
                    for (int i = 0; i < d; i++)
                    {
                        tList.Add(new List<string>());
                    }
                }
                tList[priority].Add(token);
                tokenCode.Add(token, code);
            }
            tokensList = new List<string[]>();
            foreach (List<string> lists in tList)
            {
                tokensList.Add(lists.ToArray());
            }
            codeUsings = new List<string>();
            if (json.ContainsKey("using"))
            {
                JArray usings = (JArray)json["using"];
                foreach (JToken tok in usings)
                {
                    codeUsings.Add("using " + (string)tok + ";");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: Failed to load tokens!");
            Console.WriteLine(ex.Message);
            return;
        }
        
    }

    public static void CompileTokenMethods()
    {
        string combinedCode = "";
        int index = 0;
        foreach(string use in codeUsings)
        {
            combinedCode += use + "\n";
        }
        foreach (KeyValuePair<string,string> code in tokenCode)
        {
            combinedCode += code.Value.Replace("run", "run" + index);
            combinedCode += "\n";
            index++;
        } 
        tokenSkrip = DynamicCodeExecuter.Compile(combinedCode);
    }

    public static double CalculateToken(string token, double a, double b)
    {
        string call = "run"+ GetTokenIndex(token)+ "(" + a.ToString(culturInfo) + "," + b.ToString(culturInfo) + ")";
        double result = Task.Run(() => DynamicCodeExecuter.ExecuteDynamicCodeAsync(tokenSkrip,call)).Result;
        return result;
    }


    public static int GetTokenIndex(string token)
    {
        int index = 0;
        foreach(string s in tokenCode.Keys.ToArray())
        {
            if(s == token)
            {
                return index;
            }
            index++;   
        }
        Console.WriteLine("Error: Operator was not Found: " + token);
        return 0;
    }

    public static void Log()
    {
        Console.WriteLine("Tokens:");
        int prio = 0;
        foreach (string[] tokens in tokensList)
        {
            foreach(string t in tokens)
            {
                Console.WriteLine(prio + ": " + t);
            }
            prio++;
        }
        Console.WriteLine();
        foreach(KeyValuePair<string,string> scripts in tokenCode)
        {
            Console.WriteLine(scripts.Key + ": ");
            Console.WriteLine(scripts.Value);
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static List<string[]> GetTokens()
    {
        return tokensList;
    }
    public static bool ContainsToken(string token){
        return tokenCode.ContainsKey(token);
    }
}