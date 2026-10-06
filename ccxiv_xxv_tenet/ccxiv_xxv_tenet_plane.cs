//----------------------------------------------------------
// 214.su
// 2022-09-25
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

//----------------------------------------------------------
namespace ccxiv_xxv_tenet {
    //----------------------------------------------------------
    public class ccxiv_xxv_tenet_plane : ccxiv_xxv_root {
        //----------------------------------------------------------
        public bool _visible = true;
        public vsize _texture_id = new(0);
        public vsize _program_id = new(0);
        public ccxiv_xxv_tenet_box _box = new();
        public ccxiv_xxv_tenet_spirit_list _spirits = new();
        public ccxiv_xxv_tenet_universe _universe_ptr = null;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_plane(ccxiv_xxv_tenet_universe universe_ptr_) : base() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plane::new");
            //----------------------------------------------------------
            _universe_ptr=universe_ptr_;
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
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_plane::bean_write");
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
            xxv.fenter("ccxiv_xxv_tenet_plane::bean_read");
            //----------------------------------------------------------
            _id=bean_["id"] as vsize;
            _visible=(bool)(bean_["visible"] as vsize);
            _texture_id=bean_["texture_id"] as vsize;
            _program_id=bean_["program_id"] as vsize;
            _spirits.bean_read(bean_["spirits"] as vlist);
            _box.bean_read(bean_["box"] as vhash);
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