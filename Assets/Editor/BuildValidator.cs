using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildValidator : IPreprocessBuildWithReport
{
    private const string debug = "INCLUDE_DEBUG";
    
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        bool includeDebugTools = EditorUtility.DisplayDialog(
            "Incluir scripts de debug?",
            "Deseja incluir TestScript na build?",
            "Sim",
            "Não"
        );

        var symbols = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.Standalone);

        if(includeDebugTools){
            if(!symbols.Contains(debug))
                symbols += $";{debug}";
        } else {
            symbols = symbols.Replace(debug, "");
        }

        PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, symbols);
    }
}