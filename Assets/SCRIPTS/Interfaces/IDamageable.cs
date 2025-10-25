using UnityEngine;

public interface IDamageable
{
    public void Damage(DamageData damageData);
}

public class DamageData{
    public float damage = 0f;

    public DamageData(){}
    
    public DamageData(float damage){
        this.damage = damage;
    }
}
