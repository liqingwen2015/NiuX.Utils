using NiuX.Consts;

namespace NiuX.Extensions;

/// <summary>
/// 日期扩展
/// </summary>
public static partial class DateTimeExtensions
{
    /// <summary>
    /// HH:mm:ss
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToHHmmssString(this DateTime dateTime) => dateTime.ToString("HH:mm:ss");

    /// <summary>
    /// hh:mm:ss
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string TohhmmssString(this DateTime dateTime) => dateTime.ToString("hh:mm:ss");

    /// <summary>
    /// yyyy-MM-dd HH:mm:ss
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToyyyyMMddHHmmssString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");

    /// <summary>
    /// yyyy-MM-dd hh:mm:ss
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToyyyyMMddhhmmssString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd hh:mm:ss");

    // query json
}

/// <summary>
/// <seealso cref="DateTime"/>的扩展类
/// </summary>
public static partial class DateTimeExtensions
{
    /// <summary>
    /// 表示为 DateTime 的纪元
    /// </summary>
    internal static readonly DateTime Epoch;

    static DateTimeExtensions() => Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 将给定的 <see cref="DateTime"/> 转换为Epoch的毫秒数。
    /// </summary>
    /// <param name="dateTime">给定的 <see cref="DateTime"/></param>
    /// <returns>自纪元以来的毫秒数</returns>
    public static long ToEpochMilliseconds(this DateTime dateTime) =>
        (long)dateTime.ToUniversalTime().Subtract(Epoch).TotalMilliseconds;

    /// <summary>
    ///将给定的 <see cref="DateTime"/> 转换为距纪元的秒。
    /// </summary>
    /// <param name="dateTime">给定的 <see cref="DateTime"/></param>
    /// <returns>Unix时间戳</returns>
    public static long ToEpochSeconds(this DateTime dateTime) => dateTime.ToEpochMilliseconds() / 1000;

    /// <summary>
    /// 检查给定的日期是否在两个提供的日期之间
    /// <param name="date">给定的 <see cref="DateTime"/></param>
    /// <param name="startDate">开始日期 <see cref="DateTime"/></param>
    /// <param name="endDate">结束日期 <see cref="DateTime"/></param>
    /// <param name="compareTime">是否比较时间 <see cref="Boolean"/></param>
    /// </summary>
    public static bool IsBetween(this DateTime date, DateTime startDate, DateTime endDate, bool compareTime = false) =>
        compareTime
            ? date >= startDate && date <= endDate
            : date.Date >= startDate.Date && date.Date <= endDate.Date;

    /// <summary>
    /// 返回给定日期是否为该月的最后一天
    /// </summary>
    public static bool IsLastDayOfTheMonth(this DateTime dateTime) =>
        dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);

    /// <summary>
    /// 返回给定日期是否位于周末(周六或周日)
    /// </summary>
    public static bool IsWeekend(this DateTime value) =>
        value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;

    /// <summary>
    /// 确定给定年份是否为闰年。
    /// </summary>
    public static bool IsLeapYear(this DateTime value) => DateTime.DaysInMonth(value.Year, 2) == 29;

    /// <summary>
    /// 返回基于 <paramref name="birthDay"/> 的年龄。
    /// </summary>
    /// <param name="birthDay">应计算年龄的生日</param>
    public static int Age(this DateTime birthDay)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDay.Year;

        if (birthDay > today.AddYears(-age)) age--;
        return age;
    }

    /// <summary>
    /// 返回今天凌晨时间,如: "2022-02-07 20:12:09" => "2022-02-07 00:00:00"
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime TodayStart(this DateTime dt) => dt.Date;

    /// <summary>
    /// 返回明天凌晨时间,如: "2022-02-07 20:12:09" => "2022-02-08 00:00:00"
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime TomorrowStart(this DateTime dt) => dt.AddDays(1).Date;

    #region 返回格式化字符串

    /// <summary>
    /// 返回"yyyyMMddHHmmss"格式的字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFileNameString(this DateTime value) => value.ToString("yyyyMMddHHmmss");

    /// <summary>
    /// 返回"yyyyMMddHHmmssfff"格式的字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFileNameStampString(this DateTime value) => value.ToString("yyyyMMddHHmmssfff");

    /// <summary>
    /// 返回"yyyyMMddHHmmssfff_guidformatwith32letterinatoz0to9{ext}"格式的字符串<para></para>
    /// 如: 20210925010203045_5223ed80c21a4facbc30225ed7e061d5.txt （ext参数为: .txt）
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ext"></param>
    /// <returns></returns>
    public static string ToFileNameGuidString(this DateTime value, string ext = "") =>
        $"{value:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{ext}";

    #region 不输出时区信息，使用服务器机器时区

    public static string DefaultCommonString { get; set; } = DateTimeConsts.CommonString;

    public static string DefaultCommonDateString { get; set; } = DateTimeConsts.CommonDateString;

    public static string DefaultCommonTimeString { get; set; } = DateTimeConsts.CommonTimeString;

    public static string DefaultCommonMinuteString { get; set; } = DateTimeConsts.CommonMinuteString;

    public static string DefaultCommonStampString { get; set; } = DateTimeConsts.CommonStampString;

    public static string DefaultGlobalMinuteString { get; set; } = DateTimeConsts.GlobalMinuteString;

    public static string DefaultGlobalStampString { get; set; } = DateTimeConsts.GlobalStampString;

    public static string DefaultGlobalString { get; set; } = DateTimeConsts.GlobalString;

    /// <summary>
    /// 返回"yyyy-MM-dd HH:mm:ss"格式的字符串
    /// </summary>
    /// <returns></returns>
    public static string ToCommonString(this DateTime value)
        => value.ToString(DefaultCommonString);

    /// <summary>
    /// 返回"yyyy-MM-dd"格式的字符串
    /// </summary>
    /// <returns></returns>
    public static string ToCommonDateString(this DateTime value)
        => value.ToString(DefaultCommonDateString);

    /// <summary>
    /// 返回"HH:mm:ss"格式的字符串
    /// </summary>
    /// <returns></returns>
    public static string ToCommonTimeString(this DateTime value)
        => value.ToString(DefaultCommonTimeString);

    /// <summary>
    /// 返回"yyyy-MM-dd HH:mm"格式的字符串
    /// </summary>
    /// <returns></returns>
    public static string ToCommonMinuteString(this DateTime value)
        => value.ToString(DefaultCommonMinuteString);

    /// <summary>
    /// 返回"yyyy-MM-dd HH:mm:ss.fff"格式的字符串
    /// </summary>
    /// <returns></returns>
    public static string ToCommonStampString(this DateTime value)
        => value.ToString(DefaultCommonStampString);

    #endregion 不输出时区信息，使用服务器机器时区

    #region 输出时区信息，使用服务器机器时区

    /// <summary>
    /// 返回"yyyy-MM-dd HH:mm zzz"格式的字符串, 如: "2021-02-02 13:45 +08:00"
    /// </summary>
    /// <returns></returns>
    public static string ToGlobalMinuteString(this DateTime value)
        => value.ToString(DefaultGlobalMinuteString);

    /// <summary>
    /// 返回"yyyy-MM-dd HH:mm:ss.fff zzz"格式的字符串, 如: "2021-02-02 13:45:02.123 +08:00"
    /// </summary>
    /// <returns></returns>
    public static string ToGlobalStampString(this DateTime value)
        => value.ToString(DefaultGlobalStampString);

    /// <summary>
    /// 返回"yyyy-MM-dd HH:mm:ss zzz"格式的字符串，如: "2021-02-02 13:45:02 +08:00"
    /// </summary>
    /// <returns></returns>
    public static string ToGlobalString(this DateTime value)
        => value.ToString(DefaultGlobalString);

    #endregion 输出时区信息，使用服务器机器时区

    #endregion 返回格式化字符串
}