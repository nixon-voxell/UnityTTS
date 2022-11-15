using TMPro;
using UnityEngine;

namespace Voxell.Speech.TTS.Demo
{
  public class TTSDemo : MonoBehaviour
  {
    public TMP_InputField inputField;
    public TextToSpeech textToSpeech;

    public void SpeakInput() => textToSpeech.Speak(inputField.text);

    void OnDisable()
      => textToSpeech.Dispose();
  }
}