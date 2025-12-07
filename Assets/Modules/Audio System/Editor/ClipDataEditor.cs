using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;

[CustomEditor(typeof(ClipData), true)]
[CanEditMultipleObjects]
public class ClipDataEditor : Editor
{
    private List<AudioSource> sources = new();
    private int times = 1;

    private ClipData _data;
    private ClipData data => _data??= target as ClipData;

    public override void OnInspectorGUI(){
        serializedObject.Update();

        List<string> toOmit = new();
        if(data.pitchType != PitchType.Custom)
            toOmit.Add("customPitch");
        if(data.delayType is not(DelayType.To or DelayType.FromTo))
            toOmit.Add("delayTo");
        if(data.delayType != DelayType.FromTo)
            toOmit.Add("delayFrom");

        DrawPropertiesExcluding(serializedObject, toOmit.ToArray());

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(32);

        times = EditorGUILayout.IntSlider("Times", times, 1, 10);
        if(GUILayout.Button("Preview")){
            foreach(int _ in 1.To(times))
                EditorCoroutineUtility.StartCoroutineOwnerless(PreviewCo());         
        }

        foreach(var source in sources)
            if(source != null && !source.isPlaying)
                DestroyImmediate(source.gameObject);
        sources.TrimNull();
    }

    private IEnumerator PreviewCo(){
        yield return new EditorWaitForSeconds(data.delay);

        var obj = new GameObject();
        obj.hideFlags = HideFlags.DontSave;
        AudioSource source = obj.AddComponent(typeof(AudioSource)) as AudioSource;
        source.clip = data.clip;
        source.volume = data.volume;
        source.pitch = data.pitch;
        source.spatialBlend = 0f;
        source.Play();
        sources.Add(source);
    }
}
