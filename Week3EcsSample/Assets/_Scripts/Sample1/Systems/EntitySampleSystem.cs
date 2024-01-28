using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    public partial struct EntitySampleSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var job1 = new EntityNumberIncreaseJob1();
            var job2 = new EntityNumberIncreaseJob2();

            //job1 bir once calisir system state'ine gore calistiriirz Dependency bu anlama gelir ve bu job calistiktan sonra bize bir handle doner
            //donen handle'i Job'e veririz burdaki anlami birdeki gibi system state'ine gore degil job1 state'ine gore calis demis oluruz ve bunun donen job handle'ini ayni sekil bittikten sonra state dependency'e don demis oluyoruz kisacasi job1 once calisr job1 bittiken sonra job2'i calistir demis oluruz bu schedule kullamina en buyuk ornektir birbiirni takip eden islerde farkli job'larin biri bitip bir baska job'in calismasi gibi senaryoalrda biz schedule kullaniriz.
            // var job1Handle = job1.Schedule(state.Dependency);
            // var job2Handle = job2.Schedule(job1Handle);
            // state.Dependency = job2Handle;

            
            var job2Handle = job2.Schedule(state.Dependency);
            var job1Handle = job1.Schedule(job2Handle);
            state.Dependency = job1Handle;
        }
    }

    public partial struct EntityNumberIncreaseJob1 : IJobEntity
    {
        private void Execute(ref EntitySampleData entitySampleData)
        {
            entitySampleData.Number += 1;
            Debug.Log($"{nameof(EntityNumberIncreaseJob1)}");

            if (entitySampleData.Number > 1000) entitySampleData.Number = 0;
        }
    }
    
    public partial struct EntityNumberIncreaseJob2 : IJobEntity
    {
        private void Execute(ref EntitySampleData entitySampleData)
        {
            entitySampleData.Number += 2;
            Debug.Log($"{nameof(EntityNumberIncreaseJob2)}");
            
            if (entitySampleData.Number > 1000) entitySampleData.Number = 0;
        }
    }
}