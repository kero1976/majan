using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Windows;

namespace 点棒数え.Common
{
    /// <summary>
    /// ラジオボタン用Enum to boolコンバータ
    /// </summary>
    public class OyakoEnum2BooleanConverter:IValueConverter
    {
        //public Type EnumType { get; set; }

        ///// <summary>
        ///// Enum to bool
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="targetType"></param>
        ///// <param name="parameter"></param>
        ///// <param name="language"></param>
        ///// <returns></returns>
        //public object Convert(object value, Type targetType, object parameter, string language)
        //{
        //    if (value == null)
        //    {
        //        return false;
        //    }
        //    return value == parameter || Enum.GetName(this.EnumType, value).Equals(parameter);
        //}


        ///// <summary>
        ///// bool to Enum
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="targetType"></param>
        ///// <param name="parameter"></param>
        ///// <param name="language"></param>
        ///// <returns></returns>
        //public object ConvertBack(object value, Type targetType, object parameter, string language)
        //{
        //    var boolean = value as bool?;
        //    if (boolean != null && boolean.HasValue && boolean.HasValue)
        //    {
        //        try
        //        {
        //            return Enum.Parse(this.EnumType, parameter.ToString());
        //        }
        //        catch (Exception) { }
        //    }
        //    return DependencyProperty.UnsetValue;

        //}
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
}
