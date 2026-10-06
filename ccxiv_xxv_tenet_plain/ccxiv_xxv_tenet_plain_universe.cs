//----------------------------------------------------------
// 214.su
// 2022-10-03
//----------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

//----------------------------------------------------------
using ccxiv;
using ccxiv_xxv;
using ccxiv_xxv_tenet;

//----------------------------------------------------------
using OpenTK.Graphics.OpenGL4;

//----------------------------------------------------------
namespace ccxiv_xxv_tenet_plain {
    //----------------------------------------------------------
    public class ccxiv_xxv_tenet_plain_universe : ccxiv_xxv_root {
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_texture_list _textures = new();
        public ccxiv_xxv_tenet_plain_program_list _programs = new();
        public ccxiv_xxv_tenet_plain_world_list _worlds = null;
        public bool _opengl_ready = false;
        public bool _opengl_do_init = false;
        public int _now_texture_id = -1;
        public int _now_program_id = -1;
        public float _DX = 0;
        public float _DY = 0;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_replica _replica_ptr = null;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_universe(ccxiv_xxv_tenet_plain_replica replica_ptr_) : base() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_universe::new");
            //----------------------------------------------------------
            _replica_ptr=replica_ptr_;
            _worlds =new(this);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void Dispose() {
            //----------------------------------------------------------
            if(!_disposed) {
                //----------------------------------------------------------
                _worlds.Dispose();
                _programs.Dispose();
                _textures.Dispose();
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            _opengl_ready=false;
            //----------------------------------------------------------
            base.Dispose();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void opengl_tick() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_universe::opengl_tick");
            //----------------------------------------------------------
            if(_opengl_do_init) {
                //----------------------------------------------------------
                opengl_init();
                _opengl_do_init=false;
                //----------------------------------------------------------
                };
            //----------------------------------------------------------
            xxv.fexit();
            }
        //----------------------------------------------------------
        public void opengl_init() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_universe::opengl_init");
            //----------------------------------------------------------
            _textures.opengl_init();
            _programs.opengl_init();
            _worlds.opengl_init();
            //----------------------------------------------------------
            _opengl_ready=true;
            //----------------------------------------------------------
            xxv.fexit();
            }
        //----------------------------------------------------------
        public void opengl_draw() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_universe::opengl_draw");
            //----------------------------------------------------------
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //----------------------------------------------------------
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            //GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.DstColor);
            //----------------------------------------------------------
            if(_opengl_ready) { _worlds.opengl_draw(); }
            //----------------------------------------------------------
            GL.Disable(EnableCap.Blend);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_universe::bean_write");
            //----------------------------------------------------------
            vhash bean = new();
            bean["id"]=_id;
            bean["worlds"]=_worlds.bean_write();
            bean["programs"]=_programs.bean_write();
            bean["textures"]=_textures.bean_write();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (bean);
            }
        //----------------------------------------------------------
        public void bean_read(vhash bean_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_universe::bean_read");
            //----------------------------------------------------------
            _id=bean_["id"] as vsize;
            _worlds.bean_read(bean_["worlds"] as vlist);
            _programs.bean_read(bean_["programs"] as vlist);
            _textures.bean_read(bean_["textures"] as vlist);
            //----------------------------------------------------------
            xxv.log("BEAN universe _id",_id);
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        }
    //----------------------------------------------------------
    }
//----------------------------------------------------------
//EOF