using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace Voxell.Speech.TTS
{
    public partial class TextToSpeech : MonoBehaviour
    {
        public AudioSource audioSource;
        public Logger logger;

        private int sampleLength;
        private float[] _audioSample;
        private AudioClip _audioClip;

        private bool _playAudio = false;

        void Start()
        {
            InitTTSProcessor();
            InitTTSInference();
        }

        void Update()
        {
            if (_playAudio)
            {
                _audioClip = AudioClip.Create("Speak", sampleLength, 1, 22050, false);
                _audioClip.SetData(_audioSample, 0);
                audioSource.PlayOneShot(_audioClip);
                _playAudio = false;
            }
        }

        void OnDestroy()
        {
            Dispose();
        }

        public Task Speak(string text) => Task.Run(() => SpeakTask(text));

        private void SpeakTask(string text)
        {
            CleanText(ref text);
            var inputIDs = TextToSequence(text);
            var fastspeechOutput = FastspeechInference(ref inputIDs);
            var melganOutput = MelganInference(ref fastspeechOutput);

            sampleLength = melganOutput.GetLength(1);
            _audioSample = new float[sampleLength];

            for (int s = 0; s < sampleLength; s++) 
                _audioSample[s] = melganOutput[0, s, 0];

            _playAudio = true;
        }

        public void CleanText(ref string text)
        {
            text = text.ToLower();
            NumberToWords.CleanText(ref text);
        }
    }
}