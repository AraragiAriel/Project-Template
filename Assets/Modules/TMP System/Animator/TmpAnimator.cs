using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TmpAnimator : MonoBehaviour
{
    public List<Vector3[]> baseVertices = new();

    private TMP_Text _text;
    public TMP_Text text => _text ??= GetComponent<TMP_Text>();

    public TMP_TextInfo info => text.textInfo;

    private Coroutine updateCo;
    private Dictionary<TmpAnimation, List<TMP_CharacterInfo>> toAnimate = new();

    void OnEnable()
    {
        text.OnPreRenderText += PreRenderText;
        PreRenderText(null);
    }

    void OnDisable()
    {
        text.OnPreRenderText -= PreRenderText;
        if(updateCo != null)
        {
            StopCoroutine(updateCo);
            updateCo = null;
        }
    }

    private void PreRenderText(TMP_TextInfo _)
    {
        if(info.linkCount == 0)
        {
            if(updateCo != null)
            {
                StopCoroutine(updateCo);
                updateCo = null;
            }
            return;
        }

        baseVertices.Clear();
        toAnimate.Clear();

        foreach(var mesh in text.textInfo.meshInfo)
        {
            int length = mesh.vertices == null ? 0 : mesh.vertices.Length;
            Vector3[] array = new Vector3[length];
            if(mesh.vertices != null)
                Array.Copy(mesh.vertices, array, length);
            baseVertices.Add(array);
        }

        foreach(var link in info.linkInfo)
        {
            foreach(var id in link.GetLinkID().Split(','))
            {
                if(Res.data.tmpAnimationsData.dict.TryGetValue(id, out var anim))
                {
                    List<TMP_CharacterInfo> newChars = new();
                    foreach(int i in link.linkTextfirstCharacterIndex.To(link.linkTextfirstCharacterIndex + link.linkTextLength - 1))
                        newChars.Add(info.characterInfo[i]);

                    if(!toAnimate.ContainsKey(anim))
                        toAnimate.Add(anim, newChars);
                    else
                        toAnimate[anim] = toAnimate[anim].Concat(newChars).ToList();
                }
            }
        }

        updateCo ??= StartCoroutine(UpdateCo());
    }

    private IEnumerator UpdateCo()
    {
        while (true)
        {
            if(info.characterCount == 0)
            {
                yield return null;
                continue;
            }

            foreach(var key in toAnimate.Keys)
                foreach(var c in toAnimate[key])
                    if(c.isVisible)
                        key.AnimateCharacter(c, this);

            foreach(int i in 0.To(info.meshInfo.Length - 1))
            {
                var meshInfo = info.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                meshInfo.mesh.colors32 = meshInfo.colors32;
                text.UpdateGeometry(meshInfo.mesh, i);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
