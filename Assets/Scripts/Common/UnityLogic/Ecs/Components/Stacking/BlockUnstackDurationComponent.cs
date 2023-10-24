namespace Common.UnityLogic.Ecs.Components.Stacking
{
    public struct BlockUnstackDurationComponent
    {
        public BlockUnstackDurationComponent(float timer)
        {
            Timer = timer;
        }
        
        public float Timer { get; set; }
    }
}