using System.Collections.Generic;
using UnityEngine;

public class TmpFixedTag : MonoBehaviour
{
    [SerializeField] private List<string> _tags;
    public List<string> tags => _tags;
}
