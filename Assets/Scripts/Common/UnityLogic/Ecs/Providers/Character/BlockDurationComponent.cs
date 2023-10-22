namespace Common.UnityLogic.Ecs.Providers.Character
{
    public struct BlockDurationComponent
    {
        public BlockDurationComponent(float timer)
        {
            Timer = timer;
        }

        public float Timer { get; set; }
    }
}