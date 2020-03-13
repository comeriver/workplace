// CLASS.VB KODLARI 
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace advanced_ckey
{
    public class Keyboard
    {

       
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern int SetWindowsHookEx(int Hook, KDel KeyDelegate, int HMod, int ThreadId);
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern int CallNextHookEx(int Hook, int nCode, int wParam, ref KeyStructure lParam);
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern int UnhookWindowsHookEx(int Hook);
    private delegate int KDel(int nCode, int wParam, ref KeyStructure lParam);
    public  event DownEventHandler Down;

    public  delegate void DownEventHandler(string Key);

    public  event UpEventHandler Up;

    public  delegate  void UpEventHandler(string Key);


        private static int Key;

        private static KDel KHD;

        private struct KeyStructure
        {
            public int Code;
            public int ScanCode;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        public void CreateHook()
        {
            KHD = new KDel(Proc);
            Key = SetWindowsHookEx(13, KHD, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
        }

        private int Proc(int Code, int wParam, ref KeyStructure lParam)
        {
            if (Code == 0)
            {
                switch (wParam)
                {
                    case 0x100:
                    case 0x104:
                        {
                            Down?.Invoke(Feed((Keys)lParam.Code));
                            break;
                        }

                    case 0x101:
                    case 0x105:
                        {
                            Up?.Invoke(Feed((Keys)lParam.Code));
                            break;
                        }
                }
            }

            return CallNextHookEx(Key, Code, wParam, ref lParam);
        }

        public void DiposeHook()
        {
            UnhookWindowsHookEx(Key);
        }

        private string Feed(Keys e)
        {
    
            switch (e)
            {
                case object _ when 65 <= Convert.ToInt32(e) && Convert.ToInt32(e) <= 90:
                    {
                        if (Control.IsKeyLocked(Keys.CapsLock) | (Control.ModifierKeys & Keys.Shift) != 0)
                        {
                            return e.ToString();
                        }
                        else
                        {
                            return e.ToString().ToLower();
                        }
                    }

               

                case object _ when 48 <= Convert.ToInt32(e) && Convert.ToInt32(e) <= 57:
                    {
                        if ((Control.ModifierKeys & Keys.Shift) != 0)
                        {
                            var switchExpr = e.ToString();
                            switch (switchExpr)
                            {
                                case "D1":
                                    {
                                        return "!";
                                    }

                                case "D2":
                                    {
                                        return "@";
                                    }

                                case "D3":
                                    {
                                        return "#";
                                    }

                                case "D4":
                                    {
                                        return "$";
                                    }

                                case "D5":
                                    {
                                        return "%";
                                    }

                                case "D6":
                                    {
                                        return "^";
                                    }

                                case "D7":
                                    {
                                        return "&";
                                    }

                                case "D8":
                                    {
                                        return "*";
                                    }

                                case "D9":
                                    {
                                        return "(";
                                    }

                                case "D0":
                                    {
                                        return ")";
                                    }
                            }
                        }
                        else
                        {
                            return e.ToString().Replace("D", null);
                        }

                        break;
                    }

                case object _ when 96 <= Convert.ToInt32(e) && Convert.ToInt32(e) <= 105:
                    {
                        return e.ToString().Replace("NumPad", null);
                    }

                case object _ when 106 <= Convert.ToInt32(e) && Convert.ToInt32(e) <= 111:
                    {
                        var switchExpr1 = e.ToString();
                        switch (switchExpr1)
                        {
                            case "Divide":
                                {
                                    return "/";
                                }

                            case "Multiply":
                                {
                                    return "*";
                                }

                            case "Subtract":
                                {
                                    return "-";
                                }

                            case "Add":
                                {
                                    return "+";
                                }

                            case "Decimal":
                                {
                                    return ".";
                                }
                        }

                        break;
                    }

                case object _ when 32 == Convert.ToInt32(e):
                    {
                        return " ";
                    }

                case object _ when 8 == Convert.ToInt32(e):
                    {
                        return " <Backspace> ";
                    }

                case object _ when 20 == Convert.ToInt32(e): //return null for capslock
                    {
                        return null;
                    }

                case object _ when 33 == Convert.ToInt32(e): //works as pageup
                    {
                        return null;
                    }

                case object _ when 34 == Convert.ToInt32(e): //works as pagedown
                    {
                        return null;
                    }
                case object _ when 35 == Convert.ToInt32(e): //works as  next
                    {
                        return null;
                    }
                case object _ when 36 == Convert.ToInt32(e): //works as end
                    {
                        return null;
                    }
                case object _ when 37 == Convert.ToInt32(e): //works as home 
                    {
                        return null;
                    }
                case object _ when 38 == Convert.ToInt32(e): //works as left
                    {
                        return null;
                    }
                case object _ when 39 == Convert.ToInt32(e): //works as Right
                    {
                        return null;
                    }
                case object _ when 40 == Convert.ToInt32(e): //works as Down
                    {
                        return null;
                    }
                case object _ when 41 == Convert.ToInt32(e): //works as select
                    {
                        return null;
                    }
                case object _ when 42 == Convert.ToInt32(e): //works as print
                    {
                        return null;
                    }
                case object _ when 43 == Convert.ToInt32(e): //works as execute
                    {
                        return null;
                    }
                case object _ when 44 == Convert.ToInt32(e): //works as snapshot
                    {
                        return null;
                    }
                case object _ when 45 == Convert.ToInt32(e): //works as insert
                    {
                        return null;
                    }
              
                case object _ when 47 == Convert.ToInt32(e): //works as help
                    {
                        return null;
                    }


                case object _ when 160 == Convert.ToInt32(e):
                    {
                        return null;
                    }

                case object _ when 161 == Convert.ToInt32(e):
                    {
                        return null;
                    }

                case object _ when 186 <= Convert.ToInt32(e) && Convert.ToInt32(e) <= 222:
                    {
                        if ((Control.ModifierKeys & Keys.Shift) != 0)
                        {
                            var switchExpr2 = e.ToString();
                            switch (switchExpr2)
                            {
                                case "OemMinus":
                                    {
                                        return "_";
                                    }

                                case "Oemplus":
                                    {
                                        return "+";
                                    }

                                case "OemOpenBrackets":
                                    {
                                        return "{";
                                    }

                                case "Oem6":
                                    {
                                        return "}";
                                    }

                                case "Oem5":
                                    {
                                        return "|";
                                    }

                                case "Oem1":
                                    {
                                        return ":";
                                    }

                                case "Oem7":
                                    {
                                        return "\"";
                                    }

                                case "Oemcomma":
                                    {
                                        return "<";
                                    }

                                case "OemPeriod":
                                    {
                                        return ">";
                                    }

                                case "OemQuestion":
                                    {
                                        return "?";
                                    }

                                case "Oemtilde":
                                    {
                                        return "~";
                                    }
                            }
                        }
                        else
                        {
                            var switchExpr3 = e.ToString();
                            switch (switchExpr3)
                            {
                                case "OemMinus":
                                    {
                                        return "-";
                                    }

                                case "Oemplus":
                                    {
                                        return "=";
                                    }

                                case "OemOpenBrackets":
                                    {
                                        return "[";
                                    }

                                case "Oem6":
                                    {
                                        return "]";
                                    }

                                case "Oem5":
                                    {
                                        return "";
                                    }

                                case "Oem1":
                                    {
                                        return ";";
                                    }

                                case "Oem7":
                                    {
                                        return "'";
                                    }

                                case "Oemcomma":
                                    {
                                        return ",";
                                    }

                                case "OemPeriod":
                                    {
                                        return ".";
                                    }

                                case "OemQuestion":
                                    {
                                        return "/";
                                    }

                                case "Oemtilde":
                                    {
                                        return "`";
                                    }
                            }
                        }

                        break;
                    }

                case var @case when @case == Keys.Return:
                    {
                        return Environment.NewLine;
                    }

                default:
                    {
                        return "<" + e.ToString() + ">";
                    }
            }

            return null;
        }
    }
}