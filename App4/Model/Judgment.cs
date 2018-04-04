using Prism.Mvvm;
using System;
using System.Diagnostics;
using 点棒数え.Common;

namespace 点棒数え.Model
{
    /// <summary>
    /// 審判クラス。
    /// </summary>
    /// 審判は1人のため、シングルトン。
    public class Judgment : IObserver<宣言>
    {
        private static Judgment instance = new Judgment();

        // シングルトンのためコンストラクタを非公開に
        private Judgment() { }

        public int sentenbou = 0;


        public static Judgment Instance
        {
            get { return instance; }
        }

        public void OnCompleted()
        {
            Debug.WriteLine("審判「終了」");
        }

        public void OnError(Exception error)
        {
            Debug.WriteLine("審判「エラー」");
        }

        public void OnNext(宣言 value)
        {
            Debug.WriteLine("審判「宣言は{0}」",value);
            switch (value)
            {
                case 宣言.リーチ:
                    sentenbou++;
                    break;
                case 宣言.ツモ:
                    sentenbou = 0;
                    break;
            }
            Debug.WriteLine("審判「千点棒は{0}」", sentenbou);
        }

    }
}