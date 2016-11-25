using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace moni
{
    public abstract class jianzaozhe
    {
        protected Random suijishu = new Random();

        //等于模型就是分布+分类，分类数目由指导者动态决定，然后抽象类可以添加或删除分类
        //部件创建可以加判断，容易定位问题，然后实际由构造函数的工序决定
        protected geti getin { get; set; }
        protected static uint ibbb = 0;
        protected static uint bianhao = 0;

        protected geti.chigushujuJg chigushuju;
        protected geti.jiaoyishuxingJg jiaoyishuxing;
        protected geti.yuqiJg yuqi=new geti.yuqiJg();

        protected void chuangjianBianhao()
            //只能由建造者自己创建，即创建对象时自动编号，不会重复，不用抽象
        {
            bianhao=ibbb++;// 从0开始递增
        }

        public abstract void chigushujuBujian(canshulei canshu);
        public abstract void jiaoyishuxingBujian(canshulei canshu);
        public abstract void yuqiBujian(canshulei canshu);
        
    }
    public class zhengtaifenbuQunti : jianzaozhe
    {

        public override void chigushujuBujian(canshulei canshu)
        {
            zhengtaicanshu zz =(zhengtaicanshu) canshu;
            double cangwei=-1;
            double chengbenjia=-1;
            uint chigushu=0;
            double keyongzijin=-1;
            double zijin=-1;

            //cangwei赋值
            switch (jiaoyishuxing.duokong)
            {
                case duokongMj.duo:
                    cangwei = 0.55 + 0.3 * 0.01 * suijishu.Next(100);
                    break;

                case duokongMj.kong:
                    cangwei = 0.85 - 0.6 * 0.01 * suijishu.Next(100);
                    break;

                case duokongMj.guanwang:
                    cangwei = 0;
                    break;

                case duokongMj.jiancang:
                    cangwei = 0.5 * 0.01 * suijishu.Next(100);
                    break;
            }
            //成本价赋值
            chengbenjia = zz.gujia * (1 + 0.05 / 100.0 * suijishu.Next(-100, 101));

            //资金赋值，要读取资金分布，总额和其他由指导者部分完成，这里只负责根据参数创建，以及判断创建出的数据是否服从规范
            double shizhi;
            double suijishubb =zz.shizhifenbugailv[zz.shizhifenbugailv.Length-1]* suijishu.NextDouble();
            int count = 0;
            //计算市值在哪一个分布内
            double taaa = 0;
            foreach(var vbbb in zz.shizhifenbugailv)
            {
                 
                if (suijishubb < vbbb&&suijishubb>taaa)
                {
                    break;
                }
                taaa = vbbb;
                count++;
            }
            //根据类型赋值
            shizhi = suijishu.Next(zz.shizhibiao[count, 0], zz.shizhibiao[count, 1]);

            
            switch (jiaoyishuxing.duokong)
            {
                case duokongMj.duo://
                    chigushu = (uint)(shizhi / zz.gujia);
                    zijin = shizhi / cangwei * (1 - cangwei);
                    keyongzijin = zijin;
                    break;
                case duokongMj.guanwang:
                    zijin = shizhi;
                    chigushu = 0;
                    keyongzijin = zijin;
                    break;
                case duokongMj.jiancang://市值即总资金
                    chigushu = (uint)(shizhi / zz.gujia);
                    zijin = shizhi  * (1 - cangwei);
                    keyongzijin = zijin;
                    break;
                case duokongMj.kong:
                    chigushu = (uint)(shizhi / zz.gujia);
                    zijin = shizhi / cangwei * (1 - cangwei);
                    keyongzijin = zijin;
                    break;
            }
            chigushuju = new geti.chigushujuJg(cangwei, chengbenjia, chigushu, keyongzijin, zijin);
            
        }

        //只依托外部参数，可以直接创建
        public override void jiaoyishuxingBujian(canshulei canshu)
        {
            zhengtaicanshu zz = (zhengtaicanshu)canshu;
            //用于构建交易属性结构体的中间变量
            chiguleixingMj chiguleixing;
            duokongMj duokong;
            bool ift;
            double jiazhitouzi;

            //价值投资
            jiazhitouzi = suijishu.NextDouble();
            //chengbenjia赋值,先用平均值
            

            //持股类型赋值
            int cg = zz.duanxianRenshu + zz.zhongxianRenshu + zz.changxianRenshu;
            int zbbb = suijishu.Next(cg);
            if (zbbb < zz.duanxianRenshu)
            {
                chiguleixing = chiguleixingMj.duan;
            }
            else if (zbbb >= zz.duanxianRenshu + zz.zhongxianRenshu)
            {
                chiguleixing = chiguleixingMj.chang;
            }
            else
            {
                chiguleixing = chiguleixingMj.zhong;
            }

            //多空赋值以及相关值的赋值，下同
            int dk = zz.duofangRenshu + zz.kongfangRenshu + zz.guanwangRenshu + zz.jiancangRenshu;
            zbbb = suijishu.Next(dk);
            if (zbbb < zz.duofangRenshu)
            {
                duokong = duokongMj.duo;
            }
            else if (zbbb >= zz.duofangRenshu && zbbb < zz.duofangRenshu + zz.kongfangRenshu)
            {
                duokong = duokongMj.kong;
            }
            else if (zbbb >= zz.duofangRenshu + zz.kongfangRenshu && zbbb < zz.duofangRenshu + zz.kongfangRenshu + zz.guanwangRenshu)
            {
                duokong = duokongMj.guanwang;
            }
            else
            {
                duokong = duokongMj.jiancang;
            }

            //zuot赋值
            int zt = zz.renshuBuzuot + zz.renshuZuot;
            zbbb = suijishu.Next(zt);
            if (zbbb < zz.renshuZuot)
            {
                ift = true;
            }
            else
            {
                ift = false;
            }

            //加判断，其他枚举不会超出范围
            if(jiazhitouzi>1 || jiazhitouzi < 0)
            {
                throw new ArgumentOutOfRangeException("价值投资属性值超出范围");
            }
            jiazhitouzi = Math.Round(jiazhitouzi, 3);
            jiaoyishuxing = new geti.jiaoyishuxingJg(chiguleixing, duokong, ift, jiazhitouzi);
        }

        public override void yuqiBujian(canshulei canshu)
        {
            double gerijige =-1 ;
            double qishijiage=-1;
            int qishishijian = -10000;
            double yuanqigailv = -1;
            double yuanqijiage = -1;
            int yuanqishijian = -10000;

            gerijige = 0;
            qishijiage = chigushuju.Chengbenjia * (1 + 0.05 / 100.0 * suijishu.Next(-100, 101));

            //赋值函数内生成的值
            yuqi.Gerijiage = gerijige;
            yuqi.Qishijiage = qishijiage;

            //根据类名动态创建对象，规则是枚举名相加，第二个开始大写,先后顺序先随意
            string leiming = "moni." + jiaoyishuxing.duokong.ToString() +
                jiaoyishuxing.chiguleixing.ToString().ToUpper();
            object o = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(leiming, false);
            if (o == null)
            {
                throw new ArgumentNullException("预期赋值策略不完整或其他错误");
            }
            else//赋值预期的其他值
            { 
            celuemoshi c = o as celuemoshi;
            c.huoquCelue(ref yuqi);
            }
            
        }

        
        public zhengtaifenbuQunti(canshulei canshu)//判断范围和创建以及规范格式
        {
            //创建
            chuangjianBianhao();
            jiaoyishuxingBujian(canshu);
            chigushujuBujian(canshu);
            yuqiBujian(canshu);
            

            //范围判断再规范格式

            if (chigushuju.Cangwei < 0 || chigushuju.Cangwei > 1)
                throw new ArgumentOutOfRangeException("创建仓位出错");
            if(chigushuju.Chengbenjia<0)
                throw new ArgumentOutOfRangeException("创建成本价出错");
            if(chigushuju.Keyongzijin<0)
                throw new ArgumentOutOfRangeException("创建可用资金出错");
            if(chigushuju.Zijin<0)
                throw new ArgumentOutOfRangeException("创建资金出错");

            if(jiaoyishuxing.jiazhitouzi<0||jiaoyishuxing.jiazhitouzi>1)
                throw new ArgumentOutOfRangeException("创建价值投资属性出错");

            if(yuqi.Qishijiage<0)
                throw new ArgumentOutOfRangeException("创建起始价格出错");
            if(yuqi.Yuanqigailv<0)
                throw new ArgumentOutOfRangeException("创建远期概率出错");
            if(yuqi.Gerijiage<0)
                throw new ArgumentOutOfRangeException("创建隔日价格出错");
            if(yuqi.Yuanqijiage<0)
                throw new ArgumentOutOfRangeException("创建远期价格出错");

            chigushuju.Cangwei = Math.Round(chigushuju.Cangwei, 3);
            chigushuju.Chengbenjia = Math.Round(chigushuju.Chengbenjia, 2);
            chigushuju.Keyongzijin = Math.Round(chigushuju.Keyongzijin, 2);
            chigushuju.Zijin = Math.Round(chigushuju.Zijin, 2);

            yuqi.Gerijiage = Math.Round(yuqi.Gerijiage, 2);
            yuqi.Qishijiage = Math.Round(yuqi.Qishijiage, 2);
            yuqi.Yuanqigailv = Math.Round(yuqi.Yuanqigailv, 3);
            yuqi.Yuanqijiage = Math.Round(yuqi.Yuanqijiage, 2);

            getin = new geti(bianhao, chigushuju, jiaoyishuxing, yuqi);
        }

        public geti huoqujieguo()
        {
            return getin;
        }
    }
   
}
