/*
这段代码是一个使用Unity的ECS（实体组件系统）的旋转系统。ECS是一种面向数据的框架，
它与GameObject兼容，能够让经验丰富的Unity创作者通过前所未有的控制和确定性来构建更具野心的游戏。
*/

using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube.MainThread
{
    public partial struct RotationSystem : ISystem
    {
        /*
        这是OnCreate方法，它在系统创建时被调用。它使用了BurstCompile属性来编译此方法以获得更高的性能。
        在此方法中，它调用了state.RequireForUpdate<Execute.MainThread>()来指定此系统仅在主线程上更新。
        */
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Execute.MainThread>();
        }

        /*
        这是OnUpdate方法，它在每一帧中被调用。在此方法中，它首先获取了当前帧与上一帧之间的时间差（DeltaTime） 
        */
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            // Loop over every entity having a LocalTransform component and RotationSpeed component.
            // In each iteration, transform is assigned a read-write reference to the LocalTransform,
            // and speed is assigned a read-only reference to the RotationSpeed component.
            /*
            接下来，代码使用了一个foreach循环来遍历所有具有LocalTransform组件和RotationSpeed组件的实体。在每次迭代中，transform被赋予对LocalTransform组件的读写引用，
            speed被赋予对RotationSpeed组件的只读引用。然后，代码使用string.Format创建了一个消息字符串，并使用Debug.Log将其输出到控制台。
            */
            foreach (var (transform, speed) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                var msg = string.Format("system :{0} speed={1}", deltaTime, speed.ValueRO.RadiansPerSecond);
                Debug.Log(msg);
                // ValueRW and ValueRO both return a ref to the actual component value.
                // The difference is that ValueRW does a safety check for read-write access while
                // ValueRO does a safety check for read-only access.
                /*
                ValueRW 和 ValueRO 都返回实际组件值的引用。
                区别在于 ValueRW 对读写访问进行安全检查，而ValueRO 对只读访问进行安全检查。
                */
                transform.ValueRW = transform.ValueRO.RotateY(
                    speed.ValueRO.RadiansPerSecond * deltaTime);
            }
        }
    }
}
