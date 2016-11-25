using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace moni
{
    public class geti//基类，特异的以后继承就可以了,上下调的算是第一期测试用
    {
        /// <summary>
        /// 0开始的特征码，不能重复
        /// </summary>
        


        //只能get的说明检查不应该放在这里，应该在构造的时候先检查一遍
        public uint bianhao { get; }
        
        public class jiaoyishuxingJg
        {
            public chiguleixingMj chiguleixing { get; }
            public duokongMj duokong { get; set; }
            public bool ift { get; }
            public double jiazhitouzi { get; }

            public jiaoyishuxingJg(chiguleixingMj chiguleixing, duokongMj duokong, bool ift, double jiazhitouzi)
            {
                this.chiguleixing = chiguleixing;
                this.duokong = duokong;
                this.ift = ift;
                this.jiazhitouzi = jiazhitouzi;
            }
        }
        public jiaoyishuxingJg jiaoyishuxing { get; set; }
        
        public class yuqiJg
        {
            public int qishishijian { get; set; }
            double qishijiage;
            public int yuanqishijian { get; set; }
            double yuanqijiage;
            double yuanqigailv;
            double gerijiage;

            public double Gerijiage
            {
                get { return gerijiage; }
                set { if (value < 0) throw new ArgumentOutOfRangeException("隔日价格不应小于0"); else gerijiage = value; }
            }
            public double Qishijiage
            {
                get { return this.qishijiage; }
                set { if (value <= 0) throw new ArgumentOutOfRangeException("起始价格不应小于0"); else this.qishijiage = value; }
            }
            public double Yuanqijiage
            {
                get { return yuanqijiage; }
                set { if (value <= 0) throw new ArgumentOutOfRangeException("远期价格不应小于0"); else yuanqijiage = value; }
            }
            public double Yuanqigailv
            {
                get { return yuanqigailv; }
                set { if (value < 0 || value > 1) throw new ArgumentOutOfRangeException("远期概率不应小于0或大于1"); else yuanqigailv = value; }
            }

            public yuqiJg(int qishishijian, double qishijiage, int yuanqishijian, double yuanqijiage,
                double yuanqigailv, double gerijiage)
            {
                this.qishishijian = qishishijian;
                this.qishijiage = qishijiage;
                this.yuanqishijian = yuanqishijian;
                this.yuanqijiage = yuanqijiage;
                this.yuanqigailv = yuanqigailv;
                this.gerijiage = gerijiage;
            }
            public yuqiJg() { }
        }
        public yuqiJg yuqi { set; get; }

        public class chigushujuJg
        {
            double cangwei;
            double chengbenjia;
            public uint chigushu { get; set; }
            double keyongzijin;
            double zijin;
            public uint keyongshuliang { get; set; }

            /// <summary>
            /// 仓位，范围0-1
            /// </summary>
            public double Cangwei
            {
                get { return cangwei; }
                set { if (value < 0 || value > 1) throw new ArgumentOutOfRangeException("仓位不应小于0或大于1"); else cangwei = value; }
            }
            /// <summary>
            /// 成本价
            /// </summary>
            public double Chengbenjia
            {
                get { return chengbenjia; }
                set { if (value <= 0) throw new ArgumentOutOfRangeException("成本价不应小于0"); else chengbenjia = value; }
            }
            public double Keyongzijin
            {
                get { return keyongzijin; }
                set { if (value < 0) throw new ArgumentOutOfRangeException("可用资金不应小于0"); else keyongzijin = value; }
            }
            public double Zijin
            {
                get { return zijin; }
                set { if (value < 0) throw new ArgumentOutOfRangeException("资金不应小于0"); else zijin = value; }
            }

            public chigushujuJg(double cangwei, double chengbenjia, uint chigushu, double keyongzijin, double zijin)
            {
                this.cangwei = cangwei;
                this.chengbenjia= chengbenjia;
                this.chigushu = chigushu;
                this.keyongzijin = keyongzijin;
                this.zijin = zijin;
                this.keyongshuliang = chigushu;
            }
        }
        public chigushujuJg chigushuju { set; get; }

        //只提供函数直接赋值，判断部分由建造者控制
        public geti(uint bianhao, chigushujuJg chigushuju, jiaoyishuxingJg jiaoyishuxing, yuqiJg yuqi)
        {
            this.bianhao = bianhao;
            this.chigushuju = chigushuju;
            this.jiaoyishuxing = jiaoyishuxing;
            this.yuqi = yuqi;
        }
        static double gujia;
        static double zuoshou;
        static int riqi;

        private void huoquXinxi()
        {
            gujia = new gushixitong().huoquGujia();
            riqi = new gushixitong().huoquRiqi();
            zuoshou = new gushixitong().huoquZuoshou();
        }

        public void yuqitiaozheng()
        {
            huoquXinxi();

            //变量设置，方便统一数值以及将来修改
            double chengbenjia = chigushuju.Chengbenjia; //成本价
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - yuqi.Qishijiage) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + yuqi.Qishijiage;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double qushi = 1 - jiazhi;
            double maichujia = yuqi.Qishijiage;

            //赋值
            zuoshou = gujia;

            yuqi.Gerijiage = pinghuayuqi;

            //根据枚举值动态调用方法
            yuqitiaozhengWeituo yqtz = Delegate.CreateDelegate(typeof(yuqitiaozhengWeituo), this,
              jiaoyishuxing.duokong.ToString()
               + jiaoyishuxing.chiguleixing.ToString() + "Yuqitiaozheng"
               ) as yuqitiaozhengWeituo;

            if (yqtz() != "butiaozheng")
            {
                jutiTiaozhengcelue cl= Delegate.CreateDelegate(typeof(jutiTiaozhengcelue), this,
              yqtz() ) as jutiTiaozhengcelue;
                 
                cl();
            }

        }
        private delegate string yuqitiaozhengWeituo();
        private string duoduanYuqitiaozheng()
        {
            double chengbenjia = chigushuju.Chengbenjia; //成本价
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - yuqi.Qishijiage) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + yuqi.Qishijiage;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double qushi = 1 - jiazhi;
            double maichujia = yuqi.Qishijiage;

            Func<bool> daodaYuqijia = () => (gujia >= yuanqijiage);
            Func<bool> daodaTedingshijian1 = () => (yuanqishijian == riqi);
            Func<bool> daodaTedingshijian2 = () => ((int)((yuanqishijian + qishishijian) / 2) == riqi);
            Func<bool> yuangaoyuYuqi = () => (((riqi - qishishijian) > (double)((yuanqishijian - qishishijian) / (double)(4)))/*带分割*/
                                        && (zuoshou - pinghuayuqi > (pinghuayuqi - chigushuju.Chengbenjia)));
            //价格远超预期,超预期收益部分大于预期收益且时间较长，实际判断时间为1/4,1/2处,由于短期的时间在3~10，和1/4较符合，即若涨停基本都是继续持有观望，持有第2天的数据较有效
            Func<bool> yuandiyuYuqi = () =>
            ((zuoshou / pinghuayuqi) - 1 <= -0.05/*带分割*/ && chigushuju.Chengbenjia > zuoshou);//远低于预期且亏损，判断错误，是否多空转换
            Func<bool> tiaozhengGailv = () => ((riqi - qishishijian) > 1);
             
            //逐条判断
            List<Func<bool>> tiaozhengLeixingpanduan = new List<Func<bool>>();
            tiaozhengLeixingpanduan.Add(daodaYuqijia);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian1);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian2);
            tiaozhengLeixingpanduan.Add(yuangaoyuYuqi);
            tiaozhengLeixingpanduan.Add(yuandiyuYuqi);
            tiaozhengLeixingpanduan.Add(tiaozhengGailv);

            
            Dictionary<int, string> biaoqudong = new Dictionary<int, string>()
            { { 0, nameof(daodaYuqijia) }, {1,"daodaTedingshijian1" } , { 2,"daodaTedingshijian2" },
                {3,"yuangaoyuYuqi" }, {4,"yuandiyuYuqi" }, {5,"tiaozhengGailv" } };
            for (int j = 0; j < tiaozhengLeixingpanduan.Count; j++)
            {
                if (tiaozhengLeixingpanduan[j]())
                {
                    return "duoduanYuqitiaozheng_" + biaoqudong[j];
                }
            }
            return "butiaozheng";
        }
        private string duozhongYuqitiaozheng()
        {
            double chengbenjia = chigushuju.Chengbenjia; //成本价
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - yuqi.Qishijiage) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + yuqi.Qishijiage;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double qushi = 1 - jiazhi;
            double maichujia = yuqi.Qishijiage;

            Func<bool> daodaYuqijia = () => (gujia >= yuanqijiage);
            Func<bool> daodaTedingshijian1 = () => (yuanqishijian == riqi);
            Func<bool> daodaTedingshijian2 = () => ((int)((yuanqishijian + qishishijian) / 2) == riqi);
            Func<bool> daodaTedingshijian3 = () => ((int)((yuanqishijian + qishishijian) / 4 * 3) == riqi);
            Func<bool> yuangaoyuYuqi = () => (((riqi - qishishijian) > (double)((yuanqishijian - qishishijian) / (double)(3)))/*带分割*/
                                        && (zuoshou - pinghuayuqi) > (pinghuayuqi - chigushuju.Chengbenjia));
            //价格远超预期,超预期收益部分大于预期收益且时间较长，实际判断时间为1/4,1/2处,由于短期的时间在3~10，和1/4较符合，即若涨停基本都是继续持有观望，持有第2天的数据较有效
            Func<bool> yuandiyuYuqi = () =>
            ((zuoshou / pinghuayuqi) - 1 <= -0.2 && chigushuju.Chengbenjia > zuoshou);//远低于预期且亏损，判断错误，是否多空转换
            Func<bool> tiaozhengGailv = () => ((riqi - qishishijian) > 5);

            //逐条判断
            List<Func<bool>> tiaozhengLeixingpanduan = new List<Func<bool>>();
            tiaozhengLeixingpanduan.Add(daodaYuqijia);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian1);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian2);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian3);
            tiaozhengLeixingpanduan.Add(yuangaoyuYuqi);
            tiaozhengLeixingpanduan.Add(yuandiyuYuqi);
            tiaozhengLeixingpanduan.Add(tiaozhengGailv);

            Dictionary<int, string> biaoqudong = new Dictionary<int, string>()
            { { 0, "daodaYuqijia" }, {1,"daodaTedingshijian1" } , { 2,"daodaTedingshijian2" },
              { 3,"daodaTedingshijian3" },  {4,"yuangaoyuYuqi" }, {5,"yuandiyuYuqi" }, {6,"tiaozhengGailv" } };
            for (int j = 0; j < tiaozhengLeixingpanduan.Count; j++)
            {
                if (tiaozhengLeixingpanduan[j]())
                {
                    return "duozhongYuqitiaozheng_" + biaoqudong[j];
                }
            }
            return "butiaozheng";
        }
        private string duochangYuqitiaozheng()
        {
            double chengbenjia = chigushuju.Chengbenjia; //成本价
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - chigushuju.Chengbenjia) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + chigushuju.Chengbenjia;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double qushi = 1 - jiazhi;
            double maichujia = yuqi.Qishijiage;

            Func<bool> daodaYuqijia = () => (gujia >= yuanqijiage);
            Func<bool> daodaTedingshijian1 = () => (yuanqishijian == riqi);
            Func<bool> daodaTedingshijian2 = () => ((int)((yuanqishijian + qishishijian) / 2) == riqi);
            Func<bool> daodaTedingshijian3 = () => ((int)((yuanqishijian + qishishijian) / 4 * 3) == riqi);
            Func<bool> yuangaoyuYuqi = () => (((riqi - qishishijian) > (double)((yuanqishijian - qishishijian) / (double)(3)))/*带分割*/
                                        && (zuoshou - pinghuayuqi > (pinghuayuqi - chigushuju.Chengbenjia)));
            //价格远超预期,超预期收益部分大于预期收益且时间较长，实际判断时间为1/4,1/2处,由于短期的时间在3~10，和1/4较符合，即若涨停基本都是继续持有观望，持有第2天的数据较有效
            Func<bool> yuandiyuYuqi = () =>
            ((zuoshou / chigushuju.Chengbenjia) - 1 <= -0.2/*带分割*/);//巨幅亏损，判断错误，是否多空转换，这里是成本价，而非和预期比
            Func<bool> tiaozhengGailv = () => ((riqi - qishishijian) > 20);

            //逐条判断
            List<Func<bool>> tiaozhengLeixingpanduan = new List<Func<bool>>();
            tiaozhengLeixingpanduan.Add(daodaYuqijia);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian1);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian2);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian3);
            tiaozhengLeixingpanduan.Add(yuangaoyuYuqi);
            tiaozhengLeixingpanduan.Add(yuandiyuYuqi);
            tiaozhengLeixingpanduan.Add(tiaozhengGailv);

            Dictionary<int, string> biaoqudong = new Dictionary<int, string>()
            { { 0, "daodaYuqijia" }, {1,"daodaTedingshijian1" } , { 2,"daodaTedingshijian2" },
              { 3,"daodaTedingshijian3" },  {4,"yuangaoyuYuqi" }, {5,"yuandiyuYuqi" }, {6,"tiaozhengGailv" } };
            for (int j = 0; j < tiaozhengLeixingpanduan.Count; j++)
            {
                if (tiaozhengLeixingpanduan[j]())
                {
                    return "duochangYuqitiaozheng" + biaoqudong[j];
                }
            }
            return "butiaozheng";
        }
        private string kongduanYuqitiaozheng()
        {
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - chigushuju.Chengbenjia) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + chigushuju.Chengbenjia;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double maichujia = yuqi.Qishijiage;
            pinghuayuqi = (maichujia - yuanqijiage) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + yuanqijiage;

            Func<bool> daodaYuqijia = () => (gujia <= yuanqijiage);
            Func<bool> daodaTedingshijian1 = () => (yuanqishijian == riqi);
            Func<bool> daodaTedingshijian2 = () => ((int)((yuanqishijian + qishishijian) / 2) == riqi);
            Func<bool> yuangaoyuYuqi = () => (((riqi - qishishijian) > (double)((yuanqishijian - qishishijian) / (double)(4)))
                                        && ((maichujia - pinghuayuqi) < (pinghuayuqi - zuoshou)));
            //价格远超预期,超预期收益部分大于预期收益且时间较长，实际判断时间为1/4,1/2处,由于短期的时间在3~10，和1/4较符合，即若涨停基本都是继续持有观望，持有第2天的数据较有效
            Func<bool> yuandiyuYuqi = () =>
            ((zuoshou / pinghuayuqi) - 1 >= 0.05 && maichujia < zuoshou);//远低于预期且亏损，判断错误，是否多空转换
            Func<bool> tiaozhengGailv = () => ((riqi - qishishijian) > 1);

            //逐条判断
            List<Func<bool>> tiaozhengLeixingpanduan = new List<Func<bool>>();
            tiaozhengLeixingpanduan.Add(daodaYuqijia);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian1);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian2);
            tiaozhengLeixingpanduan.Add(yuangaoyuYuqi);
            tiaozhengLeixingpanduan.Add(yuandiyuYuqi);
            tiaozhengLeixingpanduan.Add(tiaozhengGailv);

            Dictionary<int, string> biaoqudong = new Dictionary<int, string>()
            { { 0, "daodaYuqijia" }, {1,"daodaTedingshijian1" } , { 2,"daodaTedingshijian2" },
                {3,"yuangaoyuYuqi" }, {4,"yuandiyuYuqi" }, {5,"tiaozhengGailv" } };
            for (int j = 0; j < tiaozhengLeixingpanduan.Count; j++)
            {
                if (tiaozhengLeixingpanduan[j]())
                {
                    return "kongduanYuqitiaozheng" + biaoqudong[j];
                }
            }
            return "butiaozheng";
        }
        private string kongzhongYuqitiaozheng()
        {
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - chigushuju.Chengbenjia) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + chigushuju.Chengbenjia;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double maichujia = yuqi.Qishijiage;
            pinghuayuqi = (maichujia - yuanqijiage) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + yuanqijiage;

            Func<bool> daodaYuqijia = () => (gujia <= yuanqijiage);
            Func<bool> daodaTedingshijian1 = () => (yuanqishijian == riqi);
            Func<bool> daodaTedingshijian2 = () => ((int)((yuanqishijian + qishishijian) / 2) == riqi);
            Func<bool> daodaTedingshijian3 = () => ((int)((yuanqishijian + qishishijian) / 4 * 3) == riqi);
            Func<bool> yuangaoyuYuqi = () => (((riqi - qishishijian) > (double)((yuanqishijian - qishishijian) / (double)(3)))
                                         && ((maichujia - pinghuayuqi) < (pinghuayuqi - zuoshou)));
            //价格远超预期,超预期收益部分大于预期收益且时间较长，实际判断时间为1/4,1/2处,由于短期的时间在3~10，和1/4较符合，即若涨停基本都是继续持有观望，持有第2天的数据较有效
            Func<bool> yuandiyuYuqi = () =>
            ((zuoshou / pinghuayuqi) - 1 >= 0.2 && maichujia < zuoshou);//超出预期，判断错误，是否多空转换
            Func<bool> tiaozhengGailv = () => ((riqi - qishishijian) > 5);

            //逐条判断
            List<Func<bool>> tiaozhengLeixingpanduan = new List<Func<bool>>();
            tiaozhengLeixingpanduan.Add(daodaYuqijia);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian1);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian2);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian3);
            tiaozhengLeixingpanduan.Add(yuangaoyuYuqi);
            tiaozhengLeixingpanduan.Add(yuandiyuYuqi);
            tiaozhengLeixingpanduan.Add(tiaozhengGailv);

            Dictionary<int, string> biaoqudong = new Dictionary<int, string>()
            { { 0, "daodaYuqijia" }, {1,"daodaTedingshijian1" } , { 2,"daodaTedingshijian2" },
              { 3,"daodaTedingshijian3" },  {4,"yuangaoyuYuqi" }, {5,"yuandiyuYuqi" }, {6,"tiaozhengGailv" } };
            for (int j = 0; j < tiaozhengLeixingpanduan.Count; j++)
            {
                if (tiaozhengLeixingpanduan[j]())
                {
                    return "kongzhongYuqitiaozheng" + biaoqudong[j];
                }
            }
            return "butiaozheng";
        }
        private string kongchangYuqitiaozheng()
        {
            double yuanqijiage = yuqi.Yuanqijiage; //远期价格
            int yuanqishijian = yuqi.yuanqishijian;//远期时间
            int qishishijian = yuqi.qishishijian;//起始时间
            double pinghuayuqi = (yuanqijiage - chigushuju.Chengbenjia) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + chigushuju.Chengbenjia;
            double jiazhi = jiaoyishuxing.jiazhitouzi;
            double maichujia = yuqi.Qishijiage;
            pinghuayuqi = (maichujia - yuanqijiage) / (yuanqishijian - qishishijian) * (riqi - qishishijian) + yuanqijiage;

            Func<bool> daodaYuqijia = () => (gujia <= yuanqijiage);
            Func<bool> daodaTedingshijian1 = () => (yuanqishijian == riqi);
            Func<bool> daodaTedingshijian2 = () => ((int)((yuanqishijian + qishishijian) / 2) == riqi);
            Func<bool> daodaTedingshijian3 = () => ((int)((yuanqishijian + qishishijian) / 4 * 3) == riqi);
            Func<bool> yuangaoyuYuqi = () => (((riqi - qishishijian) > (double)((yuanqishijian - qishishijian) / (double)(3)))
                                         && ((maichujia - pinghuayuqi) < (pinghuayuqi - zuoshou)));
            //价格远超预期,超预期收益部分大于预期收益且时间较长，实际判断时间为1/4,1/2处,由于短期的时间在3~10，和1/4较符合，即若涨停基本都是继续持有观望，持有第2天的数据较有效
            Func<bool> yuandiyuYuqi = () =>
            ((zuoshou / maichujia) - 1 >= 0.2);//巨幅亏损，判断错误，是否多空转换，这里是成本价，而非和预期比
            Func<bool> tiaozhengGailv = () => ((riqi - qishishijian) > 20);

            //逐条判断
            List<Func<bool>> tiaozhengLeixingpanduan = new List<Func<bool>>();
            tiaozhengLeixingpanduan.Add(daodaYuqijia);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian1);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian2);
            tiaozhengLeixingpanduan.Add(daodaTedingshijian3);
            tiaozhengLeixingpanduan.Add(yuangaoyuYuqi);
            tiaozhengLeixingpanduan.Add(yuandiyuYuqi);
            tiaozhengLeixingpanduan.Add(tiaozhengGailv);

            Dictionary<int, string> biaoqudong = new Dictionary<int, string>()
            { { 0, "daodaYuqijia" }, {1,"daodaTedingshijian1" } , { 2,"daodaTedingshijian2" },
              { 3,"daodaTedingshijian3" },  {4,"yuangaoyuYuqi" }, {5,"yuandiyuYuqi" }, {6,"tiaozhengGailv" } };
            for (int j = 0; j < tiaozhengLeixingpanduan.Count; j++)
            {
                if (tiaozhengLeixingpanduan[j]())
                {
                    return "kongchangYuqitiaozheng" + biaoqudong[j];
                }
            }
            return "butiaozheng";
        }


        private void duoduanYuqitiaozheng_daodaYuqijia()
        {

            if (((zuoshou - chigushuju.Chengbenjia) / chigushuju.Chengbenjia / (double)(riqi - yuqi.qishishijian) < 0.005/*带分割*/)
                                        || (new Random().NextDouble() > (1 - jiaoyishuxing.jiazhitouzi)))
            //转换类型
            {
                duoduanZhuankong();
            }
            else  //根据趋势上调
            {
                yuqi.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(3, 11);
                //按平滑趋势上调预期价格
                yuqi.Yuanqijiage = zuoshou * (1.0 + 0.01 * new Random().Next(3, 11));
            }
        }
        private void duoduanYuqitiaozheng_daodaTedingshijian1()
        {
            if (((zuoshou - chigushuju.Chengbenjia) / chigushuju.Chengbenjia / (double)(riqi -yuqi. qishishijian) < 0.003/*带分割*/)
                                            || (new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                duoduanZhuankong();
            }
            else//保持预期，延长持有时间
            {
               yuqi. yuanqishijian = riqi + new Random().Next(3, 11);
            }
        }
        private void duoduanYuqitiaozheng_daodaTedingshijian2()
        {
            if (chigushuju.Chengbenjia > zuoshou)//成本大于昨收即亏损，转空卖出（根据远期概率定）或下调
            {
                if (new Random().NextDouble() >yuqi. Yuanqigailv) //信心不足认为判断错误，转空卖出
                {
                    duoduanZhuankong();
                }
                else if (new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi))//按趋势下调，否则不调整
                {
                    
                    yuqi.Yuanqijiage -= (yuqi.Yuanqijiage - chigushuju.Chengbenjia) * (0.1 * (new Random().Next(4, 7)));
                }
            }
            //低于或高于，下调或上调，远期和隔日不太一样,高于预期且调整，即平滑调整,当趋势判断为正时
            else  if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = chigushuju.Chengbenjia + (zuoshou - chigushuju.Chengbenjia) * 2;/*带分割*/
            }
        }
        private void duoduanYuqitiaozheng_yuangaoyuYuqi()
        {
            if (new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi))
            {
                yuqi.Yuanqijiage += (zuoshou - chigushuju.Chengbenjia) * (new Random().NextDouble());/*带分割*/
            }
        }
        private void duoduanYuqitiaozheng_yuandiyuYuqi()
        {
            if (new Random().NextDouble() > yuqi.Yuanqigailv)//每天都可能判断，等于二项分布
            {
                duoduanZhuankong();
            }
        }
        private void duoduanYuqitiaozheng_tiaozhengGailv()
        {
            if ((zuoshou < yuqi.Gerijiage) && (yuqi.Yuanqigailv > 0.1))//概率调整
            {/*带分割*/
                yuqi.Yuanqigailv -= 0.1 * (chigushuju.Chengbenjia > zuoshou ? 1 : (yuqi.Gerijiage - zuoshou) / (yuqi.Gerijiage - chigushuju.Chengbenjia));
            }
            else if (zuoshou > yuqi.Gerijiage && (yuqi.Yuanqigailv < 0.90))//概率调整
            {
                yuqi.Yuanqigailv += 0.1 * (zuoshou - yuqi.Gerijiage) / (yuqi.Yuanqijiage - yuqi.Gerijiage);
            }
        }
        private void duozhongYuqitiaozheng_daodaYuqijia()
        {
            if (((zuoshou - chigushuju.Chengbenjia) / chigushuju.Chengbenjia / (double)(riqi - yuqi.qishishijian) < 0.004/*带分割*/)
                                            || (new Random().NextDouble() > (1-jiaoyishuxing.jiazhitouzi))
                                            || (new Random().NextDouble() < yuqi.Yuanqigailv))
            {
                duozhongZhuankong();
            }
            else  //根据趋势上调
            {
                yuqi.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
                //按平滑趋势上调预期价格
                yuqi.Yuanqijiage = zuoshou * (1.0 + 0.01 * new Random().Next(15, 50));
            }
        }
        private void duozhongYuqitiaozheng_daodaTedingshijian1()
        {
            //预期不再，转空卖出
            if ((new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                duozhongZhuankong();
            }
            //远低于预期，延长时间并下调预期
            else if ((zuoshou - chigushuju.Chengbenjia) / chigushuju.Chengbenjia / (double)(riqi - yuqi.qishishijian) < 0.002/*带分割*/)
            {
                yuqi.Yuanqijiage = zuoshou + (yuqi.Yuanqijiage - zuoshou) * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
            }
            //保持预期，延长持有时间
            else
            {
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
            }
        }
        private void duozhongYuqitiaozheng_daodaTedingshijian2()
        {
            if (chigushuju.Chengbenjia > zuoshou)//成本大于昨收即亏损，转空卖出（根据远期概率定）或下调
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //转空卖出
                {
                    duozhongZhuankong();
                }
                else if (new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi))//按趋势下调，否则不调整
                {
                    yuqi.Yuanqijiage -= (yuqi.Yuanqijiage - chigushuju.Chengbenjia) * (0.1 * (new Random().Next(4, 7))/*带分割*/);
                }
            }
            //高于不调整
            else if (new Random().NextDouble() > yuqi.Yuanqigailv)//判断偏离的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = chigushuju.Chengbenjia + (zuoshou - chigushuju.Chengbenjia) * 2;//按一半时间是一半的预期来重新设定远期的
            }
        }
        private void duozhongYuqitiaozheng_daodaTedingshijian3()
        {
            if (chigushuju.Chengbenjia > zuoshou)//成本大于昨收即亏损，转空卖出（根据远期概率定）或下调，被逼空
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //转空卖出
                {
                    duozhongZhuankong();
                }
            }
            else if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = chigushuju.Chengbenjia + (zuoshou - chigushuju.Chengbenjia) / 4.0 * 3.0;//同上
            }
        }
        private void duozhongYuqitiaozheng_yuangaoyuYuqi()
        {
            if ((new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi)) && (new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                yuqi.Yuanqijiage += (zuoshou - chigushuju.Chengbenjia) * (new Random().NextDouble());
            }
        }
        private void duozhongYuqitiaozheng_yuandiyuYuqi()
        {
            if (new Random().NextDouble() > yuqi.Yuanqigailv)//每天都可能判断，等于二项分布
            {
                duozhongZhuankong();
            }
        }
        private void duozhongYuqitiaozheng_tiaozhengGailv()
        {
            if ((zuoshou < yuqi.Gerijiage) && (yuqi.Yuanqigailv > 0.2/*带分割*/))//概率调整
            {
                yuqi.Yuanqigailv -= 0.01 * (chigushuju.Chengbenjia > zuoshou ? 1 : (yuqi.Gerijiage - zuoshou) / (yuqi.Gerijiage - chigushuju.Chengbenjia));
            }/*带分割*/
            else if (zuoshou > yuqi.Gerijiage && (yuqi.Yuanqigailv < 0.90))//概率调整
            {
                yuqi.Yuanqigailv += 0.01 * (zuoshou - yuqi.Gerijiage) / (yuqi.Yuanqijiage - yuqi.Gerijiage);
            }
        }
        private void duochangYuqitiaozheng_daodaYuqijia()
        {
            //不调整，转空卖出，长线不判断趋势，对预期的把握大，只稍微判断概率，可能上涨过快调整***和短线比有数值调整
            if ((new Random().NextDouble() < yuqi.Yuanqigailv) && (new Random().NextDouble() > (1-jiaoyishuxing.jiazhitouzi)))
            {
                duochangZhuankong();
            }
            else //按趋势的部分上调部分，也按中线的数值，即更多的是由于趋势而非长线的价值
            {/*带分割*/
                yuqi.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
                //按平滑趋势上调预期价格
                yuqi.Yuanqijiage = zuoshou * (1.0 + 0.01 * new Random().Next(15, 50));
            }
        }
        private void duochangYuqitiaozheng_daodaTedingshijian1()
        {
            //预期不再，转空卖出
            if ((new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                duochangZhuankong();
            }
            //远低于预期且预期有变，延长时间并下调预期
            else if (((zuoshou - chigushuju.Chengbenjia) / chigushuju.Chengbenjia / (double)(riqi - yuqi.qishishijian) < 0.0005)
                && (yuqi.Yuanqigailv < new Random().NextDouble()))
            {
                yuqi.Yuanqijiage = zuoshou + (yuqi.Yuanqijiage - zuoshou) * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(120, 721);
            }
            //保持预期，延长持有时间
            else
            {
                yuqi.yuanqishijian = riqi + new Random().Next(120, 721);
            }
        }
        private void duochangYuqitiaozheng_daodaTedingshijian2()
        {
            if (chigushuju.Chengbenjia > zuoshou)//成本大于昨收即亏损，（根据远期概率定）下调，不卖出
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //按概率下调
                {
                    yuqi.Yuanqijiage -= (yuqi.Yuanqijiage - chigushuju.Chengbenjia) * (0.1 * (new Random().Next(4, 7)));
                }
            }
            //高于不调整
        }
        private void duochangYuqitiaozheng_daodaTedingshijian3()
        {
            if (chigushuju.Chengbenjia > zuoshou)//成本大于昨收即亏损，下调，同样不卖出，但多一个调整
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //转空卖出
                {
                    duochangZhuankong();
                }
            }
            else if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = chigushuju.Chengbenjia + (zuoshou - chigushuju.Chengbenjia) * 4.0 / 3.0;
            }
        }
        private void duochangYuqitiaozheng_yuangaoyuYuqi()
        {
            //是否趋势性决定是否上调,先按股价直接平滑增加
            if ((new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi)) && (new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                yuqi.Yuanqijiage += (zuoshou - chigushuju.Chengbenjia) * (new Random().NextDouble());
            }
        }
        private void duochangYuqitiaozheng_yuandiyuYuqi()
        {
            if (new Random().NextDouble() > yuqi.Yuanqigailv)//每天都可能判断，等于二项分布
            {
                duochangZhuankong();
            }
        }
        private void duochangYuqitiaozheng_tiaozhengGailv()
        {
            if ((zuoshou < yuqi.Gerijiage) && (yuqi.Yuanqigailv > 0.3))//概率调整
            {
                yuqi.Yuanqigailv -= 0.001/*带分割*/ * (chigushuju.Chengbenjia > zuoshou ? 1 : (yuqi.Gerijiage - zuoshou) / (yuqi.Gerijiage - chigushuju.Chengbenjia));
            }/*带分割*/
            else if (zuoshou > yuqi.Gerijiage && (yuqi.Yuanqigailv < 0.95))//概率调整
            {
                yuqi.Yuanqigailv += 0.001/*带分割*/ * (zuoshou - yuqi.Gerijiage) / (yuqi.Yuanqijiage - yuqi.Gerijiage);
            }
        }
        private void kongduanYuqitiaozheng_daodaYuqijia()
        {
            //认为预期到达，转为观望，不论是不是因为延长时间的到达
            if ((new Random().NextDouble() > (1-jiaoyishuxing.jiazhitouzi)))
            {
                kongduanZhuanguanwang();
            }
            else  //根据趋势继续下调
            {
                yuqi.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(3, 11);
                yuqi.Yuanqijiage = zuoshou * (1.0 - 0.01 * new Random().Next(3, 11));
            }
        }
        private void kongduanYuqitiaozheng_daodaTedingshijian1()
        {
            //转变预期，其他情况交给其他的判断
            if ((new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                kongduanZhuanguanwang();
            }
            //保持预期，延长持有时间
            else
            {
                yuqi.yuanqishijian = riqi + new Random().Next(3, 11);
            }
        }
        private void kongduanYuqitiaozheng_daodaTedingshijian2()
        {
            if (zuoshou > yuqi.Qishijiage)//不下跌反而上涨，（根据远期概率定）判断失误
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //转为调整
                {
                    kongduanZhuanguanwang();
                }
                else if (new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi))//被逼多，按趋势上调，否则不调整
                {
                    yuqi.Yuanqijiage += (yuqi.Qishijiage - yuqi.Yuanqijiage) * (0.1 * (new Random().Next(4, 7)));
                }
            }
            //当对自己判断不确定时，用此时的价格线性预测，改变预期
            else  if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = yuqi.Qishijiage + (zuoshou - yuqi.Qishijiage) * 2;
            }
        }

        private void kongduanYuqitiaozheng_yuangaoyuYuqi()
        {
            //是否趋势性决定是否上调,先按股价直接平滑
            if (new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi))
            {
                yuqi.Yuanqijiage += (zuoshou - yuqi.Qishijiage) * (new Random().NextDouble());
            }
        }
        private void kongduanYuqitiaozheng_yuandiyuYuqi()
        {
            if (new Random().NextDouble() > yuqi.Yuanqigailv)//每天都可能判断，等于二项分布
            {
                kongduanZhuanguanwang();
            }
        }
        private void kongduanYuqitiaozheng_tiaozhengGailv()
        {
            if ((zuoshou > yuqi.Gerijiage) && (yuqi.Yuanqigailv > 0.1))//概率调整
            {/*带分割*/
                yuqi.Yuanqigailv -= 0.1 * (yuqi.Qishijiage < zuoshou ? 1 : (yuqi.Gerijiage - zuoshou) / (yuqi.Gerijiage - yuqi.Qishijiage));
            }
            else if (zuoshou < yuqi.Gerijiage && (yuqi.Yuanqigailv < 0.90))//概率调整
            {
                yuqi.Yuanqigailv += 0.1 * (zuoshou - yuqi.Gerijiage) / (yuqi.Yuanqijiage - yuqi.Gerijiage);
            }
        }
        private void kongzhongYuqitiaozheng_daodaYuqijia()
        {
            //认为预期到达，转为观望，不论是不是因为延长时间的到达
            if ((new Random().NextDouble() > (1-jiaoyishuxing.jiazhitouzi)) || (new Random().NextDouble() < yuqi.Yuanqigailv))
            {
                kongzhongZhuanguanwang();
            }
            else  //根据趋势上调
            {
                yuqi.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
                //按平滑趋势上调预期价格
                yuqi.Yuanqijiage = zuoshou * (1.0 - 0.01 * new Random().Next(14, 26));
            }
        }
        private void kongzhongYuqitiaozheng_daodaTedingshijian1()
        {
            //预期不再，转空卖出
            if ((new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                kongzhongZhuanguanwang();
            }
            //不设远低于预期 
            //保持预期，延长持有时间
            else
            {
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
            }
        }
        private void kongzhongYuqitiaozheng_daodaTedingshijian2()
        {
            if (yuqi.Qishijiage < zuoshou)//不下跌反而上涨
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //转空卖出
                {
                    kongzhongZhuanguanwang();
                }
                else if (new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi))//按趋势上调为一半左右，否则不调整
                {
                    yuqi.Yuanqijiage -= (yuqi.Yuanqijiage - yuqi.Qishijiage) * (0.1 * (new Random().Next(4, 7)));
                }
            }
            //高于不调整
            else if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = yuqi.Qishijiage + (zuoshou - yuqi.Qishijiage) * 2;
            }
        }
        private void kongzhongYuqitiaozheng_daodaTedingshijian3()
        {
            if (yuqi.Qishijiage < zuoshou)//预期严重错误，方向相反
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //被逼多
                {
                    kongzhongZhuanguanwang();
                }
            }
            else if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = yuqi.Qishijiage + (zuoshou - yuqi.Qishijiage) * 2;
            }
        }
        private void kongzhongYuqitiaozheng_yuangaoyuYuqi()
        {
            if ((new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi)) && (new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                yuqi.Yuanqijiage += (zuoshou - yuqi.Qishijiage) * (new Random().NextDouble());
            }
        }
        private void kongzhongYuqitiaozheng_yuandiyuYuqi()
        {
            if (new Random().NextDouble() > yuqi.Yuanqigailv)//每天都可能判断，等于二项分布
            {
                kongzhongZhuanguanwang();
            }
        }
        private void kongzhongYuqitiaozheng_tiaozhengGailv()
        {
            if ((zuoshou > yuqi.Gerijiage) && (yuqi.Yuanqigailv > 0.2))//概率调整
            {
                yuqi.Yuanqigailv -= 0.01 * (yuqi.Qishijiage < zuoshou ? 1 : (yuqi.Gerijiage - zuoshou) / (yuqi.Gerijiage - yuqi.Qishijiage));
            }
            else if (zuoshou < yuqi.Gerijiage && (yuqi.Yuanqigailv < 0.90))//概率调整
            {
                yuqi.Yuanqigailv += 0.01 * (zuoshou - yuqi.Gerijiage) / (yuqi.Yuanqijiage - yuqi.Gerijiage);
            }
        }
        private void kongchangYuqitiaozheng_daodaYuqijia()
        {
            //不调整，转空卖出，长线不判断趋势，对预期的把握大，只稍微判断概率，可能上涨过快调整***和短线比有数值调整
            if ((new Random().NextDouble() < yuqi.Yuanqigailv) || (new Random().NextDouble() > (1-jiaoyishuxing.jiazhitouzi)))
            {
                kongchangZhuanguanwang();
            }
            else //按趋势的部分上调部分，长线也按中线的数值
            {
                yuqi.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
                yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
                //按平滑趋势上调预期价格
                yuqi.Yuanqijiage = zuoshou * (1.0 - 0.01 * new Random().Next(14, 26));
            }
        }
        private void kongchangYuqitiaozheng_daodaTedingshijian1()
        {
            //预期不再，转空卖出
            if ((new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                kongchangZhuanguanwang();
            }
            //卖出部分的均放弃远低于预期且预期有变，下调预期
            //保持预期，延长持有时间
            else
            {
                yuqi.yuanqishijian = riqi + new Random().Next(120, 721);
            }
        }
        private void kongchangYuqitiaozheng_daodaTedingshijian2()
        {
            if (yuqi.Qishijiage < zuoshou)//判断失误，上调，不卖出
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //按概率上调到中间一半
                {
                    yuqi.Yuanqijiage -= (yuqi.Yuanqijiage - yuqi.Qishijiage) * (0.1 * (new Random().Next(4, 7)));
                }
            }
            //高于不调整
        }
        private void kongchangYuqitiaozheng_daodaTedingshijian3()
        {
            if (yuqi.Qishijiage < zuoshou)//不下跌反而上涨，预期错误
            {
                if (new Random().NextDouble() > yuqi.Yuanqigailv) //转空卖出
                {
                    kongchangZhuanguanwang();
                }
            }
            else if (new Random().NextDouble() > yuqi.Yuanqigailv)//偏离判断的程度来决定是否重设预期
            {
                yuqi.Yuanqijiage = yuqi.Qishijiage + (zuoshou - yuqi.Qishijiage) * 2;
            }
        }
        private void kongchangYuqitiaozheng_yuangaoyuYuqi()
        {
            //是否趋势性决定是否上调,先按股价直接平滑增加
            if ((new Random().NextDouble() < (1-jiaoyishuxing.jiazhitouzi)) && (new Random().NextDouble() > yuqi.Yuanqigailv))
            {
                yuqi.Yuanqijiage += (zuoshou - yuqi.Qishijiage) * (new Random().NextDouble());
            }
        }
        private void kongchangYuqitiaozheng_yuandiyuYuqi()
        {
            if (new Random().NextDouble() > yuqi.Yuanqigailv)//每天都可能判断，等于二项分布
            {
                kongchangZhuanguanwang();
            }
        }
        private void kongchangYuqitiaozheng_tiaozhengGailv()
        {
            if ((zuoshou > yuqi.Gerijiage) && (yuqi.Yuanqigailv > 0.3/*带分割*/))//概率调整
            {
                yuqi.Yuanqigailv -= 0.001 * (yuqi.Qishijiage > zuoshou ? 1 : (yuqi.Gerijiage - zuoshou) / (yuqi.Gerijiage - yuqi.Qishijiage));
            }
            else if (zuoshou < yuqi.Gerijiage && (yuqi.Yuanqigailv < 0.95))//概率调整
            {
                yuqi.Yuanqigailv += 0.001 * (zuoshou - yuqi.Gerijiage) / (yuqi.Yuanqijiage - yuqi.Gerijiage);
            }
        }
        private delegate void jutiTiaozhengcelue();

        private void duoduanZhuankong()
        {
            jiaoyishuxing.duokong = duokongMj.kong;
            yuqi.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();
            yuqi.Yuanqijiage = zuoshou * (1 - 0.01 * (new Random().Next(3, 11)));
            yuqi.yuanqishijian = riqi + new Random().Next(3, 11);
            yuqi.qishishijian = riqi; //等于卖出时间，不改了
            yuqi.Qishijiage = zuoshou;
        }
        private void duozhongZhuankong()
        {
            jiaoyishuxing.duokong = duokongMj.kong;
            yuqi.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
            yuqi.Yuanqijiage = zuoshou * (1 - 0.01 * (new Random().Next(14, 26)));
            yuqi.yuanqishijian = riqi + new Random().Next(15, 61);
            yuqi.qishishijian = riqi; //等于卖出时间，不改了
            yuqi.Qishijiage = zuoshou;
        }
        private void duochangZhuankong()
        {
            jiaoyishuxing.duokong = duokongMj.kong;
            yuqi.Yuanqigailv = 0.8 + 0.15 * new Random().NextDouble();
            yuqi.Yuanqijiage = zuoshou * (1 - 0.01 * (new Random().Next(20, 41)));
            yuqi.yuanqishijian = riqi + new Random().Next(120, 721);
            yuqi.qishishijian = riqi; //等于卖出时间，不改了
            yuqi.Qishijiage = zuoshou;
        }
        private void kongduanZhuanguanwang()
        {
            jiaoyishuxing.duokong = duokongMj.guanwang;
            yuqi.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();
            yuqi.Yuanqijiage = zuoshou * (1 + 0.001 * (new Random().Next(-10, 11)));
            yuqi.yuanqishijian = riqi + new Random().Next(2, 4);
            yuqi.qishishijian = riqi; 
        }
        private void kongzhongZhuanguanwang()
        {
            jiaoyishuxing.duokong = duokongMj.guanwang;
            yuqi.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
            yuqi.Yuanqijiage = zuoshou * (1 + 0.001 * (new Random().Next(-10, 11)));
            yuqi.yuanqishijian = riqi + new Random().Next(3, 6);
            yuqi.qishishijian = riqi;
        }
        private void kongchangZhuanguanwang()
        {
            jiaoyishuxing.duokong = duokongMj.guanwang;
            yuqi.Yuanqigailv = 0.8 + 0.15 * new Random().NextDouble();
            yuqi.Yuanqijiage = zuoshou * (1 + 0.001 * (new Random().Next(-10, 11)));
            yuqi.yuanqishijian = riqi + new Random().Next(3, 11);
            yuqi.qishishijian = riqi;
        }
    }
}
