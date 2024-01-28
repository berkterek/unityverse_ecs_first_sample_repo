using Unity.Entities;

namespace Sample2
{
    public struct MoveData : IComponentData
    {
        public float MoveSpeed;
        public float Timer;
    }

    //IEnableableComponent bizim compnent'lerimizi enable veya disable etmemizi saglar bu yontem ile biz system'ler uzerinde state'leri yonetiriz ornegin bir AttackTag ve bir MoveTag oldugunu dusunelim MoveTag acik ve AttackTag kapali oldugunda entity'nin yurumusi gerekir ama ters islemde ise MoveTag kapali AttackTag acikta durmasi ve saldiri islemini gerceklestirmsi gerekir gibi state yonetimini saglariz
    public struct MoveTag : IComponentData, IEnableableComponent {}
    
    public struct AttackTag : IComponentData, IEnableableComponent{}

    public struct AttackData : IComponentData
    {
        public float Damage;
        public float Timer;
    }
}