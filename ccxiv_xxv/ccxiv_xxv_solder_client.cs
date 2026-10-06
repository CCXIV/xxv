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
    public class ccxiv_xxv_solder_client : ccxiv_xxv_solder {
        //----------------------------------------------------------
        public bool _is_pipe_ready = false;
        //----------------------------------------------------------
        private AnonymousPipeClientStream _pipe_in;
        private AnonymousPipeClientStream _pipe_out;
        //----------------------------------------------------------
        public ccxiv_xxv_solder_client(string name_, string[] args_) : base(name_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_client::new(string,string[])");
            xxv.log("name_", name_);
            foreach(string arg in args_) { xxv.log("arg", arg); };
            xxv.warn(args_.Length<2, "args_.Length<2");
            //----------------------------------------------------------
            string pipe_in_key = args_[0];
            string pipe_out_key = args_[1];
            //----------------------------------------------------------
            pipe_start(pipe_in_key, pipe_out_key);
            send_solder_updateme();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public ccxiv_xxv_solder_client(string name_, string pipe_in_key_, string pipe_out_key_) : base(name_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_client::new(string,string,string)");
            xxv.log("name_", name_);
            //----------------------------------------------------------
            pipe_start(pipe_in_key_, pipe_out_key_);
            send_solder_updateme();
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
            xxv.fenter("ccxiv_xxv_solder_client::process_msg");
            //----------------------------------------------------------
            switch(msg_._com) {
                //----------------------------------------------------------
                case (ccxiv_xxv_msg.com.solder_update): { process_solder_update(msg_); break; }
                    //----------------------------------------------------------
                }
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void process_solder_update(ccxiv_xxv_msg msg_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_server::process_solder_update");
            xxv.log("PROCESS SOLDER UPDATE msg_", msg_);
            //----------------------------------------------------------
            vhash hash = msg_._param as vhash;
            _id=hash["_id"] as vsize;
            _manager_id=hash["_manager_id"] as vsize;
            _parent_id=hash["_parent_id"] as vsize;
            //----------------------------------------------------------
            xxv.log("_id", _id);
            xxv.log("_manager_id", _manager_id);
            xxv.log("_parent_id", _parent_id);
            //----------------------------------------------------------
            send_ready();
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void send_solder_updateme() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_client::send_solder_updateme");
            //----------------------------------------------------------
            send(ccxiv_xxv_msg.com.solder_updateme, _name);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void send_ready() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_client::send_ready");
            //----------------------------------------------------------
            send(ccxiv_xxv_msg.com.ready, _name);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        private void pipe_start(string pipe_in_key_, string pipe_out_key_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_solder_client::pipe_start");
            //----------------------------------------------------------
            // Клиент и сервер направлены друг к другу, поэтому ключи меняются местами.
            //----------------------------------------------------------
            xxv.log("pipe_out_key_", pipe_out_key_);
            _pipe_in=new(PipeDirection.In, pipe_out_key_);
            xxv.log("_pipe_in.TransmissionMode", _pipe_in.TransmissionMode);
            pipe_in_stream(_pipe_in);
            //----------------------------------------------------------
            xxv.log("pipe_in_key_", pipe_in_key_);
            _pipe_out=new(PipeDirection.Out, pipe_in_key_);
            xxv.log("_pipe_out.TransmissionMode", _pipe_out.TransmissionMode);
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