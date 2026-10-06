//----------------------------------------------------------
// 214.su
// 2022-10-03
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public class ccxiv_xxv_tenet_plain_program : ccxiv_xxv_root {
        //----------------------------------------------------------
        public vtext _name = new();
        public vtext _sfn = new();
        public int _opengl_program_id = -1;
        public bool _opengl_ready = false;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_program() : base() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::new");
            //----------------------------------------------------------
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void Dispose() {
            //----------------------------------------------------------
            if(_opengl_ready) { GL.DeleteProgram(_opengl_program_id); }
            _opengl_ready=false;
            //----------------------------------------------------------
            base.Dispose();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void opengl_init() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::opengl_init");
            //----------------------------------------------------------
            string sfn = (string)_sfn;
            string fn = xxv._datapath+@"\"+sfn;
            xxv.log("PROGRAM BEAN fn", fn);
            //----------------------------------------------------------
            // Читаем файл
            //----------------------------------------------------------
            vbean program_bean = vbean.polymorph_discread(fn);
            //-----------------------------------------------
            vhash program_hash = program_bean as vhash;
            string shv_text = (string)(program_hash["v"] as vtext);
            string shf_text = (string)(program_hash["f"] as vtext);
            //----------------------------------------------------------
            // Регистрируем
            //----------------------------------------------------------
            _opengl_program_id=opengl_program_register(shv_text, shf_text);
            _opengl_ready=true;
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public bool opengl_use() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::opengl_use");
            //----------------------------------------------------------
            if(_opengl_ready) { GL.UseProgram(_opengl_program_id); }
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (_opengl_ready);
            }
        //----------------------------------------------------------
        private int opengl_program_register(string shv_text_, string shf_text_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::opengl_shader_reg");
            xxv.log("PROGRAM register", _name);
            //----------------------------------------------------------
            int shv_id = opengl_shader_register(ShaderType.VertexShader, shv_text_);
            int shf_id = opengl_shader_register(ShaderType.FragmentShader, shf_text_);
            //----------------------------------------------------------
            int p_id = GL.CreateProgram();
            GL.AttachShader(p_id, shv_id);
            GL.AttachShader(p_id, shf_id);
            GL.LinkProgram(p_id);
            GL.DetachShader(p_id, shv_id);
            GL.DetachShader(p_id, shf_id);
            //----------------------------------------------------------
            GL.DeleteShader(shv_id);
            GL.DeleteShader(shf_id);
            //----------------------------------------------------------
            xxv.log("PROGRAM id", p_id);
            xxv.log("PROGRAM report", GL.GetProgramInfoLog(p_id));
            xxv.fexit();
            //----------------------------------------------------------
            return (p_id);
            }
        //----------------------------------------------------------
        private int opengl_shader_register(ShaderType type_, string shader_text_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::opengl_shader_reg");
            xxv.log("SHADER register", shader_text_);
            //----------------------------------------------------------
            int id = GL.CreateShader(type_);
            GL.ShaderSource(id, shader_text_);
            GL.CompileShader(id);
            //----------------------------------------------------------
            xxv.log("SHADER id", id);
            xxv.log("SHADER report", GL.GetShaderInfoLog(id));
            xxv.fexit();
            //----------------------------------------------------------
            return (id);
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::bean_write");
            //----------------------------------------------------------
            vhash bean = new();
            bean["id"]=_id;
            bean["name"]=_name;
            bean["sfn"]=_sfn;
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (bean);
            }
        //----------------------------------------------------------
        public void bean_read(vhash bean_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_program::bean_read");
            //----------------------------------------------------------
            _id=bean_["id"] as vsize;
            _name=bean_["name"] as vtext;
            _sfn=bean_["sfn"] as vtext;
            //----------------------------------------------------------
            xxv.log("BEAN program _id", _id);
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        }
    //----------------------------------------------------------
    }
//----------------------------------------------------------
//EOF