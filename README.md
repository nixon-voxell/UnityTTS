# End to end Speech Engine In Unity

## What does this package contatins??

This package provides a fully functional cross platform end to end speech engine (Text To Speech and Speech To Text) using deep learning models intergrated in Unity with C#!!! Anyone can use this package in any way they want as long as they credit the author(s) and also respect the [license](LICENSE) agreement.

### Text To Speech

The model that we use for TTS is FastSpeech. The TFLite model that we used is converted from a pre-trained model found in the [TensorflowTTS repository](https://github.com/TensorSpeech/TensorFlowTTS).

To prevent Unity from freezing when inferencing the TFLite model, we run the inference process in a new thread and play the audio in the main thread once it is ready.

### Speech To Text

In progress...

## How to use?

All TFLite model inferencing will not be possible without the help of the [Unity TFLite](https://github.com/asus4/tf-lite-unity-sample) repository.

1. Clone the repository mentioned above.
2. Go into the Packages folder and copy the `com.github.asus4.tflite` folder into your project's Packages folder.
3. Clone this repository and the [Core Repository](https://github.com/voxell-tech/smartassistant.core) into your project's Packages folder.
4. And you are ready to go!

## Support the project!

<a href="https://www.patreon.com/voxelltech" target="_blank">
  <img src="https://teaprincesschronicles.files.wordpress.com/2020/03/support-me-on-patreon.png" alt="patreon" width="200px" height="55px"/>
</a>

<a href ="https://ko-fi.com/voxelltech" target="_blank">
  <img src="https://uploads-ssl.webflow.com/5c14e387dab576fe667689cf/5cbed8a4cf61eceb26012821_SupportMe_red.png" alt="kofi" width="200px" height="40px"/>
</a>

## License

This repository as a whole is licensed under the GNU Public License, Version 3. Individual files may have a different, but compatible license.

See [license file](./LICENSE) for details.