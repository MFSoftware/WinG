using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinG
{
    public class Css
    {
        private static string type;
        public string Text;

        public static Css LoadStyleString(string test)
        {
            var style = new Css();
            style.Text = test;
            return style;
        }

        public static void LoadStyleFile(IntPtr hwnd, string name)
        {
            var Buff = new StringBuilder(256);
            if (Core.Core.GetClassName(hwnd, Buff, 256) > 0)
                type = Buff.ToString();
            var counter = 1;

            string line;
            var file = new StreamReader(name);
            while ((line = file.ReadLine()) != null)
            {
                var arr = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                switch (arr[0].ToLower())
                {
                    case "background-color":
                        switch (type)
                        {
                            case "W":
                                var ctrl = new Window();
                                ctrl.Handle = hwnd;
                                if (!arr[1].Replace(" ", "").StartsWith("#"))
                                {
                                    ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                }
                                else if (arr[1].Replace(" ", "").StartsWith("#"))
                                {

                                }
                                else if (arr[1].Replace(" ", "").StartsWith("rgb(") && arr[1].Replace(" ", "").EndsWith(");"))
                                {

                                }
                                break;
                        }
                        break;
                    case "border-radius":
                        switch (type)
                        {
                            case "W":
                                var ctrl = new Window();
                                ctrl.Handle = hwnd;
                                if (arr[1].EndsWith("px;"))
                                {
                                    ctrl.BorderRadius = Int32.Parse(arr[1].Remove(arr[1].Length - 3).Replace(" ", ""));
                                }
                                break;
                            default:

                                break;
                        }
                        break;
                    case "padding-left":
                        switch (type)
                        {
                            case "Button":
                                var ctrl = new Control();
                                ctrl.Handle = hwnd;

                                if (Core.Core.IsWindow(ctrl.Parent))
                                {
                                    var win = new Window();
                                    win.Handle = ctrl.Parent;
                                    if (arr[1].EndsWith("px;"))
                                        ctrl.Left = Int32.Parse(arr[1].Remove(arr[1].Length - 3).Replace(" ", ""));
                                    else if (arr[1].EndsWith("%;"))
                                        ctrl.Left = (win.Width / 100) * Int32.Parse(arr[1].Remove(arr[1].Length - 2).Replace(" ", ""));
                                }
                                break;
                        }
                        break;
                    case "padding-top":
                        switch (type)
                        {
                            case "Button":
                            case "Static":
                                var c = new Control();
                                c.Handle = hwnd;
                                if (Core.Core.IsWindow(c.Parent))
                                {
                                    var win = new Window();
                                    win.Handle = c.Parent;
                                    if (arr[1].EndsWith("px;"))
                                        c.Top = Int32.Parse(arr[1].Remove(arr[1].Length - 3).Replace(" ", ""));
                                    else if (arr[1].EndsWith("%;"))
                                        c.Top = (win.Height / 100) * Int32.Parse(arr[1].Remove(arr[1].Length - 2).Replace(" ", ""));
                                }
                                break;
                            default:

                                break;
                        }
                        break;
                    case "font-size":
                        switch (type)
                        {
                            case "Button":
                                var btn = new Button();
                                btn.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            case "Label":
                                var label = new Label();
                                label.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "font-family":
                        switch (type)
                        {
                            case "Button":
                                var btnff = new Button();
                                btnff.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            case "Label":
                                var label = new Label();
                                label.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        MessageBox.Show(string.Format("Error on line {0} \nCSS properties with such a name '{1}' does not exist", counter, arr[0].ToLower()), "Error", MessageBoxType.IconError);
                        break;
                }
                counter++;
            }
            file.Close();
        }
    }
}
