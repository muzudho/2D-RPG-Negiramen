namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    internal class TilePalettePopupViewModel : ObservableObject
    {
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

        internal TilePalettePopupViewModel() : this(
            Models.X.Empty, Models.Y.Empty)
        {
        }

        internal TilePalettePopupViewModel(Models.X x, Models.Y y)
        {
            this.XAsInt = x.AsInt;
            this.YAsInt = y.AsInt;
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
    }
}
