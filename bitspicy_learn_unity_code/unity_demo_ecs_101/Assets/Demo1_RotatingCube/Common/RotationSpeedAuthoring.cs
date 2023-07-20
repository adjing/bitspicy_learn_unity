/*
这段代码定义了一个名为RotationSpeedAuthoring的创作组件（authoring component），
它是一个普通的MonoBehaviour。它还定义了一个名为RotationSpeed的ECS组件，用于存储实体的旋转速度。

*/

using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace HelloCube
{
    // An authoring component is just a normal MonoBehavior.
    /*
    创作组件（authoring component）是一种特殊的MonoBehaviour组件，
    它用于在Unity编辑器中定义ECS实体的数据。
    您可以将创作组件添加到子场景（SubScene）中的GameObject上，
    然后创建烘焙器（baker）来在转换后的实体上附加ECS组件。
    当您创建一个烘焙器时，您需要定义它所对应的MonoBehaviour组件，然后编写代码，
    使用MonoBehaviour组件的数据来创建并附加ECS组件到转换后的实体上。
    创作组件允许您使用熟悉的Unity编辑器工作流程来定义ECS实体的数据。
    它们是一种方便的方式，
    可以让您在编辑器中可视化地创建和编辑ECS实体。
    */
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;

        // In baking, this Baker will run once for every RotationSpeedAuthoring instance in an entity subscene.
        // (Nesting an authoring component's Baker class is simply an optional matter of style.)
        class Baker : Baker<RotationSpeedAuthoring>
        {
            public override void Bake(RotationSpeedAuthoring authoring)
            {
                
                Debug.LogFormat("RotationSpeedAuthoring Bake={0}",Time.time);

                // The entity will be moved
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                });
            }
        }
    }

    /*
    代码定义了一个名为RotationSpeed的结构体，它实现了IComponentData接口。
    它有一个公共字段RadiansPerSecond，用于存储每秒旋转的弧度。 
    */
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}
