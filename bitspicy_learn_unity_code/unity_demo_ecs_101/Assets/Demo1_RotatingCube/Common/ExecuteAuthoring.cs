using Unity.Entities;
using UnityEngine;

namespace HelloCube.Execute
{
    //执行创作组件
    public class ExecuteAuthoring : MonoBehaviour
    {
        public bool MainThread;
        public bool IJobEntity;
        public bool Aspects;
        public bool Prefabs;
        public bool IJobChunk;
        public bool Reparenting;
        public bool EnableableComponents;
        public bool GameObjectSync;

        // Unity DOTS 的 Baker 类是将脚本属性转换为 Unity 组件的接口。
        // 它是在 Unity 的 DOTS 架构中引入的，用于简化将脚本代码转换为可在 DOTS 引擎中运行的组件的任务。
        // 这是一个 Baker 类，它用于将 ExecuteAuthoring 脚本的属性转换为 Unity 组件。
        class Baker : Baker<ExecuteAuthoring>
        {
            public override void Bake(ExecuteAuthoring authoring)
            {
                // 创建一个新的 Entity。
                var entity = GetEntity(TransformUsageFlags.None);

                // 如果 MainThread 为真，则将 MainThread 组件添加到 Entity 上。
                if (authoring.MainThread) AddComponent<MainThread>(entity);
                if (authoring.IJobEntity) AddComponent<IJobEntity>(entity);
                if (authoring.Aspects) AddComponent<Aspects>(entity);
                if (authoring.Prefabs) AddComponent<Prefabs>(entity);
                if (authoring.IJobChunk) AddComponent<IJobChunk>(entity);
                if (authoring.GameObjectSync) AddComponent<GameObjectSync>(entity);
                if (authoring.Reparenting) AddComponent<Reparenting>(entity);
                if (authoring.EnableableComponents) AddComponent<EnableableComponents>(entity);
            }
        }
    }

    public struct MainThread : IComponentData
    {
    }

    public struct IJobEntity : IComponentData
    {
    }

    public struct Aspects : IComponentData
    {
    }

    public struct Prefabs : IComponentData
    {
    }

    public struct IJobChunk : IComponentData
    {
    }

    public struct GameObjectSync : IComponentData
    {
    }

    public struct Reparenting : IComponentData
    {
    }

    public struct EnableableComponents : IComponentData
    {
    }
}
