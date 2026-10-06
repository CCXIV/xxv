//----------------------------------------------------------
// 214.su
// 2022-10-13
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class ccxiv_xxv_tenet_plain_texture : ccxiv_xxv_root {
        //----------------------------------------------------------
        public vtext _name = new();
        public vtext _sfn = new();
        public float _dx = 0;
        public float _dy = 0;
        public float _img_dx = 0;
        public float _img_dy = 0;
        public float _slo_rows = 0;
        public float _slo_cols = 0;
        public float _slo_dx = 0;
        public float _slo_dy = 0;
        public int _opengl_texture_id = -1;
        public bool _opengl_ready = false;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plain_texture() : base() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_texture::new");
            //----------------------------------------------------------
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void Dispose() {
            //----------------------------------------------------------
            if(_opengl_ready) { GL.DeleteTexture(_opengl_texture_id); }
            _opengl_ready=false;
            //----------------------------------------------------------
            base.Dispose();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void opengl_init() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_texture::opengl_init");
            //----------------------------------------------------------
            string sfn = (string)_sfn;
            string fn = xxv._datapath+@"\"+sfn;
            xxv.log("TEXTURE BEAN fn",fn);
            //----------------------------------------------------------
            // Читаем файл
            //----------------------------------------------------------
            vbean texture_bean = vbean.polymorph_discread(fn);
            //-----------------------------------------------
            vhash texture_hash = texture_bean as vhash;
            _slo_cols=(float)(texture_hash["cols"] as vsize);
            _slo_rows=(float)(texture_hash["rows"] as vsize);
            vmem texture_mem = texture_hash["data"] as vmem;
            //-----------------------------------------------
            int texture_size =(int)texture_mem._size;
            //-----------------------------------------------
            // Автоопределение размера
            //-----------------------------------------------
            _img_dx=(float)Math.Sqrt(texture_size/4);
            _img_dy=_img_dx;
            xxv.log("_img_dx", _img_dx);
            xxv.log("_img_dy", _img_dy);
            //-----------------------------------------------
            // Рассчёты
            //-----------------------------------------------
            _dx=_img_dx/_slo_rows;
            _dy=_img_dy/_slo_cols;
            xxv.log("_dx", _dx);
            xxv.log("_dy", _dy);
            //-----------------------------------------------
            _slo_dx=1/_slo_rows;
            _slo_dy=1/_slo_cols;
            xxv.log("_slo_dx", _slo_dx);
            xxv.log("_slo_dy", _slo_dy);
            //-----------------------------------------------
            // Генерация
            //-----------------------------------------------
            _opengl_texture_id=opengl_texture_gen((int)_img_dx, (int)_img_dy, texture_mem._value);
            _opengl_ready=true;
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public bool opengl_use() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_texture::opengl_use");
            //----------------------------------------------------------
            if(_opengl_ready) {
                //-----------------------------------------------
                // Включаем текстуру в нулевую позицию
                //-----------------------------------------------
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, _opengl_texture_id);
                //-----------------------------------------------
                // Настройки
                //-----------------------------------------------
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapS.Repeat);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapT.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                //-----------------------------------------------
                }
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (_opengl_ready);
            }
        //----------------------------------------------------------
        private int opengl_texture_gen(int img_dx_, int img_dy_, byte[] data_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_texture::opengl_texture_gen");
            xxv.log("TEXTURE register", _name);
            //----------------------------------------------------------
            // Генерация
            //----------------------------------------------------------
            int tex_id=GL.GenTexture();
            //-----------------------------------------------
            GL.BindTexture(TextureTarget.Texture2D, tex_id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            //----------------------------------------------------------
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img_dx_, img_dy_, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data_);
            //----------------------------------------------------------
            xxv.log("TEXTURE id", tex_id);
            xxv.fexit();
            //----------------------------------------------------------
            return (tex_id);
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plain_texture::bean_write");
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
            xxv.fenter("ccxiv_xxv_tenet_plain_texture::bean_read");
            //----------------------------------------------------------
            _id=bean_["id"] as vsize;
            _name=bean_["name"] as vtext;
            _sfn=bean_["sfn"] as vtext;
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        }
    //----------------------------------------------------------
    }
//----------------------------------------------------------
//EOF