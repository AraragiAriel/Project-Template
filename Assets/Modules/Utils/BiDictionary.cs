using System.Collections.Generic;
using System.Collections;

public class BiDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>{
    private Dictionary<T1, T2> forward = new();
    private Dictionary<T2, T1> backward = new();

    public BiDictionary(){}

    public void Add(T1 key, T2 value){
        forward.Add(key, value);
        backward.Add(value, key);
    }

    public T2 Get(T1 key) => forward[key];
    public T1 Get(T2 key) => backward[key];

    public bool TryGet(T1 key, out T2 value) => forward.TryGetValue(key, out value);
    public bool TryGet(T2 key, out T1 value) => backward.TryGetValue(key, out value);

    public bool Remove(T1 key){
        if(forward.TryGetValue(key, out T2 val)){
            forward.Remove(key);
            backward.Remove(val);
            return true;
        }
        return false;
    }

    public bool Remove(T2 value){
        if(backward.TryGetValue(value, out T1 key)){
            backward.Remove(value);
            forward.Remove(key);
            return true;
        }
        return false;
    }   
    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => forward.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}