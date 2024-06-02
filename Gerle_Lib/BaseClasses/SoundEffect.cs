using NAudio.Wave;

namespace Gerle_Lib.BaseClasses
{
    public class SoundEffect
    {
        private Mp3FileReader FileReader { get; init; }
        private WaveOutEvent Player { get; init; }
        private Task? PlayingTask { get; set; }

        public SoundEffect(string path)
        {
            FileReader = new Mp3FileReader(path);
            Player = new WaveOutEvent();
            Player.Init(FileReader);

            Player.PlaybackStopped += Player_PlaybackStopped;
        }

        private void Player_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if(PlayingTask is not null)
            {
                PlayingTask.Dispose();
            }
        }

        public void Play()
        {
            PlayingTask = Task.Run(() =>
            {
                Player.Play();
            });
        }
    }
}
