//----------------------------------------------------------
// 214.su
// 2022-09-10
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------
using ccxiv;
using ccxiv_xxv;
using ccxiv_xxv_tenet;

//----------------------------------------------------------
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Compute.OpenCL;

//----------------------------------------------------------
namespace ccxiv_xxv_tenet_plain {
    //----------------------------------------------------------
    internal class ccxiv_xxv_tenet_plain_window : GameWindow {
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_replica _replica = null;
        //----------------------------------------------------------
        private bool _disposed=false;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_window(string[] args_) : base(
              new GameWindowSettings() {
                  // IsMultiThreaded
                  // RenderFrequency
                  // UpdateFrequency
                  },
              new NativeWindowSettings() {
                  Size=new OpenTK.Mathematics.Vector2i(400, 400),
                  // Location
                  // WindowBorder
                  // WindowState
                  // StartVisible
                  // StartFocused
                  Title="ccxiv_xxv_tenet_plain",
                  // CurrentMonitor
                  APIVersion=new System.Version(4, 6),
                  // AutoLoadBindings
                  // Flags
                  // Profile
                  API=ContextAPI.OpenGL,
                  // IsEventDriven
                  // Icon
                  // SharedContext
                  // IsFullscreen
                  NumberOfSamples=8,
                  }) 
            {
            //----------------------------------------------------------
            _replica=new(args_);
            //----------------------------------------------------------
            _replica._universe._DX=400;
            _replica._universe._DY=400;
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        protected override void Dispose(bool disposing_) {
            //----------------------------------------------------------
            if((disposing_)&&(!_disposed)) {
                //----------------------------------------------------------
                if(_replica!=null) _replica.Dispose();
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            _disposed=true;
            //----------------------------------------------------------
            base.Dispose(disposing_);
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        protected override void OnLoad() {
            //----------------------------------------------------------
            base.OnLoad();
            //----------------------------------------------------------
            GL.ClearColor(0f, 0f, 0f, 1.0f);
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        protected override void OnUpdateFrame(FrameEventArgs e) {
            //----------------------------------------------------------
            _replica.opengl_tick();
            _replica.opengl_draw();
            //----------------------------------------------------------
            SwapBuffers();
            //----------------------------------------------------------
            KeyboardState input = KeyboardState;
            //----------------------------------------------------------
            if(input.IsKeyDown(Keys.Escape)) {
                //----------------------------------------------------------
                _replica._main_close=true;
                Close();
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            base.OnUpdateFrame(e);
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        protected override void OnResize(ResizeEventArgs e_) {
            //----------------------------------------------------------
            base.OnResize(e_);
            //----------------------------------------------------------
            GL.Viewport(0, 0, e_.Width, e_.Height);
            //----------------------------------------------------------
            _replica._universe._DX=e_.Width;
            _replica._universe._DY=e_.Height;
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        }
    //----------------------------------------------------------
    }
//----------------------------------------------------------
//EOF