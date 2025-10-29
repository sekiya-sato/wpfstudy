# WPF + MVVM 80分セミナー構成（要約）

| 時間 | セクション | 内容 |
|---:|---|---|
| 5分 | オープニング：WPFとMVVMへの導入 | 1. 今日のゴール：MVVMの「勘所」を短時間で掴む。<br>2. 従来のWPF開発の課題：Code-Behindにビジネスロジックを書きがち → テスト困難・UI変更に弱い。<br>WPF や Xamarin、.NET MAUI でよく使われる |
| 10分 | MVVMの概念と誕生の背景 | 1. MVVMとは（目的：関心の分離） — Model / View / ViewModel の役割。<br>2. 最大のメリット：デザイナーと開発の並行作業、単体テストの容易さ（ViewModelの分離）。 |
| 15分 | MVVMの三種の神器：役割と関係 | 1. View (XAML)：見た目と操作の定義（ロジックは書かない）。<br>2. Model (C#)：データとビジネスロジック（UI非依存）。<br>3. ViewModel (C#)：公開プロパティとコマンド、INotifyPropertyChanged の重要性。 |
| 20分 | MVVMの核：データバインディングとコマンド | 1. データバインディング基礎：DataContext の設定、Mode（OneWay / TwoWay）。<br>2. ユーザー操作の処理：ICommand による伝達、RelayCommand（DelegateCommand）パターン紹介。 |
| 20分 | 実践的トピックとフレームワーク | 1. 導入方法：既存プロジェクトへ段階的に適用する手順。<br>2. フレームワーク紹介：Prism、Community Toolkit MVVM（推奨）、ReactiveProperty の比較。 |
| 10分 | NPOCOの導入 | DB操作の簡略化 |
| 10分 | まとめとQ&A | 1. 振り返り：分離・テスト・バインディングの重要性。<br>2. 次のステップ：フレームワーク導入の推奨。<br>3. 質疑応答。 |
| 合計 |  | 80分 |


# 補足資料

MainWindowの説明

MVVMパターンへの変更

Mvvm Toolkitの導入

Npocoの導入

# セミナー内でのProgramの修正順序

1 App.xaml.csの修正 StartupUri="MainWindow.xaml"

2 WPFアプリ(DBからデータを取得し表示する) MVVMを使わない場合

3 App.xaml.csの修正 StartupUri="Mvvm_ViewClass.xaml"
	開発画面で、 FontSize="30" を追加。全体に影響があることを確認。

4 WPFでMVVMパターンを使った場合の説明 DataContext <local:Mvvm_ViewModelClass />

5 Mvvm Toolkitの導入、実行。 DataContext <local:Mvvm_ViewModelToolkitClass />

6 さらにORMのNpocoを導入、実行。 DataContext <local:Mvvm_ViewModelToolkitNpocoClass />

Xaml側はプレゼンテーション層なので、ほぼ変更はない。
一方、ViewModelは大きく変更されることを確認。

# VS2026で導入されたmermaid chart の説明

縦のフローチャート （上から下: TD）
```mermaid
graph TD
    A[開始] --> B{条件分岐};
    B -->|Yes| C[処理を実行];
    B -->|No| D[処理をスキップ];
    C --> E[終了];
    D --> E;
```
横のフローチャート （左から右: LR）
```mermaid
graph LR
    A[データ入力] -- ユーザー操作 --> B(入力チェック);
    B -- OK --> C{DBに問い合わせ?};
    C -- Yes --> D[データ取得];
    C -- No --> E[単純計算];
    D --> F((結果表示));
    E --> F;
```
シーケンス図
```mermaid
sequenceDiagram
    participant U as ユーザー
    participant AS as アプリケーションサーバ
    participant DB as データベース

    U->>AS: ログイン要求
    AS->>DB: 認証情報を照合
    DB-->>AS: 認証結果を返却
    alt 認証成功
        AS->>U: ログイン成功画面を表示
    else 認証失敗
        AS->>U: エラーメッセージを表示
    end
```
    クラス図 
```mermaid
classDiagram
    class Customer {
        +int customerId
        +string name
        +Order[] orders
    }
    class Order {
        +int orderId
        +DateTime orderDate
    }
    class OrderDetail {
        +int quantity
    }

    Customer "1" *-- "0..*" Order : has
    Order "1" *-- "1..*" OrderDetail : contains
    Order --|> IDisplayable : implements
    <<interface>> IDisplayable
```
ステート図
```mermaid
stateDiagram-v2
    direction LR
    [*] --> Draft
    Draft --> Pending : submit
    Pending --> Approved : approve
    Pending --> Rejected : reject
    Approved --> Paid : make_payment
    Rejected --> Draft : modify
    Paid --> [*] : close
```
ガントチャート
```mermaid
gantt
  dateFormat  YYYY-MM-DD
  title       販売管理システムWPFプロジェクト 計画
  excludes    weekends

  section リリース
  開発期間        :done, 2022-04-01, 2022-04-08
  アルファリリース期間 :active, 5d
  ベータリリース期間  :5d
  正式リリース      :milestone, 0d

  section 開発
  機能開発      :crit, done, 2022-04-01, 3d
  デバッグ      :crit, done, 2d
  アナウンス     :crit, done, 1d
  ベータ機能開発 :crit, active, 4d
  バグ修正      :4d
  最終検証      :1d

```
エンティティ関連図
```mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : places
    CUSTOMER {
        int customer_id PK
        string name
        string email
    }
    ORDER ||--|{ LINEITEM : contains
    ORDER {
        int order_id PK
        string order_date
        int customer_id FK
    }
    LINEITEM {
        int line_item_id PK
        int product_id FK
        int quantity
    }
```
