namespace Gerle_Lib.Controllers
{
    public static class SettingsController
    {
        private static float _MasterVolume { get; set; } = 1.0f;
        public static float MasterVolume 
        {
            get => _MasterVolume;
            set => _MasterVolume = Math.Clamp(value, 0f, 1f);
        }
        private static float _MusicVolume { get; set; } = 1.0f;
        public static float MusicVolume
        {
            get => _MusicVolume;
            set => _MusicVolume = Math.Clamp(value, 0f, 1f);
        }
        private static float _FxVolume { get; set; } = 1.0f;
        public static float FXVolume
        {
            get => _FxVolume;
            set => _FxVolume = Math.Clamp(value, 0f, 1f);
        }
        
        private static float _DialogueVolume { get; set; } = 1.0f;
        public static float DialogueVolume
        {
            get => _DialogueVolume;
            set => _DialogueVolume = Math.Clamp(value, 0f, 1f);
        }
    }
}
