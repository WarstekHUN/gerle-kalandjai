namespace Gerle_Lib.Controllers
{
    #region SettingsController (osztály) - comment
    /// <summary>
    /// <c>SettingsController</c> osztály a hangbeállításokat kezeli, tárolja (<c>SettingsController.cs</c>).
    /// </summary>
    #endregion
    public static class SettingsController
    {
        #region _MasterVolume (tulajdonság) - comment
        /// <summary>
        /// <c>_MasterVolume</c> tulajdonság a fő hangerő értékét tartalmazza.
        /// </summary>
        #endregion
        private static float _MasterVolume { get; set; } = 1.0f;
        #region MasterVolume (tulajdonság) - comment
        /// <summary>
        /// <c>MasterVolume</c> tulajdonság meghatározza, hogy a beállítandó fő hangerő Minimum értéke: 0, Maximum értéke: 1 lehet.
        /// </summary>
        #endregion
        public static float MasterVolume 
        {
            get => _MasterVolume;
            set => _MasterVolume = Math.Clamp(value, 0f, 1f);
        }
        #region _MusicVolume (tulajdonság) - comment
        /// <summary>
        /// <c>_MusicVolume</c> tulajdonság a zene hangerejének értékét tartalmazza.
        /// </summary>
        #endregion
        private static float _MusicVolume { get; set; } = 1.0f;
        #region MusicVolume (tulajdonság) - comment
        /// <summary>
        /// <c>MusicVolume</c> tulajdonság meghatározza, hogy a beállítandó zene hangerejének Minimum értéke: 0, Maximum értéke: 1 lehet.
        /// </summary>
        #endregion
        public static float MusicVolume
        {
            get => _MusicVolume;
            set => _MusicVolume = Math.Clamp(value, 0f, 1f);
        }
        #region _FXVolume (tulajdonság) - comment
        /// <summary>
        /// <c>FXVolume</c> tulajdonság a hangeffektek hangerejének értékét tartalmazza.
        /// </summary>
        #endregion
        private static float _FxVolume { get; set; } = 1.0f;
        #region FXVolume (tulajdonság) - comment
        /// <summary>
        /// <c>FXVolume</c> tulajdonság meghatározza, hogy a beállítandó zene hangerejének Minimum értéke: 0, Maximum értéke: 1 lehet.
        /// </summary>
        #endregion
        public static float FXVolume
        {
            get => _FxVolume;
            set => _FxVolume = Math.Clamp(value, 0f, 1f);
        }
        #region _DialogueVolume (tulajdonság) - comment
        /// <summary>
        /// <c>DialogueVolume</c> tulajdonság a dialógus hangerejének értékét tartalmazza.
        /// </summary>
        #endregion
        private static float _DialogueVolume { get; set; } = 1.0f;
        #region Dialogue (tulajdonság) - comment
        /// <summary>
        /// <c>DialogueVolume</c> tulajdonság meghatározza, hogy a beállítandó dialógus hangerejének Minimum értéke: 0, Maximum értéke: 1 lehet.
        /// </summary>
        #endregion
        public static float DialogueVolume
        {
            get => _DialogueVolume;
            set => _DialogueVolume = Math.Clamp(value, 0f, 1f);
        }
    }
}
