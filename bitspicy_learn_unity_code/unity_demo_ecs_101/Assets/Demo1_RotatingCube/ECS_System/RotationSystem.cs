/*
��δ�����һ��ʹ��Unity��ECS��ʵ�����ϵͳ������תϵͳ��ECS��һ���������ݵĿ�ܣ�
����GameObject���ݣ��ܹ��þ���ḻ��Unity������ͨ��ǰ��δ�еĿ��ƺ�ȷ��������������Ұ�ĵ���Ϸ��
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
        ����OnCreate����������ϵͳ����ʱ�����á���ʹ����BurstCompile����������˷����Ի�ø��ߵ����ܡ�
        �ڴ˷����У���������state.RequireForUpdate<Execute.MainThread>()��ָ����ϵͳ�������߳��ϸ��¡�
        */
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Execute.MainThread>();
        }

        /*
        ����OnUpdate����������ÿһ֡�б����á��ڴ˷����У������Ȼ�ȡ�˵�ǰ֡����һ֮֡���ʱ��DeltaTime�� 
        */
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            // Loop over every entity having a LocalTransform component and RotationSpeed component.
            // In each iteration, transform is assigned a read-write reference to the LocalTransform,
            // and speed is assigned a read-only reference to the RotationSpeed component.
            /*
            ������������ʹ����һ��foreachѭ�����������о���LocalTransform�����RotationSpeed�����ʵ�塣��ÿ�ε����У�transform�������LocalTransform����Ķ�д���ã�
            speed�������RotationSpeed�����ֻ�����á�Ȼ�󣬴���ʹ��string.Format������һ����Ϣ�ַ�������ʹ��Debug.Log�������������̨��
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
                ValueRW �� ValueRO ������ʵ�����ֵ�����á�
                �������� ValueRW �Զ�д���ʽ��а�ȫ��飬��ValueRO ��ֻ�����ʽ��а�ȫ��顣
                */
                transform.ValueRW = transform.ValueRO.RotateY(
                    speed.ValueRO.RadiansPerSecond * deltaTime);
            }
        }
    }
}
