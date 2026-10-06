//----------------------------------------------------------
// 214.su
// 2022-09-10
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------
using ccxiv;
//----------------------------------------------------------
namespace ccxiv_xxv {
    //----------------------------------------------------------
    public class ccxiv_xxv_solder_server : ccxiv_xxv_solder {
        //----------------------------------------------------------
        public bool _is_pipe_ready = false;
        //----------------------------------------------------------
        public string _pipe_in_key = "";
        public string _pipe_out_key = "";
        //----------------------------------------------------------
        private AnonymousPipeServerStream _pipe_in;
        private AnonymousPipeServerStream _pipe_out;
        //----------------------------------------------------------
        public ccxiv_xxv_solder_server(string name_) : base(name_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_server::new(string,string[])");
            xxv.log("name_", name_);
            //----------------------------------------------------------
            pipe_start();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void Dispose() {
            //----------------------------------------------------------
            if(!_disposed) {
                _pipe_in.Dispose();
                _pipe_out.Dispose();
                }
            //----------------------------------------------------------
            base.Dispose();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public override void process_msg(ccxiv_xxv_msg msg_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_server::process_msg");
            //----------------------------------------------------------
            switch(msg_._com) {
                //----------------------------------------------------------
                case (ccxiv_xxv_msg.com.solder_updateme): { process_solder_updateme(msg_); break; }
                    //----------------------------------------------------------
                }
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void process_solder_updateme(ccxiv_xxv_msg msg_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_server::process_solder_updateme");
            xxv.log("PROCESS solder_updateme");
            //----------------------------------------------------------
            send_solder_update();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void send_solder_update() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_server::send_solder_update");
            //----------------------------------------------------------
            vhash hash = new();
            hash["_id"]=_id;
            hash["_manager_id"]=_manager_id;
            hash["_parent_id"]=_parent_id;
            //----------------------------------------------------------
            send(ccxiv_xxv_msg.com.solder_update, hash);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        void pipe_start() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_server::pipe_start");
            //----------------------------------------------------------
            _pipe_in=new(PipeDirection.In, HandleInheritability.Inheritable);
            xxv.log("_pipe_in.TransmissionMode", _pipe_in.TransmissionMode);
            _pipe_in_key=_pipe_in.GetClientHandleAsString();
            xxv.log("_pipe_in_key", _pipe_in_key);
            pipe_in_stream(_pipe_in);
            //----------------------------------------------------------
            _pipe_out=new(PipeDirection.Out, HandleInheritability.Inheritable);
            xxv.log("_pipe_out.TransmissionMode", _pipe_out.TransmissionMode);
            _pipe_out_key=_pipe_out.GetClientHandleAsString();
            xxv.log("_pipe_out_key", _pipe_out_key);
            pipe_out_stream(_pipe_out);
            //----------------------------------------------------------
            _is_pipe_ready=true;
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