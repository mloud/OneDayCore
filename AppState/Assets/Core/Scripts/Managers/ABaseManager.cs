namespace OneDay.Core
{
    public abstract class ABaseManager : InjectableMono
    {
        public void Initialize()
        {
            InternalInitialize();
        }

        public void Release()
        {
            InternalRelease();
        }

        protected virtual void InternalInitialize() { }
        protected virtual void InternalRelease() { }
    }
}