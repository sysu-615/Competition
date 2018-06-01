using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using System;
using System.Diagnostics;
// [图片]https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace Competition.Views
{
    public sealed partial class RegistDialog : ContentDialog
    {
        public RegistDialog()
        {
            this.InitializeComponent();
        }

        private string usernameError = "";
        private string passwordError = "";
        private string repeatPasswordError = "";
        private string emailError = "";

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!errorMessage.Text.Equals("") && userName.Text!="" && password1.Password != "" && password2.Password != "" && email.Text != "")
            {
                MessageDialog dialog=new MessageDialog("请正确填写信息后再进行注册");
                await dialog.ShowAsync();
                return;
            }
            bool RegisterInfo = await Internet.API.GetAPI().Register(userName.Text, password1.Password, email.Text);
            if (RegisterInfo == true)
            {
                await new MessageDialog("注册成功").ShowAsync();
                MainPage.Curr.ContentFrame.Navigate(typeof(Home));
                MainPage.Curr.UserInfoState.ShowAt(MainPage.Curr.LogInLogOut);
                MainPage.Curr.Login_Flyout.ShowAt(MainPage.Curr.LogInLogOut);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            MainPage.Curr.ContentFrame.Navigate(typeof(Home));
            MainPage.Curr.ContentFrame.Navigate(typeof(Home));
            MainPage.Curr.UserInfoState.ShowAt(MainPage.Curr.LogInLogOut);
            MainPage.Curr.Login_Flyout.ShowAt(MainPage.Curr.LogInLogOut);
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var username = (sender as TextBox).Text;
            var validate = new Regex(@"^[a-zA-Z][a-zA-Z0-9]{5,15}$");
            if (!validate.IsMatch(username))
            {
                //在这里显示错误提示
                usernameError = "用户名只能包含数字与字母且应该以字母开头，长度应该为6-16\n";
            }
            else
            {
                //这里是用户名符合规则
                usernameError = "";
            }
            errorMessage.Text = usernameError + passwordError + repeatPasswordError + emailError;
        }

        private void password1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = password1.Password;
            var validate = new Regex(@"^[a-zA-Z](\S){5,15}$");
            if (!validate.IsMatch(password))
            {
                //在这里显示错误提示
                passwordError = "密码应该以字母开头，且长度不能小于6位\n";
            }
            else
            {
                //这里是密码检测符合规则
                passwordError = "";
            }
            errorMessage.Text = usernameError + passwordError + repeatPasswordError + emailError;
        }

        private void password2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password1.Password != password2.Password)
            {
                //在这里显示错误提示
                repeatPasswordError = "两次输入的密码不一致\n";
            }
            else
            {
                //这里是密码重复检测符合规则
                repeatPasswordError = "";
            }
            errorMessage.Text = usernameError + passwordError + repeatPasswordError + emailError;
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            var email = (sender as TextBox).Text;
            var validate = new Regex(@"^(www.)?[0-9a-zA-Z]{2,13}@[a-zA-Z0-9]{1,4}\.[a-zA-Z0-9]{1,4}$");
            if (!validate.IsMatch(email))
            {
                //在这里显示错误提示
                emailError = "邮箱格式不正确\n";
            }
            else
            {
                //这里是用户名符合规则
                emailError = "";
            }
            errorMessage.Text = usernameError + passwordError + repeatPasswordError + emailError;
        }
    }
}
