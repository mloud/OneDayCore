namespace OneDay.Core.Components
{
    public class DontDestroy : ABaseMono
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}