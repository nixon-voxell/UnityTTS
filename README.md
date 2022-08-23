# Text to Speech In Unity

> Important! The current model (fastspeech) does not work well with short phrases. (e.g. "hi", "how are you", etc.)

This package provides a fully functional cross platform Text To Speech engine using deep learning models integrated in Unity with C#!

You can find the example repository [here](https://github.com/nixon-voxell/UnityTTSExamples).

- [Text to Speech In Unity](#text-to-speech-in-unity)
  - [Text To Speech](#text-to-speech)
  - [Installation](#installation)
  - [Support the project!](#support-the-project)
  - [Join the community!](#join-the-community)
  - [License](#license)
  - [References](#references)

## Text To Speech

The model that we use for TTS is FastSpeech. The TFLite model that we used is converted from a pre-trained model found in the [TensorflowTTS repository](https://github.com/TensorSpeech/TensorFlowTTS).

To prevent Unity from freezing when inferencing the TFLite model, we run the inference process in a new thread and play the audio in the main thread once it is ready.

## Installation

External dependencies:

- voxell.util ([UnityUtil](https://github.com/voxell-tech/UnityUtil))
- com.github.asus4.tflite ([UnityTFLite](https://github.com/asus4/tf-lite-unity-sample/tree/master/Packages/com.github.asus4.tflite))

1. Clone the [UnityUtil](https://github.com/voxell-tech/UnityUtil) repository into your `Packages` folder.
2. Add TFLite package via "add package from git URL" selection in the package manager using this link: https://github.com/asus4/tf-lite-unity-sample.git?path=/Packages/com.github.asus4.tflite
3. Clone this repository into your `Packages` folder.
4. Download the TFLite models from [Google Drive](https://drive.google.com/drive/u/0/folders/1--j-eDXKdtDcm5-Z7bnSTeVFWnKoHOER) and import them into Unity (place them inside the `Assets/StreamingAssets` folder).
5. And you are ready to go!

## Support the project!

<a href="https://www.patreon.com/voxelltech" target="_blank">
  <img src="https://teaprincesschronicles.files.wordpress.com/2020/03/support-me-on-patreon.png" alt="patreon" width="200px" height="56px"/>
</a>

<a href ="https://ko-fi.com/voxelltech" target="_blank">
  <img src="https://uploads-ssl.webflow.com/5c14e387dab576fe667689cf/5cbed8a4cf61eceb26012821_SupportMe_red.png" alt="kofi" width="200px" height="40px"/>
</a>

## Join the community!

<a href ="https://discord.gg/Mhnyp6VYEQ" target="_blank">
  <img src="https://gist.githubusercontent.com/nixon-voxell/e7ba303906080ffdf65b106f684801b5/raw/97c6dfce3459c0a2c2ea8e1b9593612346f3abfc/JoinVXDiscord.svg" alt="discord" width="200px" height="200px"/>
</a>

<a href ="https://discord.gg/X3ZucbxXFc" target="_blank">
  <img src="https://gist.githubusercontent.com/nixon-voxell/e7ba303906080ffdf65b106f684801b5/raw/97c6dfce3459c0a2c2ea8e1b9593612346f3abfc/JoinVXGithubDiscord.svg" alt="discord" width="200px" height="200px"/>
</a>

## License

This repository as a whole is licensed under the GNU Public License, Version 3. Individual files may have a different, but compatible license.

See [license file](./LICENSE) for details.

## References

1. [Fastspeech](https://arxiv.org/abs/1905.09263)
2. [MelGAN](https://arxiv.org/abs/1910.06711)
3. All TFLite model inferencing will not be possible without the help of the [Unity TFLite](https://github.com/asus4/tf-lite-unity-sample) repository.