# CLAUDE.md

このファイルは、このリポジトリでコードを扱う際に Claude Code (claude.ai/code) へ向けたガイダンスを提供します。

## プロジェクト概要

.NET 8 (`net8.0-windows`) をターゲットとした WPF デスクトップアプリケーションです。WPF/XAML の画面周り（レイアウト・データバインディング・MVVM・画面遷移）の要点を学ぶためのシンプルな TODO リストのサンプルアプリです。MVVM は外部ライブラリ（CommunityToolkit.Mvvm 等）を使わず、`INotifyPropertyChanged` と `ICommand` を素の C# で自作しています。まだ `.git` リポジトリは初期化されていません。

## コマンド

リポジトリルート（`WpfMvvmTodoSample.csproj` / `WpfMvvmTodoSample.slnx`）から実行します。

- ビルド: `dotnet build`
- 実行: `dotnet run`
- 復元: `dotnet restore`

このリポジトリには現時点でテストプロジェクトはありません。

## アーキテクチャ

- `App.xaml` / `App.xaml.cs` — アプリケーションのエントリーポイント。`App.xaml` の `StartupUri` が `MainWindow.xaml` を指しています。
- `MainWindow.xaml` / `MainWindow.xaml.cs` — TODO 一覧画面。`Window.DataContext` に `MainViewModel` を直接指定し、`ListBox` で一覧を表示します。追加/編集ボタンはコードビハインドの `Click` イベントで `Views/TodoEditWindow` をモーダル表示します。
- `Models/TodoItem.cs` — TODO 1件のデータモデル。チェックボックスとの双方向バインディングで一覧の取り消し線をリアルタイムに反映させるため、モデル自身が `INotifyPropertyChanged` を実装しています。
- `ViewModels/ViewModelBase.cs` — `INotifyPropertyChanged` の共通基底クラス（`SetProperty` ヘルパー付き）。
- `ViewModels/RelayCommand.cs` — `ICommand` の自作実装（`CommandManager.RequerySuggested` 方式）。
- `ViewModels/MainViewModel.cs` — `MainWindow` の `DataContext`。`ObservableCollection<TodoItem>` と、選択中アイテムがある場合のみ実行可能な `DeleteCommand` を持ちます。`Window` 型は一切参照しません。
- `ViewModels/TodoEditViewModel.cs` — 追加/編集ダイアログの `DataContext`。`TodoItem` を直接編集させず、いったんコピーして編集し、OK 時のみ呼び出し元が反映します。
- `Views/TodoEditWindow.xaml(.cs)` — 追加/編集用のモーダルウィンドウ。OK/Cancel はコマンド化せず、コードビハインドの `Click` イベントで `DialogResult` を設定します。

コマンドとバインディングの使い分け方針: 完了チェックのように実行可否の判定が不要な操作は `TwoWay` バインディングのみで済ませ、削除のように「未選択なら実行不可」というガード条件がある操作にのみ `RelayCommand` を使います。ウィンドウ生成やモーダル結果の受け取りといった View 固有の関心事は ViewModel に持たせず、コードビハインド（`MainWindow.xaml.cs` / `TodoEditWindow.xaml.cs`）に閉じています。
