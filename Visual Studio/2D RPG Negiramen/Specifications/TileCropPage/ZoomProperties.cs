﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.ViewHistory.TileCropPage;

/// <summary>
///     ズーム関連
/// </summary>
internal class ZoomProperties
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="specObj"></param>
    internal ZoomProperties(
        ItsTwoWayDoor twoWayDoor,
        ItsGardensideDoor gardensideDoor,
        IItsIndoor indoor)
    {
        this.TwoWayDoor = twoWayDoor;
        this.GardensideDoor = gardensideDoor;
        this.Indoor = indoor;
    }
    #endregion

    // - インターナル・プロパティ
    /// <summary>
    ///     ズーム
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///         <item>コード・ビハインドで使用</item>
    ///     </list>
    /// </summary>
    public Zoom Value
    {
        get => value;
        set
        {
            if (this.value == value)
                return;

            // TODO 循環参照しやすいから、良くないコード
            this.GardensideDoor.PageVM.ZoomAsFloat = value.AsFloat;
        }
    }

    /// <summary>
    ///     ［ズーム］整数形式
    ///     
    ///     <list type="bullet">
    ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
    ///     </list>
    /// </summary>
    public float AsFloat
    {
        get => value.AsFloat;
        set
        {
            if (this.value.AsFloat != value)
            {
                if (this.GardensideDoor.PageVM.ZoomMinAsFloat <= value && value <= this.GardensideDoor.PageVM.ZoomMaxAsFloat)
                {
                    Zoom oldValue = this.value;
                    Zoom newValue = new Zoom(value);

                    this.value = newValue;
                    this.Indoor.RoomsideDoorsCropCursorRefreshCanvasTrick("[TileCropPageViewModel.cs ZoomAsFloat]");

                    // 再帰的にズーム再変更、かつ変更後の影響を処理
                    App.History.Do(new ZoomProcessing(
                        this.TwoWayDoor,
                        this.GardensideDoor,
                        this.Indoor,
                        oldValue,
                        newValue));
                }
            }
        }
    }

    /// <summary>
    ///     ズーム最大
    /// </summary>
    public float MaxAsFloat => maxValue.AsFloat;

    /// <summary>
    ///     ズーム最小
    /// </summary>
    public float MinAsFloat => minValue.AsFloat;

    // - プライベート・プロパティ

    ItsTwoWayDoor TwoWayDoor { get; }
    ItsGardensideDoor GardensideDoor { get; }
    IItsIndoor Indoor { get; }

    // - プライベート・フィールド

    /// <summary>
    ///     ［ズーム］
    /// </summary>
    Zoom value = Zoom.IdentityElement;

    /// <summary>
    ///     ［ズーム］最大
    /// </summary>
    Zoom maxValue = new(4.0f);

    /// <summary>
    ///     ［ズーム］最小
    /// </summary>
    Zoom minValue = new(0.5f);
}
