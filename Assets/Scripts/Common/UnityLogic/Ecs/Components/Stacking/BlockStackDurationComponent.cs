namespace Common.UnityLogic.Ecs.Components.Stacking
{
    public struct BlockStackDurationComponent
    {
        public BlockStackDurationComponent(float timer)
        {
            Timer = timer;
        }
        
        public float Timer { get; set; }
    }
}