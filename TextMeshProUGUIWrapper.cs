namespace RoomBuildingStarterKit.Common
{
    using System;
    using TMPro;
    using UnityEngine;

    /// <summary>
    /// The TextMeshProUGUIWrapper class used to wrap the unity built-in TextMeshProUGUI and make it to support multi-language and response language settings change.
    /// </summary>
    public class TextMeshProUGUIWrapper : TextMeshProUGUI
    {
        /// <summary>
        /// The UIText enum.
        /// </summary>
        public UIText uiText;

        /// <summary>
        /// The string format arguments.
        /// </summary>
        public string[] args;

        /// <summary>
        /// Executes when the languate settings changed.
        /// </summary>
        public void OnChange()
        {
            if (LanguageManager.inst != null)
            {
                this.text = string.Format(LanguageManager.inst.GetText(this.uiText, out TMP_FontAsset font), this.args ?? Array.Empty<string>());
                this.font = font;
            }
        }

        /// <summary>
        /// Sets the text with string format.
        /// </summary>
        /// <param name="text">The ui text.</param>
        /// <param name="args">The text string format arguments</param>
        public void SetGlobalText(UIText text, params string[] args)
        {
            this.uiText = text;
            this.args = args;
            this.OnChange();
        }

        /// <summary>
        /// Executes after Awake.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            if (LanguageManager.inst != null)
            {
                LanguageManager.inst.OnChangeLanguageHandler += OnChange;
                this.PreProcessText();
            }
        }

        protected override void Start()
        {
            base.Start();

            this.OnChange();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (LanguageManager.inst != null)
            {
                LanguageManager.inst.OnChangeLanguageHandler -= OnChange;
            }
        }

        /// <summary>
        /// Converts text from UIText enum to string.
        /// </summary>
        private void PreProcessText()
        {
            if (this.text.StartsWith("UITEXT.") && (Enum.TryParse<UIText>(this.text.Substring(7), true, out UIText result)))
            {
                this.uiText = result;
            }
            
            this.OnChange();
        }
    }
}