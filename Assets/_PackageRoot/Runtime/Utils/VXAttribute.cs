using UnityEngine;
using System;

namespace Voxell.Inspector
{
    public class InspectOnlyAttribute : PropertyAttribute { }
    public class SceneAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ButtonAttribute : Attribute
    {
        public string buttonName { get; private set; }
        public ButtonAttribute(string buttonName = "") => this.buttonName = buttonName;
    }

    public class StreamingAssetFilePathAttribute : PropertyAttribute { }
    public class StreamingAssetFolderPathAttribute : PropertyAttribute { }
}