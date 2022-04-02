using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Macropse.Infrastructure.Module.Driver
{
    public class Device
    {
        private IntPtr _context;
        private Thread _callbackThread;

        private int _mouseId = 0xB;
        private int _keyBoardId = 0x2;

        public KeyboardFilterMode KeyboardFilterMode { get; set; }

        public MouseFilterMode MouseFilterMode { get; set; }

        public bool IsLoaded { get; private set; }

        public int KeyPressDelay { get; set; }
        public int ClickDelay { get; set; }
        public int ScrollDelay { get; set; }

        public event EventHandler<KeyPressedEventArgs> OnKeyPressed;
        public event EventHandler<MousePressedEventArgs> OnMousePressed;


        public Device()
        {
            _context = IntPtr.Zero;

            KeyboardFilterMode = KeyboardFilterMode.None;
            MouseFilterMode = MouseFilterMode.None;

            KeyPressDelay = 1;
            ClickDelay = 1;
            ScrollDelay = 15;
        }

        public bool TryLoad()
        {
            if (IsLoaded)
            {
                return false;
            }

            _context = InterceptionDriver.CreateContext();

            if (_context != IntPtr.Zero)
            {
                _callbackThread?.Join();
                _callbackThread = new Thread(DriverCallback)
                {
                    Priority = ThreadPriority.Highest,
                    IsBackground = true
                };
                _callbackThread.Start();

                IsLoaded = true;

                return true;
            }

            IsLoaded = false;

            return false;
        }

        public bool TryUnload()
        {
            if (!IsLoaded)
            {
                return false;
            }

            if (_context == IntPtr.Zero)
            {
                return false;
            }

            IsLoaded = false;

            InterceptionDriver.SetFilter(_context, InterceptionDriver.IsKeyboard, (ushort)KeyboardFilterMode.All);
            InterceptionDriver.SetFilter(_context, InterceptionDriver.IsMouse, (ushort)MouseFilterMode.All);

            _callbackThread.Join();
            InterceptionDriver.DestroyContext(_context);

            return true;
        }

        private void DriverCallback()
        {
            InterceptionDriver.SetFilter(_context, InterceptionDriver.IsKeyboard, (ushort)KeyboardFilterMode);
            InterceptionDriver.SetFilter(_context, InterceptionDriver.IsMouse, (ushort)MouseFilterMode);

            var stroke = new Stroke();
            int deviceId;

            while (InterceptionDriver.Receive(_context, deviceId = InterceptionDriver.Wait(_context), ref stroke, 1) > 0 && IsLoaded)
            {
                if (InterceptionDriver.IsMouse(deviceId) > 0)
                {
                    _mouseId = deviceId;

                    if (OnMousePressed != null)
                    {
                        var args = new MousePressedEventArgs
                        {
                            X = stroke.Mouse.X,
                            Y = stroke.Mouse.Y,
                            State = stroke.Mouse.State,
                            Rolling = stroke.Mouse.Rolling
                        };

                        OnMousePressed(this, args);

                        if (args.Handled)
                        {
                            continue;
                        }

                        stroke.Mouse.X = args.X;
                        stroke.Mouse.Y = args.Y;
                        stroke.Mouse.State = args.State;
                        stroke.Mouse.Rolling = args.Rolling;
                    }
                }

                if (InterceptionDriver.IsKeyboard(deviceId) > 0)
                {
                    _keyBoardId = deviceId;

                    if (OnKeyPressed != null)
                    {
                        var args = new KeyPressedEventArgs
                        {
                            Key = stroke.Key.Code,
                            State = stroke.Key.State
                        };

                        OnKeyPressed(this, args);

                        if (args.Handled)
                        {
                            continue;
                        }

                        stroke.Key.Code = args.Key;
                        stroke.Key.State = args.State;
                    }
                }

                InterceptionDriver.Send(_context, deviceId, ref stroke, 1);
            }
            if (!IsLoaded)
            {
                return;
            }
            TryUnload();
            throw new Exception("Interception.Receive() failed for an unknown reason. The driver has been unloaded.");
        }

        public void SendKey(Key key, KeyState state)
        {
            var stroke = new Stroke();
            var keyStroke = new KeyStroke
            {
                Code = key,
                State = state
            };

            stroke.Key = keyStroke;

            InterceptionDriver.Send(_context, _keyBoardId, ref stroke, 1);

            if (KeyPressDelay > 0)
            {
                Thread.Sleep(KeyPressDelay);
            }
        }

        public void SendKey(Key key)
        {
            SendKey(key, KeyState.Down);

            if (KeyPressDelay > 0)
            {
                Thread.Sleep(KeyPressDelay);
            }

            SendKey(key, KeyState.Up);
        }

        public void SendKeys(params Key[] keys)
        {
            foreach (var key in keys)
            {
                SendKey(key);
            }
        }

        public void SendMouseEvent(MouseStroke mouseStroke)
        {
            var stroke = new Stroke();

            stroke.Mouse = mouseStroke;

            InterceptionDriver.Send(_context, _mouseId, ref stroke, 1);
        }

        public void SendLeftClick()
        {
            SendMouseEvent(new MouseStroke() { State = MouseState.LeftDown });
            Thread.Sleep(ClickDelay);
            SendMouseEvent(new MouseStroke() { State = MouseState.LeftUp });
        }

        public void SendRightClick()
        {
            SendMouseEvent(new MouseStroke() { State = MouseState.RightDown });
            Thread.Sleep(ClickDelay);
            SendMouseEvent(new MouseStroke() { State = MouseState.RightUp });
        }

        private readonly short[] DirToVal = { 100, -100 };

        public void ScrollMouse(ScrollDirection direction)
        {
            SendMouseEvent(new MouseStroke() { State = MouseState.ScrollDown, Rolling = DirToVal[(int)direction] });
        }

        public void MoveMouseTo(int x, int y, bool useDriver = true)
        {
            if (useDriver)
            {
                var stroke = new Stroke();
                var mouseStroke = new MouseStroke
                {
                    X = x,
                    Y = y
                };

                stroke.Mouse = mouseStroke;
                stroke.Mouse.Flags = MouseFlags.MoveAbsolute;

                InterceptionDriver.Send(_context, _mouseId, ref stroke, 1);
            }
            else
            {
                Cursor.Position = new Point(x, y);
            }
        }
    }
}
