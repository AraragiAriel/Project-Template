using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TweenUse : MonoBehaviour
{
    public List<TweenData.Parameters> parameters;

    public IEnumerator Tween() => Tween(parameters);

    public IEnumerator Tween(TweenData.Type type)
    {
        var parameters = ParametersCopy();
        parameters.ForEach(p => p.type = type);
        yield return Tween(parameters);
    }

    public IEnumerator Tween(List<TweenData.Parameters> parameters)
    {
        yield return this.StartCoroutines(
            parameters.Where(p => p.data != null).Select(p => p.data.Tween(p)).ToArray()
        );
    }

    public void Set() => Set(parameters);

    public void Set(TweenData.Type type)
    {
        var parameters = ParametersCopy();
        parameters.ForEach(p => p.type = type);
        Set(parameters);
    }

    public void Set(List<TweenData.Parameters> parameters)
    {
        parameters.ForEach(p => p.data.SetEnd(p));
    }

    public List<TweenData.Parameters> ParametersCopy()
        => parameters.Select(p => p.Clone()).ToList();
}