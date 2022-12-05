using System.IO;
using System.Text;
using UnityEngine;

namespace Voxell
{
    public static class FileUtilx
    {
        public static readonly string projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6);

        /// <summary>Get the path of the folder given a file path by excluding the filename</summary>
        /// <param name="assetPath">full file path</param>
        /// <returns>Folder path of the file</returns>
        public static string GetFolderPath(string assetPath)
        {
            string folder = "";
            string[] paths = assetPath.Split(new char[] { '/', '\\' });
            for (int p = 0; p < paths.Length - 1; p++) folder += paths[p] + '/';
            return folder;
        }

        /// <summary>Get the path of the folder given a file path by excluding the filename</summary>
        /// <param name="assetPath">full file path</param>
        /// <param name="separator">separator of each folder in the assetPath</param>
        /// <returns>Folder path of the file</returns>
        public static string GetFolderPath(string assetPath, char[] separator)
        {
            string folder = "";
            string[] paths = assetPath.Split(separator);
            for (int p = 0; p < paths.Length - 1; p++) folder += paths[p] + '/';
            return folder;
        }

        /// <summary>Get the name of the file given the file path by excluding all of its folder paths</summary>
        /// <param name="assetPath">full file path</param>
        /// <returns></returns>
        public static string GetFilename(string assetPath)
        {
            string[] paths = assetPath.Split(new char[] { '/', '\\' });
            return paths[paths.Length - 1];
        }

        /// <summary>Get the name of the file given the file path by excluding all of its folder paths</summary>
        /// <param name="assetPath">full file path</param>
        /// <param name="separator">separator of each folder in the assetPath</param>
        /// <returns></returns>
        public static string GetFilename(string assetPath, char[] separator)
        {
            string[] paths = assetPath.Split(separator);
            return paths[paths.Length - 1];
        }

        /// <summary>Return the full file path given an asset path</summary>
        /// <param name="assetPath">Path that starts from the 'Assets/' folder</param>
        public static string GetAssetFilePath(string assetPath)
          => Path.Combine(projectPath, assetPath);

        /// <summary>Return the full file path given a streaming asset path</summary>
        /// <param name="streamingAssetPath">Path that is in the StreamingAssets/ folder</param>
        public static string GetStreamingAssetFilePath(string streamingAssetPath)
          => Path.Combine(Application.streamingAssetsPath, streamingAssetPath);

        /// <summary>Read a streaming asset file and return raw bytes from the file</summary>
        /// <param name="path">file path starting from and excluding Application.streamingAssetsPath</param>
        /// <returns>Raw bytes from the file</returns>
        public static byte[] ReadStreamingAssetFileByte(string path)
          => File.ReadAllBytes(GetStreamingAssetFilePath(path));

        /// <summary>Read a streaming asset file and return raw string from the file</summary>
        /// <param name="path">file path starting excluding Application.streamingAssetsPath</param>
        /// <returns>Raw string from the file</returns>
        public static string ReadStreamingAssetFileText(string path)
          => File.ReadAllText(GetStreamingAssetFilePath(path));

        /// <summary>Write string contents to a streaming asset file</summary>
        /// <param name="path">file path starting excluding Application.streamingAssetsPath</param>
        /// <returns>Raw string from the file</returns>
        public static void WriteStreamingAssetFileText(string path, string contents)
          => File.WriteAllText(GetStreamingAssetFilePath(path), contents);

        /// <summary>Write string contents to a streaming asset file</summary>
        /// <param name="path">file path starting excluding Application.streamingAssetsPath</param>
        /// <returns>Raw string from the file</returns>
        public static void WriteStreamingAssetFileText(string path, string contents, Encoding encoding)
          => File.WriteAllText(GetStreamingAssetFilePath(path), contents, encoding);

        /// <summary>Write byte contents to a streaming asset file</summary>
        /// <param name="path">file path starting excluding Application.streamingAssetsPath</param>
        /// <returns>Raw string from the file</returns>
        public static void WriteStreamingAssetFileByte(string path, byte[] bytes)
          => File.WriteAllBytes(GetStreamingAssetFilePath(path), bytes);

    }
}
