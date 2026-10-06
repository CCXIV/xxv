//----------------------------------------------------------
// 214.su
// 2022-10-03
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------
using ccxiv;
using ccxiv_xxv;
using ccxiv_xxv_tenet;

//----------------------------------------------------------
using OpenTK.Graphics.OpenGL4;

using static ccxiv_xxv.ccxiv_xxv_msg;

//----------------------------------------------------------
namespace ccxiv_xxv_tenet_plain {
    //----------------------------------------------------------
    public class ccxiv_xxv_tenet_plain_spirit : ccxiv_xxv_root {
        //----------------------------------------------------------
        private const int _vb_count = 4;
        private const int _ib_count = 6;
        private const int vb_length = 8;
        private const int tb_length = 8;
        private const int ib_length = 6;
        //----------------------------------------------------------
        public bool _visible = true;
        public ccxiv_xxv_tenet_box _box = new();
        public ccxiv_xxv_tenet_anima _anima = new();
        public bool _opengl_ready = false;
        private int _va_id;
        private int[] _b_id=new int[_vb_count];
        private ccxiv_xxv_tenet_plain_plane _plane_ptr = null;
        private float _frame = 0;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_spirit(ccxiv_xxv_tenet_plain_plane plane_ptr_) : base() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_spirit::new");
            //----------------------------------------------------------
            _plane_ptr=plane_ptr_;
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void Dispose() {
            //----------------------------------------------------------
            if(!_disposed) {
                //----------------------------------------------------------
                if(_opengl_ready) {
                    GL.DeleteBuffers(_vb_count, _b_id);
                    GL.DeleteVertexArray(_va_id);
                    }
                //----------------------------------------------------------
                _box.Dispose();
                _anima.Dispose();
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            _opengl_ready=false;
            //----------------------------------------------------------
            base.Dispose();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void opengl_init() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_spirit::opengl_init");
            xxv.log("INIT spirit");
            //----------------------------------------------------------
            _va_id=GL.GenVertexArray();
            GL.GenBuffers(_vb_count, _b_id);
            xxv.log("_va_id", _va_id);
            xxv.log("_b_id[0]", _b_id[0]);
            //----------------------------------------------------------
            opengl_regen();
            //----------------------------------------------------------
            _opengl_ready=true;
            //----------------------------------------------------------
            xxv.fexit();
            }
        //----------------------------------------------------------
        public void opengl_regen() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_spirit::opengl_regen");
            xxv.log("REGEN spirit");
            //-----------------------------------------------
            // Высшие объекты
            //-----------------------------------------------
            xxv.warn(_plane_ptr==null, "_plane_ptr==null");
            ccxiv_xxv_tenet_plain_texture texture_ptr = _plane_ptr._texture_ptr;
            xxv.warn(texture_ptr==null, "texture_ptr==null");
            ccxiv_xxv_tenet_plain_program program_ptr= _plane_ptr._program_ptr;
            xxv.warn(program_ptr==null, "program_ptr==null");
            //----------------------------------------------------------
            int v_location = GL.GetAttribLocation(program_ptr._opengl_program_id, "vertex_");
            int c_location = GL.GetAttribLocation(program_ptr._opengl_program_id, "color_");
            int t_location = GL.GetAttribLocation(program_ptr._opengl_program_id, "texcoord_");
            //----------------------------------------------------------
            // Поехали
            //----------------------------------------------------------
            GL.BindVertexArray(_va_id);
            //----------------------------------------------------------
            // Координаты
            //----------------------------------------------------------
            xxv.log("VB");
            float[] vb_data = { -0.5f, -0.5f, -0.5f, 0.5f, 0.5f, 0.5f, 0.5f, -0.5f };
            //----------------------------------------------------------
            GL.BindBuffer(BufferTarget.ArrayBuffer, _b_id[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, vb_length*sizeof(float), vb_data, BufferUsageHint.StaticDraw);
            //----------------------------------------------------------
            GL.EnableVertexAttribArray(v_location);
            GL.VertexAttribPointer(v_location, 2, VertexAttribPointerType.Float, false, 2*sizeof(float), 0);
            //----------------------------------------------------------
            // Текстурные координаты
            //----------------------------------------------------------
            xxv.log("TB");
            //----------------------------------------------------------
            _frame = _box._frame;
            float slo_rows = texture_ptr._slo_rows;
            float slo_y = (float)(int)(_frame/slo_rows);
            float slo_x = _frame-(slo_y*slo_rows);
            float slo_dx = texture_ptr._slo_dx;
            float slo_dy = texture_ptr._slo_dy;
            //----------------------------------------------------------
            float[] tb_sqd = { 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f };
            float[] tb_data = new float[tb_length];
                //----------------------------------------------------------
                for(int i = 0; i<4; i++) {
                    tb_data[i*2+0]=slo_dx*(slo_x+tb_sqd[i*2+0]);
                    tb_data[i*2+1]=slo_dy*(slo_y+tb_sqd[i*2+1]);
                    }
            //----------------------------------------------------------
            GL.BindBuffer(BufferTarget.ArrayBuffer, _b_id[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, tb_length*sizeof(float), tb_data, BufferUsageHint.StaticDraw);
            //----------------------------------------------------------
            GL.EnableVertexAttribArray(t_location);
            GL.VertexAttribPointer(t_location, 2, VertexAttribPointerType.Float, false, 2*sizeof(float), 0);
            //----------------------------------------------------------
            // Индексы
            //----------------------------------------------------------
            xxv.log("IB");
            //----------------------------------------------------------
            uint[] ib_data = { 0, 1, 2, 0, 2, 3 };
            //----------------------------------------------------------
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _b_id[3]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, ib_length*sizeof(uint), ib_data, BufferUsageHint.StaticDraw);
            //----------------------------------------------------------
            // Сброс (на всякий случай)
            //----------------------------------------------------------
            GL.BindVertexArray(0);
            //----------------------------------------------------------
            xxv.fexit();
            }
        //----------------------------------------------------------
        public void opengl_draw() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_spirit::opengl_draw");
            //xxv.log("DRAW spirit");
            //----------------------------------------------------------
            int opengl_program_id = _plane_ptr._program_ptr._opengl_program_id;
            //----------------------------------------------------------
            // Анимация
            //----------------------------------------------------------
            _anima.step(_box);
            if(_anima._isdone) {
                //----------------------------------------------------------
                xxv.log("DONE spirit animation");
                ccxiv_xxv_tenet_plain_replica replica_ptr = _plane_ptr._universe_ptr._replica_ptr;
                replica_ptr.send_spirit_anima_done(this.bean_write());
                //----------------------------------------------------------
                };
            //----------------------------------------------------------
            // Фрейм
            //----------------------------------------------------------
            if(_frame!=_box._frame) { opengl_regen(); }
            //----------------------------------------------------------
            // Деформация
            //----------------------------------------------------------
            float[] center = { _box._center.X, _box._center.Y };
            float[] size = { _box._size.X, _box._size.Y };
            //----------------------------------------------------------
            int object_center_loc = GL.GetUniformLocation(opengl_program_id, "_object_center");
            int object_size_loc = GL.GetUniformLocation(opengl_program_id, "_object_size");
            GL.Uniform2(object_center_loc, 1, center);
            GL.Uniform2(object_size_loc, 1, size);
            //----------------------------------------------------------
            // Цвет
            //----------------------------------------------------------
            int object_color_loc = GL.GetUniformLocation(opengl_program_id, "_object_color");
            GL.Uniform3(object_color_loc, _box._color.X, _box._color.Y, _box._color.Z);
            //----------------------------------------------------------
            // Рисуем
            //----------------------------------------------------------
            GL.BindVertexArray(_va_id);
            GL.DrawElements(PrimitiveType.Triangles,_ib_count, DrawElementsType.UnsignedInt, 0);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_spirit::bean_write");
            //----------------------------------------------------------
            vhash bean = new();
            bean["id"]=_id;
            bean["visible"]=new vsize(_visible);
            bean["box"]=_box.bean_write();
            bean["anima"]=_anima.bean_write();
            //----------------------------------------------------------
            if(_plane_ptr!=null) {
                //----------------------------------------------------------
                bean["plane_id"]=_plane_ptr._id;
                //----------------------------------------------------------
                };
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (bean);
            }
        //----------------------------------------------------------
        public void bean_read(vhash bean_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_spirit::bean_read");
            //----------------------------------------------------------
            _id=bean_["id"] as vsize;
            _visible=(bool)(bean_["visible"] as vsize);
            _box.bean_read(bean_["box"] as vhash);
            _anima.bean_read(bean_["anima"] as vhash);
            //----------------------------------------------------------
            xxv.log("BEAN spirit _id", _id);
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        }
    //----------------------------------------------------------
    }
//----------------------------------------------------------
//EOF