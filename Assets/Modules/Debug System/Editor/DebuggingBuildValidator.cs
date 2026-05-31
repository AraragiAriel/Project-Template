using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebuggingBuildValidator : IPreprocessBuildWithReport, IProcessSceneWithReport
{
    public int callbackOrder => 0;

    private bool includeDebugTools;    

    public void OnPreprocessBuild(BuildReport report){
        includeDebugTools = EditorUtility.DisplayDialog(
            "DEBUGGING",
            "Include dev tools?",
            "Yes",
            "No"
        );
    }

    public void OnProcessScene(Scene scene, BuildReport report){
        if(!BuildPipeline.isBuildingPlayer) return;

        if(includeDebugTools){
            Debug.Log("Including debug tools in build");
            return;
        }

        foreach(var rootObjects in scene.GetRootGameObjects()){
            var testComponents = rootObjects.GetComponentsInChildren<DevTools>(true);

            foreach(var testObject in testComponents){
                GameObject.DestroyImmediate(testObject.gameObject);
            }
        }
    }
}