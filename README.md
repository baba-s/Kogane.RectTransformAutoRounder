# Kogane Rect Transform Auto Rounder

シーン保存時に RectTransform の AnchoredPosition3D や SizeDelta を整数に四捨五入するエディタ拡張

## 使い方

![2020-10-07_195416](https://user-images.githubusercontent.com/6134875/95322024-d5ec7880-08d6-11eb-8d09-1627d042b899.png)

RectTransform の AnchoredPosition3D や SizeDelta を整数に四捨五入したいゲームオブジェクトに  
「RectTransformAutoRounderTarget」をアタッチした状態でシーンを保存すると、

![25](https://user-images.githubusercontent.com/6134875/95321939-b6ede680-08d6-11eb-8e24-b6b0100ef817.gif)

「RectTransformAutoRounderTarget」がアタッチされているゲームオブジェクトと  
すべての子オブジェクトの RectTransform の AnchoredPosition3D や SizeDelta を整数に四捨五入されます

![2020-10-07_195638](https://user-images.githubusercontent.com/6134875/95322253-2a8ff380-08d7-11eb-8644-6822b5d27cea.png)

四捨五入の対象外にしたい場合は「RectTransformAutoRounderIgnore」をアタッチします  
