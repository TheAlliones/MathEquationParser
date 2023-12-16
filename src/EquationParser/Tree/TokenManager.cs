using DynamicCode;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class TokenManager
{
    private static List<string[]> tokensList = new List<string[]>();
    private static Dictionary<string, string> tokenCode = new Dictionary<string, string>();
    private static Dictionary<string, Script> tokenScripts = new Dictionary<string, Script>();
    private static CultureInfo culturInfo = new CultureInfo("en-US");

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
            string token = (string) tokenObj["token"];
            int priority = (int)tokenObj["priority"];
            string code = (string)tokenObj["code"];
            if(tList.Count < priority + 1)
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
    }

    public static void CompileTokenMethods()
    {
        tokenScripts = new Dictionary<string, Script>();
        foreach (KeyValuePair<string,string> code in tokenCode)
        {
            Script sript = DynamicCodeExecuter.Compile(code.Value);
            tokenScripts[code.Key] = sript;
        }
    }

    public static double CalculateToken(string token, double a, double b)
    {
        string call = "run(" + a.ToString(culturInfo) + "," + b.ToString(culturInfo) + ")";
        double result = Task.Run(() => DynamicCodeExecuter.ExecuteDynamicCodeAsync(tokenScripts[token],call)).Result;
        return result;
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
        foreach(KeyValuePair<string,Script> scripts in tokenScripts)
        {
            Console.WriteLine(scripts.Key + ": ");
            Console.WriteLine(scripts.Value.Code);
        }
    }

    public static List<string[]> GetTokens()
    {
        return tokensList;
    }
    public static bool ContainsToken(string token){
        return tokenScripts.ContainsKey(token);
    }
}