using Android.App;
using Android.Widget;
using Android.OS;

using System.Threading.Tasks;
using Android.Views;
using System;

namespace Calc_Xamarin
{
    [Activity(Label = "Calc_Xamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private float summ = 0;                                                       // хранение первого операнда
        private float bb;                                                             // действие
        private float mem;                                                            // значение в памяти
        private string Act;                                                           // действие
        private bool reset;                                                           // отвечает за сброс при наборе следующей цифры после операции
        private bool lastActEqu;                                                      // действие
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            ActionBar.Hide();
            SetContentView(Resource.Layout.Main);
            Act = "";                                                                 
            reset = false;                                                            
            FindViewById<Button>(Resource.Id.button1).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button2).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button3).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button4).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button5).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button6).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button7).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button8).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button9).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.button0).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.buttonPlMn).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.buttonDot).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.buttonAC).Click += Btn_Click;
            FindViewById<Button>(Resource.Id.buttonEqu).Click += Btn_ClickAct;       // Арифмитические действия
            FindViewById<Button>(Resource.Id.buttonPlus).Click += Btn_ClickAct;
            FindViewById<Button>(Resource.Id.buttonMinus).Click += Btn_ClickAct;
            FindViewById<Button>(Resource.Id.buttonDiv).Click += Btn_ClickAct;
            FindViewById<Button>(Resource.Id.buttonMult).Click += Btn_ClickAct;
            FindViewById<Button>(Resource.Id.buttonSq).Click += Btn_ClickAct;
            FindViewById<Button>(Resource.Id.buttonMR).Click += Btn_ClickMem;         //Работа с памятью
            FindViewById<Button>(Resource.Id.buttonMP).Click += Btn_ClickMem;
            FindViewById<Button>(Resource.Id.buttonMC).Click += Btn_ClickMem;

            /*   if (WindowManager.DefaultDisplay.Rotation== SurfaceOrientation.Rotation0 || 
                   WindowManager.DefaultDisplay.Rotation == SurfaceOrientation.Rotation180)
                   {
                   FindViewById<GridLayout>(Resource.Id.gridLayout1).ScaleX= (float )Resources.DisplayMetrics.WidthPixels /600;
                   FindViewById<GridLayout>(Resource.Id.gridLayout1).ScaleY = (float )Resources.DisplayMetrics.WidthPixels / 600;

               }*/

        }



        private void Clr()                                                          // очистка строки ввода
        {
            FindViewById<EditText>(Resource.Id.editText1).Text = "";
        }

        private float Calculate(float Num)                                          // арифм. действия - математика
        {
            switch (Act)
            {
                case "":
                    summ = Num;

                    break;
                case "+":
                    summ += Num;
                    break;
                case "-":
                    summ -= Num;
                    break;
                case "*":
                    summ *= Num;
                    break;
                case "/":
                    summ /= Num;
                    break;
                case "=":
                    summ = Num;
                    break;
                    case "S":
                    summ = (float )Math.Sqrt(Num);
                    break;

            }
            return summ;
        }

        private async void Btn_ClickMem(object sender, EventArgs e)                 //Запоминание M+ МС MR
        {
            var a = (sender as Button).Background;
            (sender as Button).SetBackgroundResource(Resource.Drawable.btbpu);      
            await Task.Delay(200);
            (sender as Button).SetBackgroundDrawable(a);

                switch ((sender as Button).Id)
                {
                    case Resource.Id.buttonMR:

                        FindViewById<EditText>(Resource.Id.editText1).Text = mem.ToString();

                        break;
                    case Resource.Id.buttonMP:


                        mem=float.Parse(FindViewById<EditText>(Resource.Id.editText1).Text);

                        break;
                    case Resource.Id.buttonMC:

                    mem = 0;
                        break;
                  
                
            }
        }

        private async void Btn_ClickAct(object sender, EventArgs e)                 // выбор действия при нажатии
        {
            var a = (sender as Button).Background;
            (sender as Button).SetBackgroundResource(Resource.Drawable.btbpu);      // отображение нажатия
            await Task.Delay(200);
            (sender as Button).SetBackgroundDrawable(a);
            if (!reset)
            {
                bb = float.Parse(FindViewById<EditText>(Resource.Id.editText1).Text);
            }

            if ((bb == 0) && (Act=="/"))
            {
                FindViewById<EditText>(Resource.Id.editText1).Text = "Err-/0";      // деление на 0
                Act = ""; summ = 0;
                reset = true;
            }
            else
            {
                switch ((sender as Button).Id)
                {
                    case Resource.Id.buttonEqu:
                        Calculate(bb);
                        // Act = "=";
                        lastActEqu = true;
                        FindViewById<EditText>(Resource.Id.editText1).Text = summ.ToString();
                        reset = true;
                        break;
                    case Resource.Id.buttonPlus:

                        Calculate(bb);
                        Act = "+";
                        FindViewById<EditText>(Resource.Id.editText1).Text = summ.ToString();
                        reset = true;
                        break;
                    case Resource.Id.buttonMinus:

                        Calculate(bb);
                        Act = "-";
                        FindViewById<EditText>(Resource.Id.editText1).Text = summ.ToString();
                        reset = true;
                        break;
                    case Resource.Id.buttonMult:

                        Calculate(bb);
                        Act = "*";
                        FindViewById<EditText>(Resource.Id.editText1).Text = summ.ToString();
                        reset = true;
                        break;
                    case Resource.Id.buttonDiv:

                        Calculate(bb);
                        Act = "/";
                        FindViewById<EditText>(Resource.Id.editText1).Text = summ.ToString();
                        reset = true;

                        break;
                    case Resource.Id.buttonSq:
                        Act = "S";
                        Calculate(float.Parse(FindViewById<EditText>(Resource.Id.editText1).Text));
                        Act = "";
                        FindViewById<EditText>(Resource.Id.editText1).Text = summ.ToString();
                        reset = true;

                        break;
                }
            }
        }

        private async void Btn_Click(object sender, System.EventArgs e)                 // ввод переменных
        {

            var a = (sender as Button).Background;
            (sender as Button).SetBackgroundResource(Resource.Drawable.btbpu);
            await Task.Delay(200);
            (sender as Button).SetBackgroundDrawable(a);
           if (reset)
            {
        
                    FindViewById<EditText>(Resource.Id.editText1).Text = "0";
                    reset = false;
                if (lastActEqu)
                {
                    Act = "";
                    lastActEqu = false;
                }
                
            }
            switch ((sender as Button).Id)
            {
                case Resource.Id.button1:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "1";
                    break;
                case Resource.Id.button2:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "2";
                    break;
                case Resource.Id.button3:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "3";
                    break;
                case Resource.Id.button4:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "4";
                    break;
                case Resource.Id.button5:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "5";
                    break;
                case Resource.Id.button6:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "6";
                    break;
                case Resource.Id.button7:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "7";
                    break;
                case Resource.Id.button8:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "8";
                    break;
                case Resource.Id.button9:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "9";
                    break;
                case Resource.Id.button0:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text == "0") Clr();
                    FindViewById<EditText>(Resource.Id.editText1).Text += "0";
                    break;
                case Resource.Id.buttonDot:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text != "0")
                    if (!FindViewById<EditText>(Resource.Id.editText1).Text.Contains(",")) 
                            FindViewById<EditText>(Resource.Id.editText1).Text += ",";
                    break;
                case Resource.Id.buttonPlMn:
                    if (FindViewById<EditText>(Resource.Id.editText1).Text != "0")
                        FindViewById<EditText>(Resource.Id.editText1).Text = (float.Parse(FindViewById<EditText>(Resource.Id.editText1).Text) * (-1)).ToString();
                    break;
                case Resource.Id.buttonAC:

                    FindViewById<EditText>(Resource.Id.editText1).Text = "0";
                    summ = 0;
                    Act = "";
                    break;
            }
        }
    }
}

