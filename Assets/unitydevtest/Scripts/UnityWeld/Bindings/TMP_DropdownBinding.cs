using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityWeld.Binding.Internal;

namespace UnityWeld.Binding
{
    [RequireComponent(typeof(TMP_Dropdown))]
    [AddComponentMenu("Unity Weld/TMP Dropdown Binding")]
    [HelpURL("https://github.com/Real-Serious-Games/Unity-Weld")]
    public class TMP_DropdownBinding : AbstractMemberBinding
    {
        public string viewModelSelectionPropertyName;
        public string viewModelOptionsPropertyName;
        public string exceptionPropertyName;
        public string exceptionAdapterTypeName;

        private PropertyWatcher selectionPropertyWatcher;
        private UnityEventWatcher selectionEventWatcher;

        [FormerlySerializedAs("selectionViewModelToUIAdapter")]
        public string selectionViewModelToUIAdapterId;

        public string selectionUIToViewModelAdapter;

        [FormerlySerializedAs("optionsAdapter")]
        public string optionsAdapterId;

        private TMP_Dropdown dropdown;

        public override void Connect()
        {
            dropdown = GetComponent<TMP_Dropdown>();

            var selectionPropertyEndPoint = MakeViewModelEndPoint(viewModelSelectionPropertyName, selectionUIToViewModelAdapter, null);

            var selectionPropertySync = new PropertySync(
                selectionPropertyEndPoint,
                new PropertyEndPoint(
                    this,
                    "SelectedOption",
                    TypeResolver.GetAdapter(selectionViewModelToUIAdapterId),
                    null,
                    "view",
                    this
                ),
                !string.IsNullOrEmpty(exceptionPropertyName)
                    ? MakeViewModelEndPoint(exceptionPropertyName, exceptionAdapterTypeName, null)
                    : null,
                this
            );

            selectionPropertyWatcher = selectionPropertyEndPoint.Watch(() => selectionPropertySync.SyncFromSource());

            selectionEventWatcher = new UnityEventWatcher(
                dropdown,
                "onValueChanged",
                () =>
                {
                    SelectedOption = Options[dropdown.value]; // Copy value back from dropdown.
                    selectionPropertySync.SyncFromDest();
                }
            );

            var optionsPropertySync = new PropertySync(
                MakeViewModelEndPoint(viewModelOptionsPropertyName, null, null),
                new PropertyEndPoint(
                    this,
                    "Options",
                    TypeResolver.GetAdapter(selectionViewModelToUIAdapterId),
                    null,
                    "view",
                    this
                ),
                null,
                this
            );

            // Copy the initial value from view-model to view.
            optionsPropertySync.SyncFromSource();
            selectionPropertySync.SyncFromSource();
            UpdateOptions();
        }

        public override void Disconnect()
        {
            if (selectionPropertyWatcher != null)
            {
                selectionPropertyWatcher.Dispose();
                selectionPropertyWatcher = null;
            }

            if (selectionEventWatcher != null)
            {
                selectionEventWatcher.Dispose();
                selectionEventWatcher = null;
            }

            dropdown = null;
        }

        private string selectedOption = string.Empty;
        private string[] options = new string[0];

        public string[] Options
        {
            get => options;
            set
            {
                options = value;

                if (dropdown != null)
                {
                    UpdateOptions();
                }
            }
        }

        public string SelectedOption
        {
            get => selectedOption;
            set
            {
                if (selectedOption == value)
                {
                    return;
                }

                selectedOption = value;

                UpdateSelectedOption();
            }
        }

        private void UpdateOptions()
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options.ToList());
        }

        private void UpdateSelectedOption()
        {
            if (dropdown == null)
            {
                return; // Not connected.
            }

            dropdown.value = Array.IndexOf(options, selectedOption);
        }
    }
}