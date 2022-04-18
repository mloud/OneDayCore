using OneDay.Core.Inject;

namespace OneDay.Core
{
    public abstract class InjectableMono : ABaseMono
    {
        protected override void Awake()
        {
            base.Awake();
            ModuleInjector.TryToInject(this);
        }
    }
}
