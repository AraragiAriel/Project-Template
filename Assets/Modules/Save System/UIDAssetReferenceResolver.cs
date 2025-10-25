using Odin.OdinSerializer;

public class UIDAssetReferenceResolver : IExternalStringReferenceResolver
{
    public IExternalStringReferenceResolver NextResolver{get; set;}

    public bool CanReference(object value, out string id){
        id = null;

        if(value is UIDAsset uidAsset){
            id = uidAsset.uid;
            return true;
        }

        return false;
    }

    public bool TryResolveReference(string id, out object value){
        if(!string.IsNullOrEmpty(id)){
            value = Res.data.uids.Get<UIDAsset>(id);
            return value != null;
        }

        value = null;
        return false;
    }

    public bool TryResolveReference(object context, string id, out object value)
        => TryResolveReference(id, out value);

    public bool TryReference(object context, object value, out string id)
        => CanReference(value, out id);
}
