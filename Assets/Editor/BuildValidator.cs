using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildValidator : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report){
    }
}