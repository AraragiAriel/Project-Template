using UnityEngine;

public interface ILocalizer
{
    public LocalizedString Localize(string field = default);
}
