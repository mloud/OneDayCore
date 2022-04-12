using UnityEngine;

namespace OneDay.Core
{
    [DefaultExecutionOrder(-100)]
    public class ODApp : ABaseMono
    {
        public static ODApp Instance { get; private set; }

        [SerializeField] ManagerHub managerHub;
      
        public ModuleHub ModuleHub { get; private set; }
        public ManagerHub ManagerHub => managerHub;

        protected override void Awake()
        {
            Debug.Assert(Instance == null, $"Instance already exists");
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // create empty module hubs
            ModuleHub = new ModuleHub();
        }
    }
}
