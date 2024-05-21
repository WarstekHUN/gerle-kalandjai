//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Gerle_Lib.BaseClasses
//{
//    public class Menu
//    {
//        public static Menu actualMenu;

//        private string[] _fields;

//        private short _selectedField;

//        private Action[] _actions;

//        private Menu _parentMenu;

//        private bool _isOnMenu;

//        private bool _isCentered = true;

//        private static string _projectCreator = "";

//        private static List<Tuple<int, object>> _customData = new List<Tuple<int, object>>();

//        public string Field => _fields[_selectedField];

//        public string[] Fields => _fields;

//        public Action Action => _actions[_selectedField];

//        public bool IsOnMenu
//        {
//            get
//            {
//                return _isOnMenu;
//            }
//            set
//            {
//                _isOnMenu = value;
//            }
//        }

//        public Menu(string[] fields, Action[] actions, bool setToScreen = true, Menu parent = null)
//        {
//            if (_projectCreator == "")
//            {
//                throw new Exception("The project creator was not defined. Use 'Menu.SetCreator('your_creator_name');");
//            }

//            ConsoleListener.Run();
//            _fields = new string[fields.Length + 1];
//            _actions = new Action[actions.Length + 1];
//            for (int i = 0; i < fields.Length; i++)
//            {
//                _fields[i] = fields[i];
//                _actions[i] = actions[i];
//            }

//            _fields[fields.Length] = ((parent == null) ? "Quit" : "Back");
//            _actions[actions.Length] = Quit;
//            _selectedField = 0;
//            _parentMenu = parent;
//            if (setToScreen)
//            {
//                actualMenu = this;
//                Console.Clear();
//                Draw();
//            }
//        }

//        public static void Draw()
//        {
//            actualMenu.IsOnMenu = true;
//            if (_customData.Count > 0)
//            {
//                Console.Clear();
//            }

//            int windowWidth = Console.WindowWidth;
//            int windowHeight = Console.WindowHeight;
//            if (windowHeight < actualMenu._fields.Length * 2 + 1)
//            {
//                Console.WriteLine("The windows is too small to display the menu");
//                return;
//            }

//            int num = actualMenu.Fields.Length;
//            int num2 = windowHeight / 2 - num;
//            string[] fields = actualMenu.Fields;
//            foreach (string text in fields)
//            {
//                Console.SetCursorPosition(windowWidth / 2 - text.Length / 2, num2);
//                Console.BackgroundColor = ConsoleColor.Black;
//                Console.ForegroundColor = ConsoleColor.Black;
//                for (int j = 0; j < text.Length + 3; j++)
//                {
//                    Console.Write(" ");
//                }

//                Console.ResetColor();
//                Console.SetCursorPosition(windowWidth / 2 - text.Length / 2, num2);
//                if (actualMenu.Field == text)
//                {
//                    Console.BackgroundColor = ConsoleColor.White;
//                    Console.ForegroundColor = ConsoleColor.Black;
//                    Console.Write("-> " + text);
//                    Console.ResetColor();
//                }
//                else
//                {
//                    Console.Write("> " + text);
//                }

//                num2 += 2;
//            }

//            if (_customData == null)
//            {
//                return;
//            }

//            foreach (Tuple<int, object> customDatum in _customData)
//            {
//                if (customDatum == null)
//                {
//                    continue;
//                }

//                int item = customDatum.Item1;
//                object item2 = customDatum.Item2;
//                if (item2 != null)
//                {
//                    int left = windowWidth / 2 - item2.ToString().Length / 2;
//                    int num3 = ((item >= 0) ? (num2 - 1 + item) : (windowHeight / 2 - num + item));
//                    if (num3 >= 0 && num3 <= windowHeight)
//                    {
//                        Console.SetCursorPosition(left, num3);
//                        Console.Write(item2.ToString());
//                    }
//                }
//            }
//        }

//        public void SetToScreen()
//        {
//            actualMenu = this;
//            Console.Clear();
//            Draw();
//        }

//        public int GetMenuIndex(int y, int x)
//        {
//            int num = (y - Console.WindowHeight / 2 - Fields.Length) / 2 + Fields.Length;
//            if (num < 0 || num >= Fields.Length)
//            {
//                num = -1;
//            }

//            if (num != -1 && (x < Console.WindowWidth / 2 - Fields[num].Length / 2 || x > Console.WindowWidth / 2 + Fields[num].Length / 2))
//            {
//                num = -1;
//            }

//            return num;
//        }

//        public void SetField(short field)
//        {
//            if (field > _fields.Length - 1)
//            {
//                _selectedField = 0;
//            }
//            else if (field < 0)
//            {
//                _selectedField = (short)(_fields.Length - 1);
//            }
//            else
//            {
//                _selectedField = field;
//            }
//        }

//        public short GetField()
//        {
//            return _selectedField;
//        }

//        public static void SetCreator(string name)
//        {
//            _projectCreator = name;
//        }

//        public void ExecuteAction()
//        {
//            Console.Clear();
//            if (_actions[_selectedField] != null)
//            {
//                actualMenu.IsOnMenu = false;
//                _actions[_selectedField]();
//            }

//            if (!EventHandler.IsWaitingForCustomAction() && !actualMenu.IsOnMenu)
//            {
//                Console.WriteLine("Press a key or your mouse to continue...");
//                EventHandler.WaitForAction();
//            }
//        }

//        public void AddCustomData(int pos, object data)
//        {
//            _customData.Add(new Tuple<int, object>(pos, data));
//        }

//        public void RemoveCustomData(int pos)
//        {
//            _customData.RemoveAt(pos);
//        }

//        private void Quit()
//        {
//            if (_parentMenu != null)
//            {
//                actualMenu = _parentMenu;
//                Console.Clear();
//                Draw();
//            }
//            else
//            {
//                EndCredits();
//            }
//        }

//        public int GetFieldLength(int field)
//        {
//            return _fields[field].Length;
//        }

//        public void EndCredits()
//        {
//            int num = Console.WindowWidth / 2;
//            int num2 = Console.WindowHeight / 2;
//            string text = "A project by " + _projectCreator;
//            Console.SetCursorPosition(num - text.Length / 2, num2 - 1);
//            Console.ForegroundColor = ConsoleColor.DarkGray;
//            Console.Write("A project by ");
//            Console.ForegroundColor = ConsoleColor.White;
//            Console.WriteLine(_projectCreator);
//            Thread.Sleep(5000);
//            Environment.Exit(0);
//        }
//    }
//}