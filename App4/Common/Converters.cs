using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace 点棒数え.Common
{
    /// <summary>
    /// ラジオボタン用Enum to boolコンバータ
    /// </summary>
    public abstract class Enum2BooleanConverter : IValueConverter
    {
        // ConverterParameterをEnumに変換するメソッド
        private 親子 ConvertFromConverterParameter(object parameter)
        {
            string parameterString = parameter as string;
            return (親子)Enum.Parse(typeof(親子), parameterString);
        }

        #region IValueConverter メンバー
        // Enum → bool
        public object Convert(object value, Type targetType, object parameter,
                              string language)
        {
            // XAMLに定義されたConverterParameterをEnumに変換する
            親子 parameterValue = ConvertFromConverterParameter(parameter);

            // ConverterParameterとバインディングソースの値が等しいか？
            return parameterValue.Equals(value);
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
    /// ラジオボタン用親子Enum to boolコンバータ
    /// </summary>
    public class OyakoEnum2BooleanConverter : IValueConverter
    {
        // ConverterParameterをEnumに変換するメソッド
        private 親子 ConvertFromConverterParameter(object parameter)
        {
            string parameterString = parameter as string;
            return (親子)Enum.Parse(typeof(親子), parameterString);
        }

        #region IValueConverter メンバー
        // Enum → bool
        public object Convert(object value, Type targetType, object parameter,
                              string language)
        {
            // XAMLに定義されたConverterParameterをEnumに変換する
            親子 parameterValue = ConvertFromConverterParameter(parameter);

            // ConverterParameterとバインディングソースの値が等しいか？
            return parameterValue.Equals(value);
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
        /// ラジオボタン用宣言Enum to boolコンバータ
        /// </summary>
        public class SengenEnum2BooleanConverter : IValueConverter
        {
            // ConverterParameterをEnumに変換するメソッド
            private 宣言 ConvertFromConverterParameter(object parameter)
            {
                string parameterString = parameter as string;
                return (宣言)Enum.Parse(typeof(宣言), parameterString);
            }

            #region IValueConverter メンバー
            // Enum → bool
            public object Convert(object value, Type targetType, object parameter,
                                  string language)
            {
                // XAMLに定義されたConverterParameterをEnumに変換する
                宣言 parameterValue = ConvertFromConverterParameter(parameter);

                // ConverterParameterとバインディングソースの値が等しいか？
                return parameterValue.Equals(value);
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
    }
