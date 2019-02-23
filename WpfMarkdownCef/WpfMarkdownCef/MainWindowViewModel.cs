using CefSharp;
using CefSharp.Wpf;
using Markdig;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace WpfMarkdownCef
{
    public class MainWindowViewModel : BindableBase
    {

        //TextBoxにバインドするプロパティ
        private string _mdText;
        public string MdText
        {
            get { return _mdText; }
            set { SetProperty(ref _mdText, value); }
        }

        //Html文字を入れるプロパティ
        public string Html { get; set; }

        //Buttonを押したときのコマンド
        public DelegateCommand<object> ChangeCommand { get; private set; }

        //コンストラクタ
        public MainWindowViewModel()
        {
            //32bitと64bitどちらで動いてるのか調べる(CefSharpはAny CPUでは動作しないので確認するだけなのでなくていい)
            //http://var.blog.jp/archives/68638913.html
            if (Environment.Is64BitProcess)
            {
                Console.WriteLine("64bit");
            }
            else
            {
                Console.WriteLine("32bit");
            }

            //ChangeCommandのset
            ChangeCommand = new DelegateCommand<object>(Execute, CanExecute).ObservesProperty(() => MdText);

        }

        //TextBoxに文字が入っている時だけボタンが押せる
        private bool CanExecute(object o)
        {
            if (string.IsNullOrEmpty(MdText))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //ボタンを押したときの動作
        private void Execute(object o)
        {
            //https://github.com/lunet-io/markdig
            Html = Markdown.ToHtml(MdText);

            //日本語が文字化けるのでhtmlで囲んだ
            Html = $@"<!DOCTYPE html>
<html lang=""ja"">
<head>
<meta charset=""UTF-8"">
<title>Document</title>
</head>
<body>
{Html}
</body>
</html>";

            //https://github.com/cefsharp/CefSharp
            //ChromiumWebBrowser
            //1回目は空で開いてその後LoadHtmlする方法しかわからない
            var browser = new ChromiumWebBrowser();
            browser.Address = "about:blank";
            browser.FrameLoadEnd += FrameLoadEndExecute;

            //CommandParameterのElementNameを使う
            var grid = (System.Windows.Controls.Grid)o;

            //2回目はClearしないと文字が残る
            grid.Children.Clear();
            grid.Children.Add(browser);
        }

        private void FrameLoadEndExecute(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs)
        {
            var browser = (ChromiumWebBrowser)sender;
            browser.LoadHtml(Html);
            browser.FrameLoadEnd -= FrameLoadEndExecute;
        }
    }
}
