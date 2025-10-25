using UnityEngine;

public class SetupBool{
    private bool _setup = true;
    public bool setup{
        get{
            if(!_setup)
                return false;
            _setup = false;
            return true;
        }
    }

    public static implicit operator bool (SetupBool b) => b != null ? b.setup : false;
}