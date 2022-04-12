using System.Collections;

namespace OneDay.Core.Sequences
{
    public abstract class ABaseSequence : InjectableMono
    {
        public string Id;
        public virtual void Initialize() { }
        public virtual void Release() { }
        public abstract IEnumerator Play(KeyValueData data);
    }
}
