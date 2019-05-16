"# SplashButton" 
基于Xamain.Forms的散开按钮示例

使用步骤
1.xaml页面添加控件

<!--控件必须包含在RelativeLayout中，且RelativeLayout的大小必须足够按钮伸展-->
<RelativeLayout Grid.Row="1" HorizontalOptions="StartAndExpand" VerticalOptions="FillAndExpand">
    <component:SplashButton x:Name="menuBtn" RelativeLayout.YConstraint="{ConstraintExpression Type=RelativeToParent, Factor=.8,Property=Height,Constant=0}"/>
</RelativeLayout>

2.页面初始化时设置散开按钮的子按钮和图片

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
