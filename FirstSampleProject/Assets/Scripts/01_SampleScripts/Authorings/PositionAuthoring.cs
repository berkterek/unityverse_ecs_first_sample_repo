using Unity.Entities;
using UnityEngine;

namespace FirstSampleProject.Sample_02_Scripts
{
    //Authoring bizin game object uzerine atadigimiz bir MonoBehaviour class'tir bu class inspector uzerinde datalari tutar ihtiyacimiz olan datalari runtimeda olusan entity'inin component'lerine gonderir ve runtime uzerinde entity'mizi olusturur bunu yapan kisim authorin altinda baker kismdir
    public class PositionAuthoring : MonoBehaviour
    {
        [SerializeField] 
        Transform Transform;

        void OnValidate()
        {
            if (Transform == null)
            {
                Transform = GetComponent<Transform>();
            }
        }

        //baker yaklasimi runtime uzerinde authoring tetiklenmesiyel calisan yapimizdir ve baker olusak olan entity GetEntity ile olusturur ve hangi compnent'leri alicagini yazdigmiz yerdir.
        class PositionBaker : Baker<PositionAuthoring>
        {
            public override void Bake(PositionAuthoring authoring)
            {
                //burda bir entitiy olusturuz runtime uzerinde TransformUsageFlags ile hangi trasnform component'lerini calisma aninda alicagini soylemis oluyoruz ornegin Dynamic dedigmizde bizim entity'mizin haraketli ve position rotation scale bilgilerini tasicagini soylemis oluyoruz orngin none dedigmizde ise bu component'lerin hic birini almayacagini soylemis oluyoruz kisacasi ecs dots uzerinde hangi component'leri alacagini secebiliyoruz
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                //AddComponent iki turlu kullanim sekli vardir birincisi component ekleyip data tasima yontemidir entity'ye hem component ekleriz hemde o component'te authoring uzerinden veri gondeririz
                AddComponent(entity, new PositionData
                {
                    Position = authoring.Transform.position
                });

                //AddComponent ikinci kullanimi authoring uzreinden data gondermiceksek direkt component'i entity uzerine ekliceksek bu AddComponent yapisi kullanilir
                AddComponent<PositionEntityTag>(entity);
            }
        }
    }   
    
    // public class PositionBaker : Baker<PositionAuthoring>
    // {
    //     public override void Bake(PositionAuthoring authoring)
    //     {
    //     }
    // }
}

