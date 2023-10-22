using Leopotam.Ecs;

namespace Common.Infrastructure.Services.ECS
{
    public interface IEcsStartup
    {
        EcsWorld World { get; }
        public void SendMessage<T>(in T message) where T : struct;
    }
}