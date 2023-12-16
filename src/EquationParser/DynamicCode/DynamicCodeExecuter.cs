using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace DynamicCode
{
    public class DynamicCodeExecuter
    {
        public static Script Compile(string code)
        {
            try
            {
                // Compile the script
                Script script = CSharpScript.Create(code);
                script.Compile();
                return script;
            }
            catch (CompilationErrorException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static async Task<double> ExecuteDynamicCodeAsync(Script script, string call)
        {
            try
            {
                ScriptState<double> cd = await script.ContinueWith<double>(call).RunAsync();
                return cd.ReturnValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to execute: " + call);
                Console.WriteLine(ex.Message);
                return -1; // Or another suitable error code
            }
        }
    }
}