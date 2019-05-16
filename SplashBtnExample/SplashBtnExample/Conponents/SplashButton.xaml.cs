using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashBtnExample.Component
{
    /// <summary>
    /// 子按钮
    /// </summary>
    public class SubButton
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        public Action<object, EventArgs> Action { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashButton : RelativeLayout
    {
        private int diameter;
        /// <summary>
        /// 按钮直径(px)默认30
        /// </summary>
        public int ButtonDiameter
        {
            get { return diameter; }
            set
            {
                diameter = value;
                //MenuBtn.CornerRadius = diameter;
                MenuBtn.HeightRequest = diameter;
                MenuBtn.WidthRequest = diameter;
                this.WidthRequest = diameter;
            }
        }

        /// <summary>
        /// 子按钮集合
        /// </summary>
        public List<SubButton> BtnList { get; private set; }

        /// <summary>
        /// 当前是否展开
        /// </summary>
        private bool isOpen { get; set; }

        /// <summary>
        /// 子按钮的按钮实例集合
        /// </summary>
        private List<Button> subButtons;

        public SplashButton()
        {
            InitializeComponent();
            ButtonDiameter = 30;
            MenuBtn.Clicked += new EventHandler(MenuBtn_Clicked);
        }

        /// <summary>
        /// 设置主按钮的图片
        /// </summary>
        /// <param name="menuImgUrl">图片路径</param>
		public void SetMenuImage(string menuImgUrl)
        {
            isOpen = false;
            MenuBtn.Image = menuImgUrl;
        }

        /// <summary>
        /// 设置子按钮
        /// </summary>
        /// <param name="subButtons">子按钮集合</param>
        public void SetMenuButton(List<SubButton> subButtons)
        {
            this.BtnList = subButtons;
            this.subButtons = new List<Button>();
            int distance = GetDisrance(diameter, subButtons.Count);
            RelativeLayout parentView = this.Parent as RelativeLayout;
            for (int i = 0; i < subButtons.Count; i++)
            {
                var item = subButtons[i];
                Button subBtn = new Button();
                subBtn.CornerRadius = diameter;
                subBtn.WidthRequest = diameter;
                subBtn.HeightRequest = diameter;
                subBtn.Clicked += MenuBtn_Clicked;
                subBtn.Clicked += new EventHandler(item.Action);
                subBtn.Image = item.ImageUrl;

                var position = GetOffset(distance, i, subButtons.Count);
                int x = position.Item1;         //当前左下角，右下角需要乘-1
                int y = -1 * position.Item2;
                subBtn.Scale = 0;
                this.subButtons.Add(subBtn);
                parentView.Children.Add(subBtn, Constraint.RelativeToView(this, (parent, sibling) => {
                    return sibling.X + x;
                }), Constraint.RelativeToView(this, (parent, sibling) => {
                    return sibling.Y + y;
                }));
            }
        }

        /// <summary>
        /// 计算每个子按钮到主按钮的距离(按钮越多距离越远)
        /// </summary>
        /// <param name="diameter">子按钮直径</param>
        /// <param name="count">子按钮数量</param>
        /// <returns></returns>
        private int GetDisrance(int diameter, int count)
        {
            int ret = 0;
            if (count <= 2)
            {
                ret = (int)(diameter * 1.2);
            }
            else
            {
                double partDegree = 0;
                partDegree = 90 / (count - 1);
                partDegree = partDegree / 2;
                ret = (int)((diameter / (2 * Math.Sin((Math.PI / 180) * partDegree))) * 1.2);
            }

            return ret;
        }

        /// <summary>
        /// 计算每个子按钮到主按钮的偏移
        /// </summary>
        /// <param name="distance">子按钮到主按钮的距离</param>
        /// <param name="position">第几个子按钮 从0开始</param>
        /// <param name="count">一共多少个子按钮</param>
        /// <returns>(x坐标偏移，y坐标偏移)</returns>
        private (int, int) GetOffset(int distance, int position, int count)
        {
            double partDegree = 0;
            if (count > 0)
            {
                partDegree = 90 / (count - 1);
            }
            double degree = position * partDegree;
            //角度转弧度再计算sin
            double temp = Math.Sin((Math.PI / 180) * degree);
            int x = (int)(temp * distance);
            //角度转弧度再计算cos
            temp = Math.Cos((Math.PI / 180) * degree);
            int y = (int)(temp * distance);
            return (x, y);
        }

        private void MenuBtn_Clicked(object sender, EventArgs e)
        {
            isOpen = !isOpen;
            Splash();
        }

        /// <summary>
        /// 散开/闭合 动画
        /// </summary>
        private async void Splash()
        {
            if (isOpen)
            {
                // animation
                foreach (var item in subButtons)
                {
                    await item.ScaleTo(1, 100);
                }
            }
            else
            {
                // animation
                foreach (var item in subButtons)
                {
                    await item.ScaleTo(0, 100);
                }
            }
        }
    }
}