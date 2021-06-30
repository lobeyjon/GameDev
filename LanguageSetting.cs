namespace RoomBuildingStarterKit.Configurations
{
    using RoomBuildingStarterKit.Common;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The LanguageSetting class.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "LanguageSetting", menuName = "GameSettings/LanguageSetting", order = 1)]
    public class LanguageSetting : ConfigurationItemBase
    {
        /// <summary>
        /// The playerrefs key.
        /// </summary>
        private const string PREFS_KEY = "LanguageSetting";

        /// <summary>
        /// The option texts.
        /// </summary>
        private readonly List<UIText> contents = new List<UIText>
        {
            UIText.ENGLISH,
            UIText.CHINESE,
        };

        /// <summary>
        /// The default value.
        /// </summary>
        [SerializeField]
        private Language defaultValue = Language.ENGLISH;

        /// <summary>
        /// The current value.
        /// </summary>
        private Language currentValue;

        /// <summary>
        /// Gets the contents.
        /// </summary>
        public override List<UIText> Contents => this.contents;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public override UIText Value
        {
            get
            {
                return (UIText)Enum.Parse(typeof(UIText), this.currentValue.ToString());
            }
            set
            {
                this.contents.FindIndex(c => c == value);
                this.currentValue = (Language)Enum.Parse(typeof(Language), value.ToString());
                LanguageManager.inst.OnChangeLanguageHandler();
            }
        }

        /// <summary>
        /// Executes when enable the ScriptableObject.
        /// </summary>
        private void OnEnable()
        {
            this.currentValue = (Language)Enum.Parse(typeof(Language), PlayerPrefs.GetString(PREFS_KEY, this.defaultValue.ToString()));
        }

        /// <summary>
        /// Executes when disable the ScriptableObject.
        /// </summary>
        private void OnDisable()
        {
            PlayerPrefs.SetString(PREFS_KEY, this.currentValue.ToString());
            PlayerPrefs.Save();
        }
    }
}