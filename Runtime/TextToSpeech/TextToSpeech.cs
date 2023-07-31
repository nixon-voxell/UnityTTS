using UnityEngine;
using System.Threading;

namespace Voxell.Speech.TTS
{
  public partial class TextToSpeech : MonoBehaviour
  {
    public AudioSource audioSource;
    public Logger logger;

    private int sampleLength;
    private float[] _audioSample;
    private AudioClip _audioClip;
    private Thread _speakThread;
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
      _speakThread?.Abort();
      Dispose();
    }

    public void Speak(string text)
    {
      _speakThread?.Abort();
      _speakThread = new Thread(new ParameterizedThreadStart(SpeakTask));
      _speakThread.Start(text);
    }

    private void SpeakTask(object inputText)
    {
      string text = inputText as string;
      CleanText(ref text);
      int[] inputIDs = TextToSequence(text);
      float[,,] fastspeechOutput = FastspeechInference(ref inputIDs);
      float[,,] melganOutput = MelganInference(ref fastspeechOutput);

      sampleLength = melganOutput.GetLength(1);
      _audioSample = new float[sampleLength];
      for (int s=0; s < sampleLength; s++) _audioSample[s] = melganOutput[0, s, 0];
      _playAudio = true;
    }

    public void CleanText(ref string text)
    {
      text = text.ToLower();
      // TODO: also convert numbers to words using the NumberToWords class
    }
  }
}