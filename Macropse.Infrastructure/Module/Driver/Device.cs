using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Driver
{
    class Device
    {
        private IntPtr CTX;
        private Thread CallbackThread;

        public bool IsLoaded { get; set; }
        public int KeyPressDelay { get; set; }
        public int ClickDelay { get; set; }
        public int ScrollDelay { get; set; }

        public event EventHandler<KeyPressedEventArgs> OnKeyPressed;
        public event EventHandler<MousePressedEventArgs> OnMousePressed;

        private int DeviceId;

        public Device()
        {
            CTX = IntPtr.Zero;
            KeyPressDelay = 1;
            ClickDelay = 1;
            ScrollDelay = 15;
        }

        public bool Load()
        {
            if (IsLoaded)
            {
                return false;
            }
            CTX = InterceptionDriver.CreateContext();
            if (CTX != IntPtr.Zero)
            {
                CallbackThread = new Thread(new ThreadStart(DriverCallback))
                {
                    Priority = ThreadPriority.Highest,
                    IsBackground = true
                };
                CallbackThread.Start();
                IsLoaded = true;
                return true;
            }
            else
            {
                IsLoaded = false;
                return false;
            }
        }

        public void Unload()
        {
            if (!IsLoaded)
            {
                return;
            }
            if (CTX != IntPtr.Zero)
            {
                CallbackThread.Abort();
                InterceptionDriver.DestroyContext(CTX);
                IsLoaded = false;
            }
        }

        private void DriverCallback()
        {
            InterceptionDriver.SetFilter(CTX, InterceptionDriver.IsKeyboard, 0xFFFF);
            InterceptionDriver.SetFilter(CTX, InterceptionDriver.IsMouse, 0xFFFF);

            var stroke = new Stroke();

            while (InterceptionDriver.Receive(CTX, DeviceId = InterceptionDriver.Wait(CTX), ref stroke, 1) > 0)
            {
                if (InterceptionDriver.IsMouse(DeviceId) > 0)
                {
                    if (OnMousePressed != null)
                    {
                        var args = new MousePressedEventArgs()
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
                if (InterceptionDriver.IsKeyboard(DeviceId) > 0)
                {
                    if (OnKeyPressed != null)
                    {
                        var args = new KeyPressedEventArgs()
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
                InterceptionDriver.Send(CTX, DeviceId, ref stroke, 1);
            }
            Unload();
            throw new Exception(" Driver failed and has been unloaded.");
        }

        public void SendKey(Keys key, KeyState state)
        {
            var stroke = new Stroke();
            var keyStroke = new KeyStroke
            {
                Code = key,
                State = state
            };
            stroke.Key = keyStroke;
            InterceptionDriver.Send(CTX, DeviceId, ref stroke, 1);
            if (KeyPressDelay > 0)
            {
                Thread.Sleep(KeyPressDelay);
            }
        }

        public void SendKey(Keys key)
        {
            SendKey(key, KeyState.Down);
            if (KeyPressDelay > 0)
            {
                Thread.Sleep(KeyPressDelay);
            }
            SendKey(key, KeyState.Up);
        }

        public void SendKeys(params Keys[] HWKeys)
        {
            foreach (Keys key in HWKeys)
            {
                SendKey(key);
            }
        }

        public void SendMouseEvent(MouseState state)
        {
            var stroke = new Stroke();
            var mouseStroke = new MouseStroke
            {
                State = state
            };
            if (state == MouseState.ScrollUp)
            {
                mouseStroke.Rolling = 120;
            }
            else if (state == MouseState.ScrollDown)
            {
                mouseStroke.Rolling = -120;
            }
            stroke.Mouse = mouseStroke;
            InterceptionDriver.Send(CTX, 12, ref stroke, 1);
        }

        public void SendLeftClick()
        {
            SendMouseEvent(MouseState.LeftDown);
            Thread.Sleep(ClickDelay);
            SendMouseEvent(MouseState.LeftUp);
        }

        public void SendRightClick()
        {
            SendMouseEvent(MouseState.RightDown);
            Thread.Sleep(ClickDelay);
            SendMouseEvent(MouseState.RightUp);
        }

        private readonly MouseState[] DirToStateTable = { MouseState.ScrollDown, MouseState.ScrollUp };

        public void ScrollMouse(ScrollDirection direction)
        {
            SendMouseEvent(DirToStateTable[(int)direction]);
        }

        public void MoveMouseTo(int x, int y)
        {
            //Cursor.Position = new System.Drawing.Point(x, y);
        }
    }
}
