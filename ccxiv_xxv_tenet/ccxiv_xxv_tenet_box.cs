//----------------------------------------------------------
// 214.su
// 2022-09-28
// 2023-09-16
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

//----------------------------------------------------------
using ccxiv;
using ccxiv_xxv;

//----------------------------------------------------------
namespace ccxiv_xxv_tenet {
    //----------------------------------------------------------
    public class ccxiv_xxv_tenet_box : ccxiv_xxv_root {
        //----------------------------------------------------------
        public Vector3 _center;
        public Vector3 _size;
        public float _frame = 0;
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_box() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_box::box");
            //----------------------------------------------------------
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_box::bean_write");
            //----------------------------------------------------------
            vhash bean = new();
            bean["cx"]=new vdigit(_cx);
            bean["cy"]=new vdigit(_cy);
            bean["cz"]=new vdigit(_cz);
            bean["dx"]=new vdigit(_dx);
            bean["dy"]=new vdigit(_dy);
            bean["dz"]=new vdigit(_dz);
            bean["cr"]=new vdigit(_cr);
            bean["cg"]=new vdigit(_cg);
            bean["cb"]=new vdigit(_cb);
            bean["frame"]=new vdigit(_frame);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            return (bean);
            }
        //----------------------------------------------------------
        public void bean_read(vhash bean_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_box::bean_read");
            //----------------------------------------------------------
            _cx=(float)(bean_["cx"] as vdigit);
            _cy=(float)(bean_["cy"] as vdigit);
            _cz=(float)(bean_["cz"] as vdigit);
            _dx=(float)(bean_["dx"] as vdigit);
            _dy=(float)(bean_["dy"] as vdigit);
            _dz=(float)(bean_["dz"] as vdigit);
            _cr=(float)(bean_["cr"] as vdigit);
            _cg=(float)(bean_["cg"] as vdigit);
            _cb=(float)(bean_["cb"] as vdigit);
            _frame=(float)(bean_["frame"] as vdigit);
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