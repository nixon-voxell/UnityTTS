using TensorFlowLite;
using System;
using UnityEngine;

namespace Voxell.Speech.TTS
{
  using Inspector;

  public partial class TextToSpeech : System.IDisposable
  {
    [StreamingAssetFilePath] public string fastspeech;
    [StreamingAssetFilePath] public string melgan;
    [Tooltip("The ID of the speaker, currently will not affect the speech yet.")]
    public int speakerID = 1;
    [Range(0.0f, 1.0f), Tooltip("The lower, the faster the speed!")]
    public float speedRatio = 1.0f;

    private Interpreter _fastspeechInterpreter;
    private Interpreter _melganInterpreter;
    private InterpreterOptions _options;

    /// <summary>
    /// Create Fastspeech and Melgan interpreters
    /// </summary>
    void InitTTSInference()
    {
      _options = new InterpreterOptions() {threads = 4};
      _fastspeechInterpreter = new Interpreter(FileUtilx.ReadStreamingAssetFileByte(fastspeech), _options);
      _melganInterpreter = new Interpreter(FileUtilx.ReadStreamingAssetFileByte(melgan), _options);
    }

    #region Inferencing
    /// <summary>
    /// Formats inputIDs, speakerID and speedRatio into arrays that is to be used as _fastspeechInterpreter input tensors.
    /// </summary>
    /// <param name="inputIDs">input token ids translated from text string letter by letter</param>
    /// <param name="speakerID">the id of the speaker that we wish to use</param>
    /// <param name="speedRatio">the speed of the output speech</param>
    /// <returns>An array of all input data.</returns>
    private Array[] PrepareInput(ref int[] inputIDs, ref int speakerID, ref float speedRatio)
    {
      Array[] inputData = new Array[3];

      int[,] formatedInputIDS = new int[1, inputIDs.Length];
      for (int i=0; i < inputIDs.Length; i++) formatedInputIDS[0, i] = inputIDs[i];
      speedRatio = Mathf.Clamp(speedRatio, 0.0f, 1.0f);
      inputData[0] = formatedInputIDS;
      inputData[1] = new int[1]{speakerID};
      inputData[2] = new float[1]{speedRatio};

      return inputData;
    }

    /// <summary>
    /// Inferencing fastspeech tflite model by taking in text and converting them into spectogram
    /// </summary>
    /// <param name="text">input text</param>
    private float[,,] FastspeechInference(ref int[] inputIDs)
    {
      _fastspeechInterpreter.ResizeInputTensor(0, new int[2]{1, inputIDs.Length});
      _fastspeechInterpreter.ResizeInputTensor(1, new int[1]{1});
      _fastspeechInterpreter.ResizeInputTensor(2, new int[1]{1});

      _fastspeechInterpreter.AllocateTensors();
      Array[] inputData = PrepareInput(ref inputIDs, ref speakerID, ref speedRatio);
      for (int d=0; d < inputData.Length; d++)
        _fastspeechInterpreter.SetInputTensorData(d, inputData[d]);

      _fastspeechInterpreter.Invoke();

      int[] outputShape = _fastspeechInterpreter.GetOutputTensorInfo(1).shape;
      float[,,] outputData = new float[outputShape[0], outputShape[1], outputShape[2]];
      _fastspeechInterpreter.GetOutputTensorData(1, outputData);
      return outputData;
    }
    
    /// <summary>
    /// Inferencing melgan tflite model by converting spectogram to audio
    /// </summary>
    /// <param name="spectogram">input spectogram</param>
    /// <returns></returns>
    private float[,,] MelganInference(ref float[,,] spectogram)
    {
      _melganInterpreter.ResizeInputTensor(0, new int[3]{
        spectogram.GetLength(0),
        spectogram.GetLength(1),
        spectogram.GetLength(2)});

      _melganInterpreter.AllocateTensors();
      _melganInterpreter.SetInputTensorData(0, spectogram);

      _melganInterpreter.Invoke();

      int[] outputShape = _melganInterpreter.GetOutputTensorInfo(0).shape;
      float[,,] outputData = new float[outputShape[0], outputShape[1], outputShape[2]];
      _melganInterpreter.GetOutputTensorData(0, outputData);
      return outputData;
    }
    #endregion

    public void Dispose()
    {
      // null check before disposing to prevent unity crashing
      _fastspeechInterpreter?.Dispose();
      _melganInterpreter?.Dispose();
      _options?.Dispose();
    }
  }
}