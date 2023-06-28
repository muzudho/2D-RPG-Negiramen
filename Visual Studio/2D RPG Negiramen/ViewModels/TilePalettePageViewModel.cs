namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// ［タイル・パレット・ページ］ビューモデル
    /// </summary>
    internal class TilePalettePageViewModel : ObservableObject
    {
        // - プロパティ

        /// <summary>
        ///     マウス・カーソルのキャプチャーに利用するコマンド
        /// </summary>
        public ICommand PointerMovedCommand { get; }

        // - 変更通知プロパティ

        /// <summary>
        /// X
        /// </summary>
        public int XAsInt
        {
            get => _x.AsInt;
            set
            {
                if (_x.AsInt != value)
                {
                    _x = new Models.X(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Y
        /// </summary>
        public int YAsInt
        {
            get => _y.AsInt;
            set
            {
                if (_y.AsInt != value)
                {
                    _y = new Models.Y(value);
                    OnPropertyChanged();
                }
            }
        }

        // - その他

        /// <summary>
        ///     生成
        ///     
        ///     <list type="bullet">
        ///         <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        ///     </list>
        /// </summary>
        public TilePalettePageViewModel() : this(
            Models.X.Empty, Models.Y.Empty)
        {
        }

        internal TilePalettePageViewModel(Models.X x, Models.Y y)
        {
            this.XAsInt = x.AsInt;
            this.YAsInt = y.AsInt;

            // コマンド
            PointerMovedCommand = new Command(CapturePointerMoved);
        }

        // - プライベート・フィールド

        /// <summary>
        /// X
        /// </summary>
        private Models.X _x = Models.X.Empty;

        /// <summary>
        /// Y
        /// </summary>
        private Models.Y _y = Models.Y.Empty;

        // - プライベート・メソッド

        /// <summary>
        /// マウス・カーソルの位置をキャプチャーする
        /// </summary>
        /// <returns>なし</returns>
        void CapturePointerMoved()
        {
            Trace.WriteLine("[TilePalettePageViewModel CapturePointerMoved]");
            /*
        /// <param name="sender">このイベントを起こしたオブジェクト</param>
        /// <param name="e">ポインター・イベント引数</param>
            // object sender, PointerEventArgs e

            await Task.Run(() =>
            {
                var point = e.GetPosition((Element)sender).Value;

                this.XAsInt = (int)point.X;
                this.YAsInt = (int)point.Y;
            });

            // 画面遷移、戻る
            await Shell.Current.GoToAsync("..");

            // 履歴は戻しておく
            var shellNavigationState = App.NextPage.Pop();

            // 全ての入力が準備できているなら、画面遷移する
            var newConfiguration = App.GetOrLoadConfiguration();
            if (newConfiguration.IsReady())
            {
                await Shell.Current.GoToAsync(shellNavigationState);
            }
            */
        }
    }
}
