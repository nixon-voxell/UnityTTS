using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Voxell.Speech.TTS
{
    public partial class TextToSpeech : MonoBehaviour
    {
        public AudioSource audioSource;
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

        private void SpeakTask(string text, float volume)
        {
            CleanText(ref text);
            var inputIDs = TextToSequence(text);
            var fastspeechOutput = FastspeechInference(ref inputIDs);
            var melganOutput = MelganInference(ref fastspeechOutput);

            var sampleLength = melganOutput.GetLength(1);
            var audioSample = new float[sampleLength];

            for (int s = 0; s < sampleLength; s++) 
                audioSample[s] = melganOutput[0, s, 0];

            UniTask.Post(async () =>
            {
                var audioClip = AudioClip.Create("Speak", sampleLength, 1, 22050, false);
                    audioClip.SetData(audioSample, 0);
                audioSource.PlayOneShot(audioClip);

                await UniTask.Delay(sampleLength / 22050);

                Destroy(audioClip);
            });
        }

        public void CleanText(ref string text)
        {
            text = text.ToLower();
            NumberToWords.CleanText(ref text);
        }
    }
}