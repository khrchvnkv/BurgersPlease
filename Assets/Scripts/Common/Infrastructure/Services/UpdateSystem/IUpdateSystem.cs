using System;

namespace Common.Infrastructure.Services.UpdateSystem
{
    public interface IUpdateSystem
    {
        event Action OnUpdate;
        event Action OnFixedUpdate;
        event Action OnLateUpdate;
    }
}