using System.Numerics;

namespace YiJingFramework.PrimitiveTypes.GanzhiCombinations;

/// <summary>
/// 天干或地支。
/// A Tiangan or a Dizhi.
/// </summary>
public readonly struct TianganOrDizhi :
    IComparable<TianganOrDizhi>, IEquatable<TianganOrDizhi>, IFormattable,
    IEqualityOperators<TianganOrDizhi, TianganOrDizhi, bool>,
    IAdditionOperators<TianganOrDizhi, int, TianganOrDizhi>,
    ISubtractionOperators<TianganOrDizhi, int, TianganOrDizhi>
{
    private const int dizhiMask = 0x10;
    private readonly int maskedIndex;
    /// <summary>
    /// 创建一个 <seealso cref="TianganOrDizhi"/> 的实例。
    /// Initialize an instance of <seealso cref="TianganOrDizhi"/>.
    /// </summary>
    /// <param name="tiangan">
    /// 所表示的天干。
    /// The Tiangan to be represented.
    /// </param>
    public TianganOrDizhi(Tiangan tiangan)
    {
        this.maskedIndex = (int)tiangan;
    }
    /// <summary>
    /// 创建一个 <seealso cref="TianganOrDizhi"/> 的实例。
    /// Initialize an instance of <seealso cref="TianganOrDizhi"/>.
    /// </summary>
    /// <param name="dizhi">
    /// 所表示的地支。
    /// The Dizhi to be represented.
    /// </param>
    public TianganOrDizhi(Dizhi dizhi)
    {
        this.maskedIndex = (int)dizhi | dizhiMask;
    }

    #region converting
    /// <summary>
    /// 指示此实例是否表示天干。
    /// Indicates whether the instance represents a Tiangan.
    /// </summary>
    public bool IsTiangan => (maskedIndex & dizhiMask) is 0;
    /// <summary>
    /// 指示此实例是否表示地支。
    /// Indicates whether the instance represents a Dizhi.
    /// </summary>
    public bool IsDizhi => (maskedIndex & dizhiMask) is not 0;

    /// <summary>
    /// 尝试将此实例转换为天干。
    /// Try to convert the instance to a Tiangan.
    /// </summary>
    /// <param name="tiangan">
    /// 转换结果。
    /// 即使转换失败，此参数也可能具有非默认的值。
    /// The conversion result.
    /// Even when the conversion is failed, the parameter may also have a value other than the default one.
    /// </param>
    /// <returns>
    /// 指示转换是否成功。
    /// Indicates whether the conversion is succeeded or not.
    /// </returns>
    public bool TryAsTiangan(out Tiangan tiangan)
    {
        var index = this.maskedIndex & (~dizhiMask);
        tiangan = (Tiangan)index;
        return index == this.maskedIndex;
    }

    /// <summary>
    /// 尝试将此实例转换为地支。
    /// Try to convert the instance to a Dizhi.
    /// </summary>
    /// <param name="dizhi">
    /// 转换结果。
    /// 即使转换失败，此参数也可能具有非默认的值。
    /// The conversion result.
    /// Even when the conversion is failed, the parameter may also have a value other than the default one.
    /// </param>
    /// <returns>
    /// 指示转换是否成功。
    /// Indicates whether the conversion is succeeded or not.
    /// </returns>
    public bool TryAsDizhi(out Dizhi dizhi)
    {
        var index = this.maskedIndex & (~dizhiMask);
        dizhi = (Dizhi)index;
        return index != this.maskedIndex;
    }

    /// <summary>
    /// 尝试将此实例转换为天干。
    /// 若失败则也转换为地支。
    /// Try to convert the instance to a Tiangan.
    /// And then convert to a Dizhi if failed.
    /// </summary>
    /// <param name="tiangan">
    /// 转换到天干的结果。
    /// 即使转换失败，此参数可能具有非默认的值。
    /// The conversion result.
    /// Even when the conversion is failed, the parameter may also have a value other than the default one.
    /// </param>
    /// <param name="dizhi">
    /// 转换到地支的结果。
    /// 即使（到天干的）转换成功，此参数也可能具有非默认的值。
    /// The conversion result.
    /// Even when the conversion (to Tiangan) is succeeded, the parameter may also have a value other than the default one.
    /// </param>
    /// <returns>
    /// 指示（到天干的）转换是否成功。
    /// Indicates whether the conversion (to Tiangan) is succeeded or not.
    /// </returns>
    public bool TryAsTiangan(out Tiangan tiangan, out Dizhi dizhi)
    {
        var index = this.maskedIndex & (~dizhiMask);
        tiangan = (Tiangan)index;
        dizhi = (Dizhi)index;
        return index == this.maskedIndex;
    }

    /// <exception cref="InvalidCastException">
    /// 给定的实例表示的是地支而非天干。
    /// The given instance represents a Dizhi instead of a Tiangan.
    /// </exception>
    /// <inheritdoc />
    public static explicit operator Tiangan(TianganOrDizhi tianganOrDizhi)
    {
        if (!tianganOrDizhi.TryAsTiangan(out var result))
            throw new InvalidCastException(
                "The given instance represents a Dizhi, so it cannot be converted to a Tiangan.");
        return result;
    }

    /// <exception cref="InvalidCastException">
    /// 给定的实例表示的是天干而非地支。
    /// The given instance represents a Tiangan instead of a Dizhi.
    /// </exception>
    /// <inheritdoc />
    public static explicit operator Dizhi(TianganOrDizhi tianganOrDizhi)
    {
        if (!tianganOrDizhi.TryAsDizhi(out var result))
            throw new InvalidCastException(
                "The given instance represents a Tiangan, so it cannot be converted to a Dizhi.");
        return result;
    }

    /// <inheritdoc />
    public static implicit operator TianganOrDizhi(Tiangan tiangan)
    {
        return new TianganOrDizhi(tiangan);
    }

    /// <inheritdoc />
    public static implicit operator TianganOrDizhi(Dizhi dizhi)
    {
        return new TianganOrDizhi(dizhi);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (this.TryAsTiangan(out var t, out var d))
            return t.ToString();
        return d.ToString();
    }

    /// <inheritdoc cref="Tiangan.ToString(string?, IFormatProvider?)" />
    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        if (this.TryAsTiangan(out var t, out var d))
            return t.ToString(format);
        return d.ToString(format);
    }
    #endregion


    /// <summary>
    /// 获取此天干或地支的下第 <paramref name="n"/> 个。
    /// Get the <paramref name="n"/>th Tiangan or Dizhi next to this instance.
    /// </summary>
    /// <param name="n">
    /// 数字 <paramref name="n"/> 。
    /// 可以小于零以表示另一个方向。
    /// The number <paramref name="n"/>.
    /// It could be smaller than zero which means the other direction.
    /// </param>
    /// <returns>
    /// 指定天干或地支。
    /// The Tiangan or Dizhi.
    /// </returns>
    public TianganOrDizhi Next(int n = 1)
    {
        if (this.TryAsTiangan(out var t, out var d))
            return new TianganOrDizhi(t.Next(n));
        return new TianganOrDizhi(d.Next(n));
    }

    /// <inheritdoc/>
    public static TianganOrDizhi operator +(TianganOrDizhi left, int right)
    {
        if (left.TryAsTiangan(out var t, out var d))
            return new TianganOrDizhi(t + right);
        return new TianganOrDizhi(d + right);
    }

    /// <inheritdoc/>
    public static TianganOrDizhi operator -(TianganOrDizhi left, int right)
    {
        if (left.TryAsTiangan(out var t, out var d))
            return new TianganOrDizhi(t - right);
        return new TianganOrDizhi(d - right);
    }

    #region comparing

    /// <inheritdoc/>
    public int CompareTo(TianganOrDizhi other)
    {
        return this.maskedIndex.CompareTo(other.maskedIndex);
    }

    /// <inheritdoc/>
    public bool Equals(TianganOrDizhi other)
    {
        return this.maskedIndex.Equals(other.maskedIndex);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is not TianganOrDizhi other)
            return false;
        return this.maskedIndex.Equals(other.maskedIndex);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.maskedIndex.GetHashCode();
    }

    /// <inheritdoc/>
    public static bool operator ==(TianganOrDizhi left, TianganOrDizhi right)
    {
        return left.maskedIndex == right.maskedIndex;
    }

    /// <inheritdoc/>
    public static bool operator !=(TianganOrDizhi left, TianganOrDizhi right)
    {
        return left.maskedIndex != right.maskedIndex;
    }
    #endregion
}
