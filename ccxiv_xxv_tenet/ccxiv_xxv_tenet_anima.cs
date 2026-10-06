//----------------------------------------------------------
// 214.su
// 2023-09-15
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
    public class ccxiv_xxv_tenet_anima : ccxiv_xxv_root {
        //----------------------------------------------------------
        public int _type = 0;
        public float _speed = 0;
        public bool _isdone = false;
        public Vector3 _goal = new Vector3(0);
        public Vector3 _dir = new Vector3(0);
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_anima() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_anima::anima");
            //----------------------------------------------------------
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public vhash bean_write() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_tenet_anima::bean_write");
            //----------------------------------------------------------
            vhash bean = new();
            bean["type"]=new vsize(_type);
            bean["speed"]=new vdigit(_speed);
            bean["isdone"]=new vsize(_isdone);
            bean["goal"]=new vvec(_goal);
            bean["dir"]=new vvec(_dir);
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
            _type=(int)(bean_["type"] as vsize);
            _speed=(float)(bean_["speed"] as vdigit);
            _isdone=(bool)(bean_["iddone"] as vsize);
            _goal=(bean_["goal"] as vvec).Value;
            _dir=(bean_["dir"] as vvec).Value;
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