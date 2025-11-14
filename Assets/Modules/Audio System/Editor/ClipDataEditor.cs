using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClipData), true)]
[CanEditMultipleObjects]
public class ClipDataEditor : Editor
{
    private List<AudioSource> sources = new();

    public override void OnInspectorGUI(){
        serializedObject.Update();

        ClipData script = target as ClipData;

        List<string> toOmit = new();
        if(script.pitchType != PitchType.Custom)
            toOmit.Add("customPitch");

        DrawPropertiesExcluding(serializedObject, toOmit.ToArray());

        serializedObject.ApplyModifiedProperties();

        if(GUILayout.Button("Preview")){
            var obj = new GameObject();
            obj.hideFlags = HideFlags.DontSave;
            AudioSource source = obj.AddComponent(typeof(AudioSource)) as AudioSource;
            source.clip = script.clip;
            source.volume = Mathf.Pow(script.volume, 2f);
            source.pitch = script.pitch;
            source.spatialBlend = 0f;
            source.Play();
            sources.Add(source);
        }

        int count = sources.Count;
        for(int i = count - 1; i >= 0; i--)
            if(sources[i] != null)
                if(!sources[i].isPlaying)
                    DestroyImmediate(sources[i].gameObject);
    }
}
