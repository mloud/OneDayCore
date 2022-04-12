using System.Collections;
using UnityEngine;

namespace OneDay.Core.Ui
{
    public abstract class ABaseShow : MonoBehaviour
    {
        public abstract IEnumerator Show();
    }
}

