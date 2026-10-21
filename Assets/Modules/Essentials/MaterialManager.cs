using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    private Renderer _rend;
    private Renderer rend => _rend ??= GetComponent<Renderer>();

    private MaterialPropertyBlock _block;
    private MaterialPropertyBlock block
    {
        get
        {
            if(_block == null)
            {
                _block = new();
                rend.GetPropertyBlock(_block);
            }
            return _block;
        }
    }

    private Graphic _graphic;
    private Graphic graphic => _graphic ??= GetComponent<Graphic>();

    private Material _material;
    private Material material
    {
        get
        {
            if(_material == null)
            {
                _material = new(graphic.material);
                graphic.material = _material;
            }
            return _material;
        }
    }

    public void SetInt(string name, int value)
    {
        if(rend != null)
        {
            block.SetInteger(name, value);
            rend.SetPropertyBlock(block);
        }
        else if(graphic != null)
        {
            material.SetInteger(name, value);
        }
    }

    public void SetFloat(string name, float value)
    {
        if(rend != null)
        {
            block.SetFloat(name, value);
            rend.SetPropertyBlock(block);
        }
        else if(graphic != null)
        {
            material.SetFloat(name, value);
        }
    }

    public void SetColor(string name, Color value)
    {
        if(rend != null)
        {
            block.SetColor(name, value);
            rend.SetPropertyBlock(block);
        }
        else if(graphic != null)
        {
            material.SetColor(name, value);
        }
    }
}
