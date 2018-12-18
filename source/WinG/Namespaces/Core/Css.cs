using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinG.Core
{
    public static class Css
    {
        public static string type;

        public static void LoadStyleFile(IntPtr hwnd, string name)
        {
            StringBuilder Buff = new StringBuilder(256);
            if (Core.GetClassName(hwnd, Buff, 256) > 0){ type = Buff.ToString(); }
            int counter = 0;
            string line;

            StreamReader file = new StreamReader(name);
            while ((line = file.ReadLine()) != null)
            {
                string[] arr = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                switch (arr[0])
                {
                    case "background-color":
                        switch (type)
                        {
                            case "W":
                                Window ctrl = new Window();
                                ctrl.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            default:

                                break;
                        }
                        break;
                    case "font-size":
                        switch (type)
                        {
                            case "Button":
                                Button ctrl = new Button();
                                ctrl.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            case "Label":
                                Label label = new Label();
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
                                Button ctrl = new Button();
                                ctrl.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            case "Label":
                                Label label = new Label();
                                label.Handle = hwnd;
                                //ctrl.BackColor = ColorLoader.FromName(arr[1].Remove(arr[1].Length - 1).Replace(" ", ""));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        MessageBox.Show(string.Format("Error on line: ", counter), "Error", MessageBoxType.Error);
                        break;
                }
                counter++;
            }
            file.Close();
        }
    }
}
