using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace 点棒数え.Common
{
    /// <summary>
    /// ラジオボタン用Enum to boolコンバータベースクラス
    /// </summary>
    public abstract class Enum2BooleanConverter : IValueConverter
    {

        public abstract object ConvertFromConverterParameter(object parameter);

        #region IValueConverter メンバー
        // Enum → bool
        public object Convert(object value, Type targetType, object parameter,
                              string language)
        {
            // ConverterParameterとバインディングソースの値が等しいか？
            return ConvertFromConverterParameter(parameter).Equals(value);
        }

        // bool → Enum
        public object ConvertBack(object value, Type targetType, object parameter,
                                  string language)
        {
            // true→falseの変化は無視する
            // ※こうすることで、選択されたラジオボタンだけをデータに反映させる
            if (!(bool)value)
                return DependencyProperty.UnsetValue;

            // ConverterParameterをEnumに変換して返す
            return ConvertFromConverterParameter(parameter);
        }
        #endregion
    }

    /// <summary>
    /// 親子ラジオボタン用Enum to boolコンバータ
    /// </summary>
    public class OyakoEnum2BooleanConverter : Enum2BooleanConverter
    {
        // ConverterParameterをEnumに変換するメソッド
        public override object ConvertFromConverterParameter(object parameter)
        {
            string parameterString = parameter as string;
            return Enum.Parse(typeof(親子), parameterString);
        }
    }

    /// <summary>
    /// 宣言ラジオボタン用Enum to boolコンバータ
    /// </summary>
    public class SengenEnum2BooleanConverter : Enum2BooleanConverter
    {
        // ConverterParameterをEnumに変換するメソッド
        public override object ConvertFromConverterParameter(object parameter)
        {
            string parameterString = parameter as string;
            return Enum.Parse(typeof(宣言), parameterString);
        }
    }
}
