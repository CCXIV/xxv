//----------------------------------------------------------
// 214.su
// 2022-10-03
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------
using ccxiv;
using ccxiv_xxv;
using ccxiv_xxv_tenet;

//----------------------------------------------------------
using OpenTK.Graphics.OpenGL4;

//----------------------------------------------------------
namespace ccxiv_xxv_tenet_plain {
    //----------------------------------------------------------
    public class ccxiv_xxv_tenet_plain_plane : ccxiv_xxv_root {
        //----------------------------------------------------------
        public bool _visible = true;
        public vsize _texture_id = new(0);
        public vsize _program_id = new(0);
        public ccxiv_xxv_tenet_box _box = new();
        public ccxiv_xxv_tenet_plain_spirit_list _spirits = null;
        public ccxiv_xxv_tenet_plain_world _world_ptr = null;
        public ccxiv_xxv_tenet_plain_texture _texture_ptr = null;
        public ccxiv_xxv_tenet_plain_program _program_ptr = null;
        public bool _opengl_ready = false;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_plane(ccxiv_xxv_tenet_plain_world world_ptr_) : base(world_ptr_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_plane::new");
            //----------------------------------------------------------
            _world_ptr=world_ptr_;
            _spirits=new(this);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void Dispose() {
            //----------------------------------------------------------
            if(!_disposed) {
                //----------------------------------------------------------
                _box.Dispose();
                _spirits.Dispose();
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            base.Dispose();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void opengl_init() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_plane::opengl_init");
            //----------------------------------------------------------
            if(_texture_ptr==null) { _texture_ptr=_world_ptr._universe_ptr._textures.gi((int)_texture_id) as ccxiv_xxv_tenet_plain_texture; }
            xxv.warn(_texture_ptr==null, "_texture_ptr==null");
            //----------------------------------------------------------
            if(_program_ptr==null) { _program_ptr=_world_ptr._universe_ptr._programs.gi((int)_program_id) as ccxiv_xxv_tenet_plain_program; }
            xxv.warn(_program_ptr==null, "_program_ptr==null");
            //----------------------------------------------------------
            _spirits.opengl_init();
            _opengl_ready=true;
            //----------------------------------------------------------
            xxv.fexit();
            }
        //----------------------------------------------------------
        public void opengl_draw() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_plane::opengl_draw");
            xxv.warn(_universe_ptr==null, "_universe_ptr==null");
            //----------------------------------------------------------
            // Надо ли переключить текстуру
            //----------------------------------------------------------
            int t_id = (int)_texture_id;
            if(t_id!=_universe_ptr._now_texture_id) {
                //----------------------------------------------------------
                xxv.log("USE texture", t_id);
                //----------------------------------------------------------
                if(_texture_ptr==null) { _texture_ptr=_universe_ptr._textures.gi(t_id) as ccxiv_xxv_tenet_plain_texture; }
                xxv.warn(_texture_ptr==null, "_texture_ptr==null");
                //----------------------------------------------------------
                xxv.log("USE texture", _texture_ptr._name);
                bool isok = _texture_ptr.opengl_use();
                if(isok) { _universe_ptr._now_texture_id=t_id; }
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            // Надо ли переключить программу
            //----------------------------------------------------------
            int p_id = (int)_program_id;
            if(p_id!=_universe_ptr._now_program_id) {
                //----------------------------------------------------------
                xxv.log("USE program", p_id);
                //----------------------------------------------------------
                if(_program_ptr==null) { _program_ptr=_universe_ptr._programs.gi(p_id) as ccxiv_xxv_tenet_plain_program; }
                xxv.warn(_program_ptr==null, "_program_ptr==null");
                //----------------------------------------------------------
                xxv.log("USE program", _program_ptr._name);
                bool isok = _program_ptr.opengl_use();
                if(isok) { _universe_ptr._now_program_id=p_id; }
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            // Камера для этого плана
            //----------------------------------------------------------
            float scale = 2.0f;
            float DX = _universe_ptr._DX;
            float DY = _universe_ptr._DY;
            float[] camera_scale = { DY/DX, -1 };
            int camera_scale_loc = GL.GetUniformLocation(_program_ptr._opengl_program_id, "_camera_scale");
            GL.Uniform2(camera_scale_loc, 1, camera_scale);
            //----------------------------------------------------------
            // Список духов
            //----------------------------------------------------------
            _spirits.opengl_draw();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_plane::bean_write");
            //----------------------------------------------------------
            vhash bean = new();
            bean["id"]=_id;
            bean["visible"]=new vsize(_visible);
            bean["texture_id"]=_texture_id;
            bean["program_id"]=_program_id;
            bean["spirits"]=_spirits.bean_write();
            bean["box"]=_box.bean_write();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (bean);
            }
        //----------------------------------------------------------
        public void bean_read(vhash bean_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_plane::bean_read");
            //----------------------------------------------------------
            _id=bean_["id"] as vsize;
            _visible=(bool)(bean_["visible"] as vsize);
            _texture_id=bean_["texture_id"] as vsize;
            _program_id=bean_["program_id"] as vsize;
            _spirits.bean_read(bean_["spirits"] as vlist);
            _box.bean_read(bean_["box"] as vhash);
            //----------------------------------------------------------
            xxv.log("BEAN plane _id", _id);
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        }
    //----------------------------------------------------------
    }
//----------------------------------------------------------
//EOF