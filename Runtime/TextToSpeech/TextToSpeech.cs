/*
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.

The Original Code is Copyright (C) 2020 Voxell Technologies.
All rights reserved.
*/

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

    void OnDisable()
    {
      _speakThread?.Abort();
    }

    public void Speak(string text)
    {
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