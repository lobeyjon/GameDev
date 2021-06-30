namespace RoomBuildingStarterKit.Common
{
    using UnityEngine;
    
    /// <summary>
    /// The singleton base class. All monobehaviour singleton class should derived from this class.
    /// </summary>
    public class Singleton<T> : MonoBehaviour
        where T : Singleton<T>
    {
        /// <summary>
        /// The mouse event listener instance.
        /// </summary>
        public static T inst;

        /// <summary>
        /// Initializes a singleton instance.
        /// </summary>
        protected virtual void InitSingletonInst()
        {
            if (inst != null && inst != (T)this)
            {
                Destroy(this);
                return;
            }

            inst = (T)this;
            this.AwakeInternal();
        }

        /// <summary>
        /// Executes when gameObject instantiates.
        /// </summary>
        protected virtual void AwakeInternal()
        {
        }

        /// <summary>
        /// Executes when gameObject instantiates.
        /// </summary>
        private void Awake()
        {
            this.InitSingletonInst();
        }
    }
}