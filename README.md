# WPF + MVVM 80分セミナー構成（要約）

| 時間 | セクション | 内容 |
|---:|---|---|
| 5分 | オープニング：WPFとMVVMへの導入 | 1. 今日のゴール：MVVMの「勘所」を短時間で掴む。<br>2. 従来のWPF開発の課題：Code-Behindにビジネスロジックを書きがち → テスト困難・UI変更に弱い。 |
| 10分 | MVVMの概念と誕生の背景 | 1. MVVMとは（目的：関心の分離） — Model / View / ViewModel の役割。<br>2. 最大のメリット：デザイナーと開発の並行作業、単体テストの容易さ（ViewModelの分離）。 |
| 15分 | MVVMの三種の神器：役割と関係 | 1. View (XAML)：見た目と操作の定義（ロジックは書かない）。<br>2. Model (C#)：データとビジネスロジック（UI非依存）。<br>3. ViewModel (C#)：公開プロパティとコマンド、INotifyPropertyChanged の重要性。 |
| 20分 | MVVMの核：データバインディングとコマンド | 1. データバインディング基礎：DataContext の設定、Mode（OneWay / TwoWay）。<br>2. ユーザー操作の処理：ICommand による伝達、RelayCommand（DelegateCommand）パターン紹介。 |
| 20分 | 実践的トピックとフレームワーク | 1. 導入方法：既存プロジェクトへ段階的に適用する手順。<br>2. フレームワーク紹介：Prism、Community Toolkit MVVM（推奨）、ReactiveProperty の比較。 |
| 10分 | NPOCOの導入 | DB操作の簡略化 |
| 10分 | まとめとQ&A | 1. 振り返り：分離・テスト・バインディングの重要性。<br>2. 次のステップ：フレームワーク導入の推奨。<br>3. 質疑応答。 |
| 合計 |  | 80分 |

