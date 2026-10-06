//----------------------------------------------------------
// 214.su
// 2022-08-28
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//----------------------------------------------------------
namespace ccxiv {
    //----------------------------------------------------------
    class ccxiv_xxv_manager_module : ccxiv_xxv_root {
        //----------------------------------------------------------
        public string _name="";
        public bool _isexec=true;
        public ccxiv_xxv_solder _solder;
        //----------------------------------------------------------
        public string _pipe_in_key = "";
        public string _pipe_out_key = "";
        //----------------------------------------------------------
        public ccxiv_xxv_manager_module(string name_, bool isexec_=true) {
            //----------------------------------------------------------
            xxv.fenter(this.ToString(), "new");
            //----------------------------------------------------------
            _name=name_;
            _isexec=isexec_;
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