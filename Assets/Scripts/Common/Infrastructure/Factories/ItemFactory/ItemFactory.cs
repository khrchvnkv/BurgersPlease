using Zenject;

namespace Common.Infrastructure.Factories.ItemFactory
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly DiContainer _diContainer;

        public ItemFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
    }
}