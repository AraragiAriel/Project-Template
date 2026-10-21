using System.Collections;
using UnityEngine;

public class AlternateToggler : Toggler
{
    [SerializeField] private int increment;
    [SerializeField] private float duration;
    [SerializeField] private Vector2Int spacing = Vector2Int.one;

    private void OnEnable()
    {
        StartCoroutine(FlickCo());
    }

    private IEnumerator FlickCo()
    {
        int size = spacing.x + spacing.y;
        int pos = 0;

        while(true)
        {
            pos += increment;

            Reset();
            foreach(int i in 0.To(units.Count - 1))
                units[i].Toggle((i + pos).Mod(size) < spacing.x);

            yield return new WaitForSeconds(duration);
        }
    }
}