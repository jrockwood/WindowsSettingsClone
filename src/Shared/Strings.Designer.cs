﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WindowsSettingsClone.Shared.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal error: {0}.
        /// </summary>
        internal static string InternalError {
            get {
                return ResourceManager.GetString("InternalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing required value &apos;{0}&apos; from message..
        /// </summary>
        internal static string MissingRequiredMessageValue {
            get {
                return ResourceManager.GetString("MissingRequiredMessageValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error in reading registry value &apos;{0}\{1}&apos;: {2}..
        /// </summary>
        internal static string RegistryReadError {
            get {
                return ResourceManager.GetString("RegistryReadError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error in writing registry value &apos;{0}\{1}&apos;: {2}..
        /// </summary>
        internal static string RegistryWriteError {
            get {
                return ResourceManager.GetString("RegistryWriteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message value &apos;{0}&apos; has an incorrect type &apos;{1}&apos;. Expecting type &apos;{2}&apos;..
        /// </summary>
        internal static string WrongMessageValueType {
            get {
                return ResourceManager.GetString("WrongMessageValueType", resourceCulture);
            }
        }
    }
}
