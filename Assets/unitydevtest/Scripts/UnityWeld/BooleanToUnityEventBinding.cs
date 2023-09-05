using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding.Internal;

namespace UnityWeld.Binding
{
    public class BooleanToUnityEventBinding : AbstractMemberBinding
    {
        public string ViewModelPropertyName
        {
            get { return viewModelPropertyName; }
            set { viewModelPropertyName = value; }
        }

        [SerializeField] private string viewModelPropertyName = default;

        [SerializeField] private UnityEvent onToggleOn = default;
        [SerializeField] private UnityEvent onToggleOff = default;

        /// <summary>
        /// Watcher for the view-model for changes that must be propagated to the view. 
        /// </summary>
        private PropertyWatcher viewModelWatcher;

        public bool BooleanValue
        {
            set
            {
                ToggleUnityEvent(value);
            }
        }

        public override void Connect()
        {
            var viewModelEndPoint = MakeViewModelEndPoint(viewModelPropertyName, null, null);
            var propertySync = new PropertySync(
                // Source
                viewModelEndPoint,

                // Dest
                new PropertyEndPoint(
                    this,
                    "BooleanValue",
                    null,
                    null,
                    "view",
                    this
                ),

                // Errors, exceptions, and validation.
                null, // Validation not needed

                this
            );

            viewModelWatcher = viewModelEndPoint.Watch(
                () => propertySync.SyncFromSource()
            );

            // Copy the initial value over from the view-model.
            propertySync.SyncFromSource();
        }

        public override void Disconnect()
        {
            if (viewModelWatcher != null)
            {
                viewModelWatcher.Dispose();
                viewModelWatcher = null;
            }
        }

        private void ToggleUnityEvent(bool value)
        {
            if (value)
                onToggleOn.Invoke();
            else
                onToggleOff.Invoke();
        }
    }
}