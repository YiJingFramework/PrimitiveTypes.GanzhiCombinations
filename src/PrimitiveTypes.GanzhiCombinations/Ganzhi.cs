using System.Diagnostics;
using System.Numerics;

namespace YiJingFramework.PrimitiveTypes.GanzhiCombinations;

/// <summary>
/// 干支。
/// Ganzhi.
/// </summary>
public readonly struct Ganzhi :
    IComparable<Ganzhi>, IEquatable<Ganzhi>, IFormattable,
    IEqualityOperators<Ganzhi, Ganzhi, bool>,
    IAdditionOperators<Ganzhi, int, Ganzhi>,
    ISubtractionOperators<Ganzhi, int, Ganzhi>
{
    private readonly int indexMinusOne;
    private Ganzhi(int indexMinusOneChecked)
    {
        Debug.Assert(indexMinusOneChecked is >= 0 and < 60);
        this.indexMinusOne = indexMinusOneChecked;
    }

    /// <summary>
    /// 获取此干支的下第 <paramref name="n"/> 个干支。
    /// Get the <paramref name="n"/>th Ganzhi next to this instance.
    /// </summary>
    /// <param name="n">
    /// 数字 <paramref name="n"/> 。
    /// 可以小于零以表示另一个方向。
    /// The number <paramref name="n"/>.
    /// It could be smaller than zero which means the other direction.
    /// </param>
    /// <returns>
    /// 指定干支。
    /// The Ganzhi.
    /// </returns>
    public Ganzhi Next(int n = 1)
    {
        n %= 60;
        n += 60;
        n += this.indexMinusOne;
        return new Ganzhi(n % 60);
    }

    /// <inheritdoc/>
    public static Ganzhi operator +(Ganzhi left, int right)
    {
        right %= 60;
        right += 60;
        right += left.indexMinusOne;
        return new Ganzhi(right % 60);
    }

    /// <inheritdoc/>
    public static Ganzhi operator -(Ganzhi left, int right)
    {
        right %= 60;
        right = -right;
        right += 60;
        right += left.indexMinusOne;
        return new Ganzhi(right % 60);
    }

    /// <inheritdoc/>
    public static int operator -(Ganzhi left, Ganzhi right)
    {
        return (left.indexMinusOne - right.indexMinusOne + 60) % 60;
    }

    #region string converting
    /// <inheritdoc/>
    public override string ToString()
    {
        return this.ToString("G");
    }

    /// <summary>
    /// 按照指定格式转换为字符串。
    /// Convert to a string with the given format.
    /// </summary>
    /// <param name="format">
    /// 要使用的格式。
    /// <c>"G"</c> 表示拼音； <c>"C"</c> 表示中文。
    /// The format to use.
    /// <c>"G"</c> means to be in Pinyin; and <c>"C"</c> means in Chinese.
    /// </param>
    /// <param name="formatProvider">
    /// 不会使用此参数。
    /// This parameter will won't be used.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    /// <exception cref="FormatException">
    /// 给出的格式化字符串不受支持。
    /// The given format is not supported.
    /// </exception>
    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        return $"{this.Tiangan.ToString(format)}" +
            $"{this.Dizhi.ToString(format).ToLowerInvariant()}";
    }
    #endregion

    #region int converting
    /// <summary>
    /// 干支的序数。
    /// 如甲子为 <c>1</c> 。
    /// Get the index of the Tiangan.
    /// For example, it will be <c>1</c> for Jiazi.
    /// </summary>
    public int Index => this.indexMinusOne + 1;

    /// <summary>
    /// 从 <seealso cref="Index"/> 获取干支。
    /// Get a Ganzhi from its <seealso cref="Index"/>.
    /// </summary>
    /// <param name="index">
    /// 序数。
    /// The index.
    /// </param>
    /// <returns>
    /// 干支。
    /// The Ganzhi.
    /// </returns>
    public static Ganzhi FromIndex(int index)
    {
        index %= 60;
        index += 60 - 1;
        return new Ganzhi(index % 60);
    }

    /*
    /// <inheritdoc/>
    public static explicit operator int(Ganzhi dizhi)
    {
        return dizhi.index;
    }

    /// <inheritdoc/>
    public static explicit operator Ganzhi(int value)
    {
        value %= 60;
        value += 60;
        return new Ganzhi(value % 60);
    }
    */
    #endregion

    #region ganzhi converting
    /// <summary>
    /// 天干部分。
    /// The Tiangan part.
    /// </summary>
    public Tiangan Tiangan => (Tiangan)this.indexMinusOne;

    /// <summary>
    /// 地支部分。
    /// The Dizhi part.
    /// </summary>
    public Dizhi Dizhi => (Dizhi)this.indexMinusOne;

    /// <summary>
    /// 析构此实体到 <seealso cref="PrimitiveTypes.Tiangan"/> 和 <seealso cref="PrimitiveTypes.Dizhi"/> 。
    /// Deconstruct the instance to a <seealso cref="PrimitiveTypes.Tiangan"/> and a <seealso cref="PrimitiveTypes.Dizhi"/>.
    /// </summary>
    /// <param name="tiangan">
    /// 天干部分。
    /// The Tiangan part.
    /// </param>
    /// <param name="dizhi">
    /// 地支部分。
    /// The Dizhi part.
    /// </param>
    public void Deconstruct(out Tiangan tiangan, out Dizhi dizhi)
    {
        tiangan = this.Tiangan;
        dizhi = this.Dizhi;
    }

    /// <summary>
    /// 从 <seealso cref="Tiangan"/> 和 <seealso cref="Dizhi"/> 获取干支。
    /// Get a Ganzhi from its <seealso cref="Tiangan"/> and <seealso cref="Dizhi"/>.
    /// </summary>
    /// <param name="tiangan">
    /// 天干部分。
    /// The Tiangan part.
    /// </param>
    /// <param name="dizhi">
    /// 地支部分。
    /// The Dizhi part.
    /// </param>
    /// <returns>
    /// 干支。
    /// The Ganzhi.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// 天干地支的阴阳属性不匹配。
    /// The Yinyangs of the Tiangan and the Dizhi do not match.
    /// </exception>
    public static Ganzhi FromGanzhi(Tiangan tiangan, Dizhi dizhi)
    {
        var t = (int)tiangan;
        var d = (int)dizhi;
        if (t % 2 != d % 2)
            throw new ArgumentException(
                $"The Yinyangs of the Tiangan {tiangan} and the Dizhi {dizhi} do not match.");
        var g = 6 * t - 5 * d + 60;
        return new Ganzhi(g % 60);
    }
    #endregion

    #region comparing

    /// <inheritdoc/>
    public int CompareTo(Ganzhi other)
    {
        return this.indexMinusOne.CompareTo(other.indexMinusOne);
    }

    /// <inheritdoc/>
    public bool Equals(Ganzhi other)
    {
        return this.indexMinusOne.Equals(other.indexMinusOne);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is not Ganzhi other)
            return false;
        return this.indexMinusOne.Equals(other.indexMinusOne);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.indexMinusOne.GetHashCode();
    }

    /// <inheritdoc/>
    public static bool operator ==(Ganzhi left, Ganzhi right)
    {
        return left.indexMinusOne == right.indexMinusOne;
    }

    /// <inheritdoc/>
    public static bool operator !=(Ganzhi left, Ganzhi right)
    {
        return left.indexMinusOne != right.indexMinusOne;
    }
    #endregion
}
