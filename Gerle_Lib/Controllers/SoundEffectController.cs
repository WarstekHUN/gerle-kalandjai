using Gerle_Lib.BaseClasses;

namespace Gerle_Lib.Controllers
{
    public static class EnumExtender
    {
        /// <summary>
        /// Visszaadja az adott ENUM érték nevét
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetName(this Enum en)
        {
            return en.GetType().Name;
        }

        public static T GetEnumByName<T>(string name) where T : struct => Enum.Parse<T>(name);
    }

    public static class SoundEffectController
    {
        public enum SoundEffects
        {
            DealDamage,
            ReceiveDamage,
            RecieveHeal,
            LoseGame,
            ChoseAction,
            ChoseStory
        }

        private static bool HasLoaded = false;

        private static Dictionary<SoundEffects, List<SoundEffect>> effects = new Dictionary<SoundEffects, List<SoundEffect>>();

        /// <summary>
        /// Betölti RAM-ba az összes hangeffektet, hogy ne késlekedjen a játék, amikor le kell játszani.
        /// Egy nagyobb játéknál ez próblémát okozna, mert sok lenne az effekt, de jelen esetben nem eszik meg túl sok memóriát.
        /// </summary>
        public static void LoadEffects()
        {
            if (HasLoaded) return;

            IEnumerable<IGrouping<string, string>> files = Directory.GetFiles("Data/Audio/Effects").GroupBy(el => el.Split('_')[0]);

            foreach (IGrouping<string, string> fileName in files)
            {
                List<SoundEffect> current = new List<SoundEffect>();

                foreach (string fileVariant in fileName)
                {
                    current.Add(new SoundEffect(fileVariant));
                }

               effects.Add(EnumExtender.GetEnumByName<SoundEffects>(Path.GetFileNameWithoutExtension(fileName.Key)!.Split('_')[0]), current);
            }

            HasLoaded = true;
        }

        /// <summary>
        /// Lejátszik egy véletlenszerűen kiválasztott változatott a megadott hangeffektből.
        /// </summary>
        /// <param name="effect"></param>
        public static void PlayEffect(SoundEffects effect)
        {
            List<SoundEffect> pool = effects[effect];
            pool[Random.Shared.Next(0, pool.Count)].Play();
        }
    }
}
