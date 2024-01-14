using Unity.Entities;

namespace FirstSampleProject.Sample_02_Scripts
{
    //PositionEntityTag burda entity'leri birbirinden ayrimak icin tag olarak component'ler kullanilir ayni yontem Mono GameObject'ler icin Enum kullanilirken burda tag yontemi kullanilir mantik sunun uzerinden yurur bana PositionData, PositionEntityTag ve LocalTransform olanlari bir query uzerinde don veya PositionData ve LocalTransform olan entity'leri don seklindedir cunku dod(data oriented design) yaklasiminda datalari coklu yonetiriz ve bu coklu yonetimi ve datalalari birbirinden ayristirmak icin kullanilan yontemlerden bir tanesidir bu tag yontemi
    public struct PositionEntityTag : IComponentData{}
}