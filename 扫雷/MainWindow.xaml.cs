using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace 扫雷
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
#pragma warning disable
    public partial class MainWindow : Window
    {
        public int lieint = 16;
        int[,] lints = new int[16, 16];
        Label[,] llabs = new Label[16, 16];
        List<Button> buttons = new List<Button>();
        public double lwidth = 25;
        public int lnum = 40;
        public DoubleAnimation broken = new DoubleAnimation();
        public DispatcherTimer timer1 = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1), IsEnabled = false, };
        public MainWindow()
        {
            InitializeComponent();

            broken.From = 1;
            broken.To = 1.2;
            broken.Duration = TimeSpan.FromSeconds(0.5);
            broken.Completed += Broken_Completed;
            lnumlab.Content = lnum.ToString();
            timer1.Tick += Timer1_Tick;

            for (int i = 0; i < lints.GetLength(0); i++)
            {
                for (int ii = 0; ii < lints.GetLength(1); ii++)
                {
                    lints[i, ii] = 0;
                }
            }
            for (int i = 0; i < lnum;)
            {
                
                int a = random.Next(lieint);
                int b = random.Next(lieint);
                //MessageBox.Show(a.ToString()+","+b.ToString());
                if (lints[a,b] != 1)
                {
                    lints[a, b] = 1;
                    for (int x = a - 1; x < a + 2; x++)
                    {
                        for (int y = b - 1; y < b + 2; y++)
                        {
                            try
                            {
                                if (llabs[x, y] == null)
                                {
                                    Label llabtip = new Label();
                                    llabtip.HorizontalAlignment = HorizontalAlignment.Left;
                                    llabtip.VerticalAlignment = VerticalAlignment.Top;
                                    llabtip.HorizontalContentAlignment = HorizontalAlignment.Center;
                                    llabtip.VerticalContentAlignment = VerticalAlignment.Center;
                                    llabtip.Width = lwidth;
                                    llabtip.Height = lwidth;
                                    //llabtip.Background = Brushes.SkyBlue;
                                    llabtip.BorderThickness = new Thickness(1);
                                    llabtip.Padding = new Thickness(0);
                                    llabtip.BorderBrush = Brushes.Black;
                                    llabtip.Content = "1";
                                    //llabtip.Foreground = Brushes.Gray;
                                    llabtip.Margin = new Thickness(x * lwidth, y * lwidth, 0, 0);
                                    llabs[x, y] = llabtip;
                                }
                                else
                                {
                                    llabs[x, y].Content = (int.Parse(llabs[x, y].Content.ToString()) + 1).ToString();
                                }
                            }
                            catch (Exception ex) { }
                        }
                    }
                    /*for (int x = b - 1; x < b+2; x += 2)
                    {
                        try
                        {
                            if (llabs[a, x] == null)
                            {
                                Label llabtip = new Label();
                                llabtip.HorizontalAlignment = HorizontalAlignment.Left;
                                llabtip.VerticalAlignment = VerticalAlignment.Top;
                                llabtip.HorizontalContentAlignment = HorizontalAlignment.Center;
                                llabtip.VerticalContentAlignment = VerticalAlignment.Center;
                                llabtip.Width = lwidth;
                                llabtip.Height = lwidth;
                                llabtip.Content = "1";
                                llabtip.Background = Brushes.Yellow;
                                llabtip.Margin = new Thickness(a * lwidth, x * lwidth, 0, 0);
                                llabs[a, x] = llabtip;
                            }
                            else
                            {
                                llabs[a, x].Content = (int.Parse(llabs[a, x].Content.ToString()) + 1).ToString();
                            }
                        }
                        catch(Exception ex) { }
                    }*/
                    i++;
                }
                else
                {
                }
            }

            foreach (Label i in llabs)
            {
                if (i != null)
                    ft1.Children.Add(i);
            }

            for (int i = 0; i < lints.GetLength(0); i++)
            {
                for (int ii = 0; ii < lints.GetLength(1); ii++)
                {
                    Button lbtn = new Button();
                    lbtn.HorizontalAlignment = HorizontalAlignment.Left;
                    lbtn.VerticalAlignment = VerticalAlignment.Top;
                    lbtn.Width = lwidth;
                    lbtn.Height = lwidth;
                    lbtn.Margin = new Thickness(ii * lwidth, i * lwidth, 0, 0);
                    lbtn.Tag = new Point(ii, i);
                    if (lints[ii,i] == 1)
                    {
                        lbtn.Content = "雷";
                        lbtn.Foreground = Brushes.Transparent;
                        lbtn.Click += Lbtn_Click;
                        
                    }
                    else
                    {
                        lbtn.Click += Lbtn_Click1;
                    }
                    lbtn.Background = Brushes.Silver;
                    Rectangle rr = new Rectangle { Fill = Brushes.White };
                    Trigger tr = new Trigger { Property = Button.IsMouseOverProperty, Value = true };
                    tr.Setters.Add(new Setter
                    {
                        Property = Ellipse.FillProperty,
                        Value = new SolidColorBrush(Colors.AliceBlue)
                    });
                    
                    lbtn.MouseRightButtonUp += Lbtn_MouseRightButtonUp;
                    lbtn.SetValue(Button.StyleProperty, Application.Current.Resources["MyButton"]);
                    buttons.Add(lbtn);
                    ft1.Children.Add(lbtn);
                }
            }

        }

        TimeSpan times = new TimeSpan(0);
        private void Timer1_Tick(object sender, EventArgs e)
        {
            times = new TimeSpan(0, 0, 0, (int)times.TotalSeconds + 1);
            timelab.Content = times.ToString();
        }

        private void Broken_Completed(object sender, EventArgs e)
        {
            Timeline name = ((AnimationClock)sender).Timeline;

        }

        private void Lbtn_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Button i = sender as Button;
            if (i.Background == Brushes.Red)
            {
                lnumlab.Content = (int.Parse(lnumlab.Content.ToString()) + 1).ToString();
                i.Background = Brushes.Silver;
            }
            else
            {
                if (int.Parse(lnumlab.Content.ToString()) > 0)
                {
                    lnumlab.Content = (int.Parse(lnumlab.Content.ToString()) - 1).ToString();
                    i.Background = Brushes.Red;
                }
            }


            int restbtnnums = 0;
            foreach (Button b in buttons)
            {
                if (b.Background == Brushes.Red)
                {
                    restbtnnums++;
                }
                if (b != null)
                {
                    restbtnnums++;
                }
            }
            //restartbtn.Content = restbtnnums.ToString();
            if ((float)restbtnnums/2 == lnum &&lnumlab.Content.ToString() == "0")
            {
                timer1.Stop();
                restartbtn.Content = ": D";
                MessageBox.Show("YOU WIN !!!\n:D");
                
            }
        }

        List<Label> labels = new List<Label>();
        bool findlabbytag(Point findpars)
        {
            foreach (Label i in labels)
            {
                Point point = (Point)i.Tag;
                if (point == findpars)
                {
                    i.Content = (int.Parse(i.Content.ToString()) + 1).ToString();
                    return true;
                }
            }
            return false;
        }
        Button findbtnbytag(Point findpars)
        {
            foreach(Button i in buttons)
            {
                //MessageBox.Show(((Point)i.Tag).ToString()+"\n"+findpars.ToString());
                if (i.Tag != null && i.Tag is Point && (Point)i.Tag == findpars)
                    return i;
            }
            return null;
        }
        private async void Lbtn_Click1(object sender, RoutedEventArgs e)
        {
            timer1.IsEnabled = true;
            Button sendbtn = sender as Button;
            Point sendpo = (Point)sendbtn.Tag;
            breakbtns(sendpo);
            ScaleEasingAnimationShow(sendbtn, 1, 2, 1, new TimeSpan(0, 0, 1));
            //sendbtn.Click -= Lbtn_Click1;
        }

        List<Point> alllast = new List<Point>();
        public void breakbtns(Point sendpo,int disabletype = -1)
        {
            if (alllast.Contains(sendpo))
                return;
            Point mysendpo = sendpo;
            int turnleft = (int)mysendpo.X;
            int turnright = (int)mysendpo.X;
            int turnup = (int)mysendpo.Y;
            int turndown = (int)mysendpo.Y;
            //while (turnleft != 0 || turnright != 9 || turnup != 0 || turndown != 9)
            //{
            alllast.Add(sendpo);
                if (turnleft != 0)
                {
                //MessageBox.Show(sendpo.ToString());
                    turnleft -= 1;
                Button findbtnl = findbtnbytag(new Point(turnleft, mysendpo.Y));
                Button findbtnld = findbtnbytag(new Point(turnleft, mysendpo.Y + 1)) != null ? findbtnbytag(new Point(turnleft, mysendpo.Y + 1)) : new Button();
                Button findbtnlu = findbtnbytag(new Point(turnleft, mysendpo.Y - 1)) != null ? findbtnbytag(new Point(turnleft, mysendpo.Y - 1)) : new Button();
                if (findbtnl != null)
                    { 
                        if (findbtnl.Content != "雷" && findbtnld.Content != "雷" && findbtnlu.Content != "雷")
                        {

                            breakbtns((Point)findbtnl.Tag);
                        ScaleEasingAnimationShow(findbtnl, 1, 2, 1, new TimeSpan(0, 0, 1));

                        }
                        else
                        {
                            turnleft = 0;
                            turnright = 9;
                            turnup = 0;
                            turndown = 9;
                        }
                    }
                }
                if (turnright != 9)
                {
                    turnright += 1;
                Button findbtnl = findbtnbytag(new Point(turnright, mysendpo.Y));
                Button findbtnld = findbtnbytag(new Point(turnleft, mysendpo.Y + 1)) != null ? findbtnbytag(new Point(turnleft, mysendpo.Y + 1)) : new Button();
                Button findbtnlu = findbtnbytag(new Point(turnleft, mysendpo.Y - 1)) != null ? findbtnbytag(new Point(turnleft, mysendpo.Y - 1)) : new Button();
                if (findbtnl != null)
                    {
                        if (findbtnl.Content != "雷" && findbtnld.Content != "雷" && findbtnlu.Content != "雷")
                        {
                            breakbtns((Point)findbtnl.Tag);
                        ScaleEasingAnimationShow(findbtnl, 1, 2, 1, new TimeSpan(0, 0, 1));
                    }
                        else
                        {
                            turnleft = 0;
                            turnright = 9;
                            turnup = 0;
                            turndown = 9;
                        }
                    }
                }
                if (turnup != 0)
                {
                    turnup -= 1;
                    Button findbtnl = findbtnbytag(new Point(mysendpo.X, turnup));
                Button findbtnul = findbtnbytag(new Point(mysendpo.X - 1,turnup)) != null ? findbtnbytag(new Point(mysendpo.X - 1, turnup)) : new Button();
                Button findbtnur = findbtnbytag(new Point(mysendpo.X + 1, turnup)) != null ? findbtnbytag(new Point(mysendpo.X + 1, turnup)) : new Button();

                if (findbtnl != null)
                    {
                        if (findbtnl.Content != "雷" && findbtnul.Content != "雷" && findbtnur.Content != "雷")
                        {
                            breakbtns((Point)findbtnl.Tag);
                        ScaleEasingAnimationShow(findbtnl, 1, 2, 1, new TimeSpan(0, 0, 1));
                    }
                        else
                        {
                            turnleft = 0;
                            turnright = 9;
                            turnup = 0;
                            turndown = 9;
                        }
                    }
                }
                if (turndown != 9)
                {
                    turndown += 1;
                    Button findbtnl = findbtnbytag(new Point(mysendpo.X, turndown));
                Button findbtnul = findbtnbytag(new Point(mysendpo.X - 1, turnup)) != null ? findbtnbytag(new Point(mysendpo.X - 1, turnup)) : new Button();
                Button findbtnur = findbtnbytag(new Point(mysendpo.X + 1, turnup)) != null ? findbtnbytag(new Point(mysendpo.X + 1, turnup)) : new Button();
                if (findbtnl != null)
                    {
                        if (findbtnl.Content != "雷" && findbtnul.Content != "雷" && findbtnur.Content != "雷")
                        {
                            breakbtns((Point)findbtnl.Tag);
                        ScaleEasingAnimationShow(findbtnl, 1, 2, 1, new TimeSpan(0, 0, 1));
                    }
                        else
                        {
                            turnleft = 0;
                            turnright = 9;
                            turnup = 0;
                            turndown = 9;
                        }
                    }

               // }
            }
        }
        Random random = new Random();
        public void ScaleEasingAnimationShow(UIElement element, double Sizefrom, double Sizeto, int power, TimeSpan time)
        {
            if ((element as Button).Tag == null)
                return;
            (element as Button).Click -= Lbtn_Click1;
            ScaleTransform scale = new ScaleTransform();  //旋转
            Panel.SetZIndex(element, 1);
            element.RenderTransform = scale;
            //定义圆心位置
            element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            //定义过渡动画,power为过度的强度
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
                Power = power
                //Power = 0
            };
            
            time = TimeSpan.FromMilliseconds(random.Next((int)time.TotalMilliseconds - 600, (int)time.TotalMilliseconds + 600));
            DoubleAnimation scaleAnimation = new DoubleAnimation()
            {
                From = Sizefrom,                                   //起始值
                To = Sizeto,                                     //结束值
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,                                 //动画播放时间
                
                SpeedRatio = ((float)random.Next(30,100))/100
            };
            
            DoubleAnimation opAnimation = new DoubleAnimation()
            {
                From = 1,                                   //起始值
                To = 0,                                     //结束值
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,                                 //动画播放时间
            };
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTargetProperty(opAnimation, new PropertyPath(Button.OpacityProperty));
            storyboard.Children.Add(opAnimation);
            scaleAnimation.Name = "s" + (element as Button).Tag.ToString().Replace(",","p");
            scaleAnimation.Completed += (sender, e) =>
            {

                Timeline name = ((AnimationClock)sender).Timeline;
                
                Button btn = findbtnbytag(name.Name);
                if (btn != null)
                {//MessageBox.Show(Point.Parse(name.Name.Replace("s", "").Replace("p", ",")).ToString());
                    if (btn.Background == Brushes.Red)
                    {
                        lnumlab.Content = (int.Parse(lnumlab.Content.ToString()) + 1).ToString();
                    }
                    buttons.Remove(btn);
                    ft1.Children.Remove(btn);
                }
                
            };
            storyboard.Begin(element as Button,true);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            (element as Button).Tag = "s" + (element as Button).Tag.ToString().Replace(",", "p");
        }

        public Button findbtnbytag(string tag)
        {
            foreach (Button i in buttons)
            {
                if (i.Tag != null && i.Tag is string && i.Tag.ToString() == tag)
                {
                    return i;
                }
            }
            return null;
        }

        private void Lbtn_Click(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            restartbtn.Content = "X<";
            //MessageBox.Show("YOU DIE!!!");
            foreach (Button b in buttons)
            {
                if (b != null && b.Content != null)
                {
                    b.Foreground = Brushes.Purple;
                }
            }
            //restartbtn_Click(null, null);
        }

        private void restartbtn_Click(object sender, RoutedEventArgs e)
        {
            Window w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
