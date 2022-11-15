using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Voxell.Speech.TTS
{
    public partial class TextToSpeech : MonoBehaviour
    {
        public AudioSource audioSourceRef;
        public Logger logger;

        void Start()
        {
            InitTTSProcessor();
            InitTTSInference();
        }

        void OnDestroy()
        {
            Dispose();
        }

        public Task Speak(string text, float volume = 1f) => Task.Run(() => SpeakTask(text, volume));

        private async Task SpeakTask(string text, float volume)
        {
            CleanText(ref text);
            var inputIDs = TextToSequence(text);
            var fastspeechOutput = FastspeechInference(ref inputIDs);
            var melganOutput = MelganInference(ref fastspeechOutput);

            var sampleLength = melganOutput.GetLength(1);
            var audioSample = new float[sampleLength];

            for (int s = 0; s < sampleLength; s++) 
                audioSample[s] = melganOutput[0, s, 0];

            volume = Mathf.Min(0f, Mathf.Max(1f, volume));
            var completed = false;
            UniTask.Post(async () =>
            {
                var audioClip = AudioClip.Create("Speak", sampleLength, 1, 22050, false);
                    audioClip.SetData(audioSample, 0);

                var audioSource = GameObject.Instantiate(audioSourceRef, transform);
                audioSource.volume = volume;
                audioSource.PlayOneShot(audioClip);

                await UniTask.Delay(sampleLength / 22050);

                Destroy(audioClip);
                Destroy(audioSource);

                completed = true;
            });
            while (!completed)
                await Task.Yield();
        }

        public void CleanText(ref string text)
        {
            text = text.ToLower();
            NumberToWords.CleanText(ref text);
        }
    }
}