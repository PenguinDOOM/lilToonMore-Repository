# SGMB (Single-pass Gaussian-weighted Multi-ring Blur)

## 概要

**SGMB**は、VRChatのレンダリング制約に最適化された特殊なブラー実装です。**Single-pass Gaussian-weighted Multi-ring Blur（シングルパス・ガウス重み付き・マルチリングブラー）**の略称です。

### 主な特徴

- **Kawase Blurからインスパイア**: Kawase Blur（川瀬正樹、2003年）の対角サンプリング手法を採用
- **VRChat最適化**: VRChatのGrabPass制約内で動作するシングルパスレンダリング専用設計
- **複数の品質レベル**: 超軽量なBilinearモードから高品質なUltraモードまで
- **物理的正確性**: lilToonのオリジナル実装のガウス重み付けを使用し、物理的に正確なブラー分布を実現

## 技術的な背景

### 本物のKawase Blurとの違い

| 観点 | Kawase Blur | SGMB |
|------|-------------|------|
| パス数 | 複数（Pass1 → Pass2 → Pass3...） | シングルパス |
| ターゲット | 汎用 | VRChat（GrabPass制約） |
| サンプリング | 段階的ダウンサンプリング | 直接マルチリングサンプリング |
| 最適化 | カスケードブラー | バイリニア補間 + ガウス重み |

**なぜシングルパス？**  
VRChatのGrabPassはシェーダーごとに1回しか呼び出せないため、従来のKawase Blurのようなマルチパス技術は不可能です。SGMBは賢いシングルパスサンプリングにより同等の品質を実現します。

### バイリニア補間の利点

各`tex2D`呼び出しはGPUのバイリニア補間を活用し、実質的に一度に4ピクセルをサンプリングします：
- **1タップ** = 補間された4ピクセル
- **実質サンプル数** = タップ数 × 4

### ガウス重み付け

ガウス関数を使用した物理的なブラー分布（lilToonのオリジナル実装から流用）：

```hlsl
sigmaSq = (LIL_REFRACTION_SAMPNUM * LIL_REFRACTION_SAMPNUM) / 2.0
weight = exp(-distance * distance / sigmaSq)
```

`LIL_REFRACTION_SAMPNUM = 8`の場合

## 品質レベル

| 品質 | タップ数 | 実質サンプル | カバー範囲 | 速度倍率 | ガウス重み付け | 推奨用途 |
|------|---------|------------|-----------|---------|----------------|----------|
| **Bilinear** | 4 | 16px | 4×4px | 24倍 | ❌ なし | Quest最軽量 |
| **Low** | 8 | 32px | 10×7px | 11.8倍 | ✓ あり | PC VR推奨 |
| **Mid** | 13 | 52px | 16×10px | 4.6倍 | ✓ あり | PC Desktop推奨 |
| **High** | 17 | 68px | 16×10px（密） | 3.7倍 | ✓ あり | 高性能PC |
| **Ultra** | 25 | 100px | 21×13px | 2.5倍 | ✓ あり | スクリーンショット用 |
| **Original** | 33 | 132px | 2×50px（垂直のみ） | 1.0倍（基準） | ✓ あり | 互換性 |

**注記**: Bilinearモードは最大のパフォーマンスのためガウス重み付けを使用せず、シンプルな平均を使用します。その他の品質レベル（LowからOriginalまで）は全て、物理的に正確なブラー分布のためガウス重み付きサンプリングを使用します。

## パフォーマンス比較

### GPU負荷（相対値）

1080pでの理論的サイクル計算に基づく：

```
Original: 461サイクル (100%) - 基準
Bilinear:  19サイクル (4.1%) - 24倍高速
Low:       39サイクル (8.5%) - 11.8倍高速
Mid:      101サイクル (22%)  - 4.6倍高速
High:     125サイクル (27%)  - 3.7倍高速
Ultra:    181サイクル (39%)  - 2.5倍高速
```

### 解像度別フレームタイム影響（推定）

**1080p (1920×1080)**
- Original: 約0.25ms
- Bilinear: 約0.01ms
- Low: 約0.02ms
- Mid: 約0.05ms
- High: 約0.07ms
- Ultra: 約0.10ms

**Quest 2 (片目あたり1832×1920)**
- Original: 片目約0.48ms
- Bilinear: 片目約0.02ms
- Low: 片目約0.04ms
- Mid: 片目約0.10ms

**4K (3840×2160)**
- Original: 約1.0ms
- Bilinear: 約0.04ms
- Low: 約0.08ms
- Mid: 約0.20ms
- High: 約0.27ms
- Ultra: 約0.40ms

### メモリ帯域幅削減

1080p全画面での例：
- **Original**: 33タップ × 1920×1080 × 4バイト = 約274 MB
- **Low**: 8タップ × 1920×1080 × 4バイト = 約66 MB
- **Bilinear**: 4タップ × 1920×1080 × 4バイト = 約33 MB

**削減された帯域幅**: Bilinearモードで最大88%

## サンプリングパターン

### Bilinear（4タップ）
```
    X       X
    
    
    X       X
```
バイリニア補間付き対角4点  
**ガウス重み付けなし** - 最大パフォーマンスのためシンプルな平均を使用

### Low（8タップ）
```
    X   +   X       リング1: 対角（4）
    
+       C       +   リング2: 十字（4）
    
    X   +   X
```
**ガウス重み付きあり** - 物理的に正確なブラー分布

### Mid（13タップ）
```
    X   +   X       リング1: 対角（4）
    
+       C       +   リング2: 十字（4）
    
    X   +   X       リング3: 対角（4）
    
            +
```

### High（17タップ）
```
        X           リング1: 対角（4）
    X   +   X       リング2: 対角（4）
  +     C     +     リング3: 十字 + 対角（8）
    X   +   X
        X
```

### Ultra（25タップ）
```
            X           リング1: 対角（4）
        X   +   X       リング2: 十字 + 対角（8）
    X   +   C   +   X   リング3: 十字 + 対角（8）
        X   +   X       リング4: 対角（4）
            X
```

**注記**: リング距離は基本ブラーオフセットの0.5倍、1.5倍、2.5倍、3.5倍です。

## 使い方

### シェーダープロパティ

ブラー品質はシェーダープロパティで制御できます：

```shaderlab
[Enum(Original,0,Bilinear,1,Low,2,Mid,3,High,4,Ultra,5)]
_RefractionBlurType ("Blur Quality", Int) = 2
```

### C#での制御

```csharp
// 品質レベルの設定
material.SetInt("_RefractionType", 1);        // SGMBモード有効化
material.SetInt("_RefractionSGMBQuality", 0); // Low
material.SetInt("_RefractionSGMBQuality", 1); // Mid
material.SetInt("_RefractionSGMBQuality", 2); // High
material.SetInt("_RefractionSGMBQuality", 3); // Ultra

// Bilinearモード
material.SetInt("_RefractionType", 2);        // Bilinearモード

// 動的品質調整の例
float fps = 1.0f / Time.deltaTime;
if (fps < 60)
    material.SetInt("_RefractionType", 2);    // Bilinearに切り替え
else if (fps < 90)
    material.SetInt("_RefractionSGMBQuality", 0); // Low品質
else
    material.SetInt("_RefractionSGMBQuality", 1); // Mid品質
```

## プラットフォーム別推奨設定

```
PC Desktop:     Mid (13タップ)     - 品質とパフォーマンスの最良バランス
PC VR:          Low (8タップ)      - VRパフォーマンス最適化
Quest:          N/A                - カスタムシェーダー非対応
モバイル:       Bilinear (4タップ) - 最大パフォーマンス
```

### VRChatでの考慮事項

- **VRChat PC**: アバター複雑度に応じてMidまたはLow推奨
- **VRChat VR**: 安定した90fps維持のためLow推奨
- **Quest**: カスタムシェーダー制約によりSGMB利用不可

## 技術詳細

### バイリニア補間の説明

ピクセル間でサンプリングする際、GPUは自動的に4つの隣接ピクセルを補間します：

```
[P1] --- [P2]    中央でのサンプル = P1, P2, P3, P4の
  |   *   |      重み付き平均
[P3] --- [P4]    * = サンプル点
```

つまり：
- **1回のテクスチャフェッチ** = ブレンドされた4ピクセル
- **4タップ** = 実質16サンプル
- **8タップ** = 実質32サンプル

### ガウス重みの計算

ガウス重み付けの式は、lilToonのオリジナル屈折ブラー実装から流用しています：

```hlsl
// シグマの二乗の計算
float sigmaSq = (LIL_REFRACTION_SAMPNUM * LIL_REFRACTION_SAMPNUM) / 2.0;
// LIL_REFRACTION_SAMPNUM = 8の場合:
// sigmaSq = 64 / 2 = 32

// 各サンプルの重み
float weight = exp(-distance * distance / sigmaSq);

// 重みの例:
// distance 0.5 → weight ≈ 0.992
// distance 1.5 → weight ≈ 0.930
// distance 2.5 → weight ≈ 0.726
// distance 3.5 → weight ≈ 0.467
```

**注記**: ガウス重み付けはLow、Mid、High、Ultra、Originalの品質モードで使用されます。Bilinearモードはガウス重み付けを使用せず、最大のパフォーマンスのためシンプルな平均（タップ数で割る）を使用します。

### BilinearとGaussian重み付きモードの比較

**Bilinearモード（ガウス重み付けなし）**
- シンプルな平均を使用: `refractCol / 4.0`
- 全てのサンプルが等しく重み付け
- 最速のパフォーマンス（Originalより24倍高速）
- 最適用途: Quest、モバイル、またはパフォーマンス重視のシナリオ

**Low/Mid/High/Ultraモード（ガウス重み付きあり）**
- 重み付き平均を使用: `refractCol / sum` (sumは全ての重みの合計)
- 中心に近いサンプルほど高い重み
- 物理的に正確なブラー分布
- 最適用途: 品質が重要なPC DesktopとVR

### OriginalとSGMBの比較

**Original（33タップ垂直ガウシアン）**
- サンプル: 垂直軸のみに沿った33タップ
- パターン: 線形（Y方向に-16から+16）
- カバー範囲: 2×50ピクセル領域
- 用途: 互換性モード

**SGMB（マルチリング）**
- サンプル: 円形/十字パターンで4～25タップ
- パターン: 多方向リング
- カバー範囲: 4×4から21×13ピクセル領域
- 用途: 最適化された現代的実装

## よくある質問（FAQ）

### Q: なぜKawase Blurと呼ばないのですか？

**A:** Kawase Blurの対角サンプリング手法からインスパイアされていますが、SGMBは根本的に異なります：
- Kawaseは段階的マルチパスダウンサンプリングを使用
- SGMBはガウス重み付きシングルパスマルチリングサンプリングを使用
- 異なる数学的基礎と最適化戦略

「Kawase Blur」と呼ぶと、実際の実装について誤解を招きます。

### Q: どの品質を選ぶべきですか？

**A:** 用途によります：
- **スクリーンショット/動画**: Ultra
- **デスクトップPC（60fps）**: Mid
- **VR（90fps目標）**: Low
- **モバイル/Quest単体**: Bilinear
- **パフォーマンス重視**: Bilinear

Midから始めて、パフォーマンス要件に基づいて調整してください。

### Q: Questで使えますか？

**A:** いいえ。VRChat Questはカスタムシェーダーに対応していません。SGMBはPCプラットフォーム（デスクトップおよびPCVR）でのみ利用可能です。

### Q: パフォーマンスへの影響は？

**A:** 1080pでMid品質の場合：
- GPU: 約0.05ms（16.67msフレーム予算の約0.3%）
- メモリ帯域幅: フレームあたり約100 MB
- 現代のGPUでは一般的に無視できるレベル

### Q: Originalブラーより優れていますか？

**A:** ほとんどの場合、はい：
- **優れたパフォーマンス**: 2.5倍から24倍高速
- **優れたカバー範囲**: 多方向 vs 垂直のみ
- **調整可能な品質**: ニーズに合わせた最適なバランス選択
- **Originalの用途**: 古いコンテンツとの正確な互換性のみ

### Q: すべてのマテリアルで動作しますか？

**A:** SGMBはlilToonの屈折機能を使用する任意のマテリアルで動作します。透明/屈折マテリアルの屈折ブラー専用に設計されています。

## クレジット

- **lilToon**: [lilxyzw](https://github.com/lilxyzw/lilToon)
- **Kawase Blur**（インスピレーション）: 川瀬正樹（2003年）- "Frame Buffer Postprocessing Effects in DOUBLE-S.T.E.A.L"
- **SGMB実装**: [PenguinDOOM](https://github.com/PenguinDOOM)

## ライセンス

MITライセンス（lilToon互換）

```
Copyright (c) 2024 PenguinDOOM

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## 参考文献

- [Kawase Blur - "Frame Buffer Postprocessing Effects in DOUBLE-S.T.E.A.L" (GDC 2003)](http://www.daionet.gr.jp/~masa/archives/GDC2003_DSTEAL.ppt)
- [lilToon ドキュメント](https://lilxyzw.github.io/lilToon/)
- [VRChat シェーダー最適化](https://creators.vrchat.com/avatars/avatar-performance-ranking-system/)
