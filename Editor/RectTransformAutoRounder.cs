using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Kogane.Internal
{
    /// <summary>
    /// シーンを保存する時に RectTransform のパラメータを四捨五入するエディタ拡張
    /// </summary>
    [InitializeOnLoad]
    internal static class RectTransformAutoRounder
    {
        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static RectTransformAutoRounder()
        {
            // シーンが保存された時に呼び出されます
            EditorSceneManager.sceneSaving += ( scene, path ) => OnSave( scene.GetRootGameObjects() );

            // プレハブのシーンが保存された時に呼び出されます
            PrefabStage.prefabSaving += instance => OnSave( instance );
        }

        /// <summary>
        /// シーンやプレハブが保存された時に呼び出されます
        /// </summary>
        private static void OnSave( params GameObject[] rootGameObjects )
        {
            var settings = RectTransformAutoRounderSetting.instance;

            // 経過時間のログを出力しない場合
            if ( !settings.EnabledLog )
            {
                Round( rootGameObjects );
                return;
            }

            // 経過時間のログを出力する場合
            var sw = Stopwatch.StartNew();
            Round( rootGameObjects );
            sw.Stop();
            Debug.LogFormat( settings.LogFormat, sw.Elapsed.TotalSeconds );
        }

        /// <summary>
        /// 保存するシーンの RectTransform のパラメータを四捨五入します
        /// </summary>
        private static void Round( GameObject[] rootGameObjects )
        {
            var rectTransforms = rootGameObjects
                    .SelectMany( x => x.GetComponentsInChildren<RectTransformAutoRounderTarget>( true ) )
                    .SelectMany( x => x.GetComponentsInChildren<RectTransform>( true ) )
                    .Where( x => x.GetComponent<RectTransformAutoRounderIgnore>() == null )
                    .ToArray()
                ;

            if ( rectTransforms.Length <= 0 ) return;

            // 保存時に Undo できるようにすると
            // シーン保存後にも Dirty フラグが付いてしまうため
            // Undo には登録しないようにしています
            //Undo.RecordObjects( rectTransforms, "UniRectTransformAutoRounder" );

            foreach ( var t in rectTransforms )
            {
                var gameObject             = t.gameObject;
                var isPartOfPrefabInstance = PrefabUtility.IsPartOfPrefabInstance( gameObject );
                var isPrefabInstanceRoot   = PrefabUtility.IsAnyPrefabInstanceRoot( gameObject );

                // 通常のシーンの Hierarchy において
                // プレハブのインスタンスのルートであれば四捨五入の対象
                // プレハブのインスタンスの子であれば対象外
                if ( isPartOfPrefabInstance && !isPrefabInstanceRoot ) continue;

                var anchoredPosition3D = t.anchoredPosition3D;
                anchoredPosition3D.x = Mathf.Round( anchoredPosition3D.x );
                anchoredPosition3D.y = Mathf.Round( anchoredPosition3D.y );
                anchoredPosition3D.z = Mathf.Round( anchoredPosition3D.z );
                t.anchoredPosition3D = anchoredPosition3D;

                var sizeDelta = t.sizeDelta;
                sizeDelta.x = Mathf.Round( sizeDelta.x );
                sizeDelta.y = Mathf.Round( sizeDelta.y );
                t.sizeDelta = sizeDelta;

                // offsetMin や offsetMax を変更すると
                // sizeDelta の値が 1 ズレてしまうことがあるため
                // offsetMin や offsetMax は変更しないようにしています
                // var offsetMin = t.offsetMin;
                // offsetMin.x = Mathf.Round( offsetMin.x );
                // offsetMin.y = Mathf.Round( offsetMin.y );
                // t.offsetMin = offsetMin;
                //
                // var offsetMax = t.offsetMax;
                // offsetMax.x = Mathf.Round( offsetMax.x );
                // offsetMax.y = Mathf.Round( offsetMax.y );
                // t.offsetMax = offsetMax;

                var localScale = t.localScale;
                localScale.x = Mathf.Round( localScale.x );
                localScale.y = Mathf.Round( localScale.y );
                localScale.z = Mathf.Round( localScale.z );
                t.localScale = localScale;
            }
        }
    }
}