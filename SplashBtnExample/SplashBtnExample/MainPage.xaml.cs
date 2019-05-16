using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SplashBtnExample.Component;

namespace SplashBtnExample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitMenuBtn();
        }

        /// <summary>
        /// 初始化分散按钮的设置
        /// </summary>
        private void InitMenuBtn()
        {
            // 设置子按钮
            List<SubButton> subs = new List<SubButton>();
            subs.Add(new SubButton { ImageUrl = "icon_setting.png", Action = TestMethod });
            subs.Add(new SubButton { ImageUrl = "icon_setting.png", Action = TestMethod });
            subs.Add(new SubButton { ImageUrl = "icon_setting.png", Action = TestMethod });
            subs.Add(new SubButton { ImageUrl = "icon_setting.png", Action = TestMethod });
            subs.Add(new SubButton { ImageUrl = "icon_setting.png", Action = TestMethod });

            // 设置按钮的直径为40px
            menuBtn.ButtonDiameter = 40;
            // 设置主按钮的按钮图片
            menuBtn.SetMenuImage("icon_setting.png");
            // 绑定子按钮
            menuBtn.SetMenuButton(subs);
        }

        private void TestMethod(object obj, EventArgs args)
        {
            // your code
            // 子按钮的点击事件绑定到这个方法
        }
    }
}
