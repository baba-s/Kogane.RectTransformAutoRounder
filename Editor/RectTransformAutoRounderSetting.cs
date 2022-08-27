using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// Preferences における設定を管理する ScriptableObject
    /// </summary>
    [FilePath( "UserSettings/Kogane/RectTransformAutoRounder.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class RectTransformAutoRounderSetting : ScriptableSingleton<RectTransformAutoRounderSetting>
    {
        //================================================================================
        // 定数
        //================================================================================
        private static readonly string PACKAGE_NAME        = "Kogane RectTransform Auto Rounder";
        private static readonly bool   DEFAULT_ENABLED_LOG = false;
        private static readonly string DEFAULT_LOG_FORMAT  = $"[{PACKAGE_NAME}] シーンの保存にかかった時間：{{0}} 秒";

        //================================================================================
        // 変数(SerializeField)
        //================================================================================
        [SerializeField] private bool   m_enabledLog = DEFAULT_ENABLED_LOG;
        [SerializeField] private string m_logFormat  = DEFAULT_LOG_FORMAT;

        //================================================================================
        // プロパティ
        //================================================================================
        public bool   EnabledLog => m_enabledLog;
        public string LogFormat  => m_logFormat;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 保存します
        /// </summary>
        public void Save()
        {
            Save( true );
        }
    }
}