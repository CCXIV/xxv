//----------------------------------------------------------
// 214.su
// 2022-09-25
//----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------
using ccxiv;

using ccxiv_xxv;

using ccxiv_xxv_tenet;
//----------------------------------------------------------
namespace ccxiv_xxv_helloscroll {
    //----------------------------------------------------------
    internal class ccxiv_xxv_helloscroll_replica {
        //----------------------------------------------------------
        public ccxiv_xxv_tenet_universe _universe = new();
        int _texture_stars = 0;
        int _texture_enemy = 0;
        int _program0 = 0;
        int _world_game = 0;
        int _plane_stars = 0;
        int _plane_aster = 0;
        //----------------------------------------------------------
        public vsize _tenet_id = new(-1);
        public bool _tenet_ready = false;
        //----------------------------------------------------------
        public ccxiv_xxv_helloscroll_replica() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_helloscroll_replica::new");
            //----------------------------------------------------------
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void init() {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_helloscroll_replica::init");
            //----------------------------------------------------------
            Random rand = new();
            //----------------------------------------------------------
            // Текстуры
            //----------------------------------------------------------
            ccxiv_xxv_tenet_texture texture_stars = new();
            texture_stars._name="stars";
            texture_stars._sfn=@"ccxiv_xxv_helloscroll\texture_star.bean";
            _texture_stars=_universe._textures.add(texture_stars);
            //----------------------------------------------------------
            ccxiv_xxv_tenet_texture texture_enemy = new();
            texture_enemy._name="enemy";
            texture_enemy._sfn=@"ccxiv_xxv_helloscroll\texture_enemy.bean";
            _texture_enemy=_universe._textures.add(texture_enemy);
            //----------------------------------------------------------
            // Шейдеры
            //----------------------------------------------------------
            ccxiv_xxv_tenet_program program0 =new();
            program0._name="slo";
            program0._sfn=@"ccxiv_xxv_helloscroll\program_slo.bean";
            _program0=_universe._programs.add(program0);
            //----------------------------------------------------------
            // Звёзды
            //----------------------------------------------------------
            ccxiv_xxv_tenet_plane plane_stars = new(_universe) {
                _texture_id=_texture_stars,
                _program_id=_program0
                };
            //----------------------------------------------------------
            for(int i = 0; i<20; i++) {
                //----------------------------------------------------------
                ccxiv_xxv_tenet_spirit spirit_star = new();
                //----------------------------------------------------------
                spirit_star._box._center.X=(float)(rand.NextDouble()*2.0f-0.5f);
                spirit_star._box._center.Y=(float)(rand.NextDouble()*2.0f-0.5f);
                spirit_star._box._size.X=0.04f;
                spirit_star._box._size.Y=0.04f;
                spirit_star._box._frame=rand.Next(0,15);
                //----------------------------------------------------------
                spirit_star._anima.start_goal(0.001f, spirit_star._box._center.X,1,0, spirit_star._box);
                //----------------------------------------------------------
                plane_stars._spirits.add(spirit_star);
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            // Астероиды
            //----------------------------------------------------------
            ccxiv_xxv_tenet_plane plane_aster = new(_universe) {
                _texture_id=_texture_enemy,
                _program_id=_program0
                };
            //----------------------------------------------------------
            for(int i = 0; i<20; i++) {
                //----------------------------------------------------------
                ccxiv_xxv_tenet_spirit spirit_aster = new();
                spirit_aster._box._center.X=(float)(rand.NextDouble()*2.0f-0.5f);
                spirit_aster._box._center.Y=(float)(rand.NextDouble()*2.0f-0.5f);
                spirit_aster._box._size.X=0.14f;
                spirit_aster._box._size.Y=0.14f;
                spirit_aster._box._frame=rand.Next(0, 15);
                //----------------------------------------------------------
                spirit_aster._anima.start_goal(0.0001f, spirit_aster._box._center.X, 1, 0, spirit_aster._box);
                //----------------------------------------------------------
                plane_aster._spirits.add(spirit_aster);
                //----------------------------------------------------------
                }
            //----------------------------------------------------------
            // Мир игры
            //----------------------------------------------------------
            ccxiv_xxv_tenet_world world_game = new(_universe);
            //----------------------------------------------------------
            _plane_stars=world_game._planes.add(plane_stars);
            _plane_aster=world_game._planes.add(plane_aster);
            //----------------------------------------------------------
            // Вселенная
            //----------------------------------------------------------
            _world_game=_universe._worlds.add(world_game);
            //----------------------------------------------------------
            xxv.fexit();
            //----------------------------------------------------------
            }
        //----------------------------------------------------------
        public void send_universe_update(ccxiv_xxv_solder_client solder_) {
            //----------------------------------------------------------
            xxv.fenter("ccxiv_xxv_helloscroll_replica::send_universe_update");
            //----------------------------------------------------------
            solder_.send(_tenet_id, ccxiv_xxv_msg.com.tenet_universe_update, _universe.bean_write());
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