using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// シーンを保存する時に RectTransform のパラメータを四捨五入したいゲームオブジェクトにアタッチします
	/// すべての子オブジェクトも四捨五入の対象になります
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class RectTransformAutoRounderTarget : MonoBehaviour
	{
	}
}