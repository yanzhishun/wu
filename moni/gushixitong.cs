using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moni
{
    class gushixitong
    {
        static double gujia;
        static int riqi;
        static int shijian;
        static double zuoshou;
        static double zhangtingjia;
        static double dietingjia;
        static double kaipan;
        static double zuidi;
        static double zuigao;
        static double shoupan;

        public double huoquGujia()
        {
            return gujia;
        }

        public int huoquRiqi()
        {
            return riqi;
        }
        public double huoquZuoshou()
        {
            return zuoshou;
        }
        qunti moniqunti = new qunti();
        SortedDictionary<double, uint> weimai3duilie = new SortedDictionary<double, uint>();//这个等同于实际看到的
        SortedDictionary<double, uint> weimai4duilie = new SortedDictionary<double, uint>();
        SortedSet<weimai3Shuju> weimai3jihe = new SortedSet<weimai3Shuju>();//使用两个数据结构，这个是包含编号的
        SortedSet<weimai4Shuju> weimai4jihe = new SortedSet<weimai4Shuju>();

        geti caozuogeti;

          private void rudui(weimai3Shuju wt) //委买数据入队
        {
            weimai3jihe.Add(wt);
            if (weimai3duilie.ContainsKey(wt.jiage))
            {
                weimai3duilie[wt.jiage] += wt.shuliang;
            }
            else
            {
                weimai3duilie.Add(wt.jiage, wt.shuliang);
            }
        }
          private void rudui(weimai4Shuju wt) //委卖数据入队
        {
            weimai4jihe.Add(wt);
            if (weimai4duilie.ContainsKey(wt.jiage))
            {
                weimai4duilie[wt.jiage] += wt.shuliang;
            }
            else
            {
                weimai4duilie.Add(wt.jiage, wt.shuliang);
            }
        }
          private void chudui(maimaileixingMj m)//默认出队第一个
        {
            switch (m)
            {
                case maimaileixingMj.mai3:
                    weimai3jihe.Remove(weimai3jihe.First());
                    weimai3duilie[weimai3jihe.First().jiage] -= weimai3jihe.First().shuliang;
                    if (weimai3duilie[weimai3jihe.First().jiage] == 0)
                    {
                        weimai3duilie.Remove(weimai3jihe.First().jiage);
                    }
                    break;
                case maimaileixingMj.mai4:
                    weimai4jihe.Remove(weimai4jihe.First());
                    weimai4duilie[weimai4jihe.First().jiage] -= weimai4jihe.First().shuliang;
                    if (weimai4duilie[weimai4jihe.First().jiage] == 0)
                    {
                        weimai4duilie.Remove(weimai4jihe.First().jiage);
                    }
                    break;
            }
        }

         public void weituoXingwei(weimai3Shuju wt)
        {
            //调整价格和纠错
            if (wt.jiage <= 0) { throw new ArgumentOutOfRangeException("委托价不应小于或等于0"); }
            if (wt.jiage > zhangtingjia)
            {
                wt.jiage = zhangtingjia;
            }
            if (wt.jiage < dietingjia)
            {
                wt.jiage = dietingjia;
            }
            wt.jiage = Math.Round(wt.jiage, 2);

            //调整可用资金
            moniqunti.Getishuzu[wt.bianhao].chigushuju.Keyongzijin -= wt.jiage * wt.shuliang;
            //根据判断来确定行为


            while (weimai4jihe.Count > 0 && wt.shuliang > 0 && weimai4jihe.First().jiage <= wt.jiage)
            {
                chengjiao(ref wt, weimai4jihe.First());
                if (weimai4jihe.First().shuliang == 0)
                    chudui(maimaileixingMj.mai4);
            }
            if (wt.shuliang > 0)//不成交,即委卖为0或委卖价大于委买价
            {
                rudui(wt);
            }

        }
        private void chengjiao(ref weimai3Shuju zhudongdan, weimai4Shuju weituodan)
        {
            if (zhudongdan.jiage < weituodan.jiage)
            {
                throw new ArgumentOutOfRangeException("成交的委买价格小于委卖价格");
            }

            uint zdbianhao = zhudongdan.bianhao;
            uint wtbianhao = weituodan.bianhao;
            geti.chigushujuJg zhudongchigushuju =
            moniqunti.Getishuzu[zdbianhao].chigushuju;
            geti.chigushujuJg weituochigushuju =
                moniqunti.Getishuzu[zdbianhao].chigushuju;
            //确定数量和价格
            uint chengjiaoliang;
            double chengjiaojia;
            chengjiaojia = weituodan.jiage;
            if (zhudongdan.shuliang < weituodan.shuliang)
            {
                chengjiaoliang = zhudongdan.shuliang;
            }
            else
            {
                chengjiaoliang = weituodan.shuliang;
            }
            //主动数据修改
            zhudongdan.shuliang -= chengjiaoliang;

            zhudongchigushuju.Chengbenjia = (zhudongchigushuju.Chengbenjia * zhudongchigushuju.chigushu +
                chengjiaoliang * chengjiaojia) / (zhudongchigushuju.chigushu + chengjiaoliang);
            zhudongchigushuju.chigushu += chengjiaoliang;
            zhudongchigushuju.Zijin -= chengjiaojia * chengjiaoliang;
            zhudongchigushuju.Cangwei = zhudongchigushuju.chigushu * chengjiaojia / (zhudongchigushuju.chigushu * chengjiaojia + zhudongchigushuju.Zijin);
            zhudongchigushuju.Keyongzijin += (chengjiaojia - zhudongdan.jiage) * chengjiaoliang;


            //委托数据修改
            weituodan.shuliang -= chengjiaoliang;

            weituochigushuju.Chengbenjia = (weituochigushuju.Chengbenjia * weituochigushuju.chigushu -
                chengjiaoliang * chengjiaojia) / (weituochigushuju.chigushu - chengjiaoliang);
            weituochigushuju.chigushu -= chengjiaoliang;
            weituochigushuju.Zijin += chengjiaojia * chengjiaoliang;
            weituochigushuju.Cangwei = weituochigushuju.chigushu * chengjiaojia / (weituochigushuju.chigushu * chengjiaojia + weituochigushuju.Zijin);
            weituochigushuju.Keyongzijin += chengjiaoliang * chengjiaojia;


        }
        private void weituoXingwei(weimai4Shuju wt)
        {
            //调整价格和纠错
            if (wt.jiage <= 0) { throw new ArgumentOutOfRangeException("委托价不应小于或等于0"); }
            if (wt.jiage > zhangtingjia)
            {
                wt.jiage = zhangtingjia;
            }
            if (wt.jiage < dietingjia)
            {
                wt.jiage = dietingjia;
            }
            wt.jiage = Math.Round(wt.jiage, 2);

            //调整可用数量
            moniqunti.Getishuzu[wt.bianhao].chigushuju.keyongshuliang -= wt.shuliang;
            //根据判断来确定行为

            while (weimai3jihe.Count > 0 && wt.shuliang > 0 && weimai3jihe.First().jiage >= wt.jiage)
            {
                chengjiao(ref wt, weimai3jihe.First());
                if (weimai3jihe.First().shuliang == 0)
                    chudui(maimaileixingMj.mai3);
            }
            if (wt.shuliang > 0)//不成交,即委买为0或委买价小于委卖价
            {
                rudui(wt);
            }
        }
        private void chengjiao(ref weimai4Shuju zhudongdan, weimai3Shuju weituodan)
        {
            if (zhudongdan.jiage > weituodan.jiage)
            {
                throw new ArgumentOutOfRangeException("成交的委买价格小于委卖价格");
            }

            uint zdbianhao = zhudongdan.bianhao;
            uint wtbianhao = weituodan.bianhao;
            geti.chigushujuJg zhudongchigushuju =
            moniqunti.Getishuzu[zdbianhao].chigushuju;
            geti.chigushujuJg weituochigushuju =
                moniqunti.Getishuzu[zdbianhao].chigushuju;
            //确定数量和价格
            uint chengjiaoliang;
            double chengjiaojia;
            chengjiaojia = weituodan.jiage;
            if (zhudongdan.shuliang < weituodan.shuliang)
            {
                chengjiaoliang = zhudongdan.shuliang;
            }
            else
            {
                chengjiaoliang = weituodan.shuliang;
            }
            //主动数据修改
            zhudongdan.shuliang -= chengjiaoliang;

            zhudongchigushuju.Chengbenjia = (zhudongchigushuju.Chengbenjia * zhudongchigushuju.chigushu -
                chengjiaoliang * chengjiaojia) / (zhudongchigushuju.chigushu - chengjiaoliang);
            zhudongchigushuju.chigushu -= chengjiaoliang;
            zhudongchigushuju.Zijin += chengjiaojia * chengjiaoliang;
            zhudongchigushuju.Cangwei = zhudongchigushuju.chigushu * chengjiaojia / (zhudongchigushuju.chigushu * chengjiaojia + zhudongchigushuju.Zijin);

            //委托数据修改
            weituodan.shuliang -= chengjiaoliang;

            weituochigushuju.Chengbenjia = (weituochigushuju.Chengbenjia * weituochigushuju.chigushu +
                chengjiaoliang * chengjiaojia) / (weituochigushuju.chigushu + chengjiaoliang);
            weituochigushuju.chigushu += chengjiaoliang;
            weituochigushuju.Zijin -= chengjiaojia * chengjiaoliang;
            weituochigushuju.Cangwei = weituochigushuju.chigushu * chengjiaojia / (weituochigushuju.chigushu * chengjiaojia + weituochigushuju.Zijin);

        }

        public void yuqitiaozheng()
        {


        }
        public void jiaoyiLiucheng()
        {

        }

        public void test()
        {

        }


    }
    internal class weimai3Shuju : IComparable //测试通过，委买数据，接口从大到小排序
    {
        public uint bianhao { get; }
        public double jiage { get; set; }
        public uint shuliang { set; get; }//只有数量可以修改

        internal weimai3Shuju(uint bianhao, double jiage, uint shuliang)
        {
            if (jiage < 0)
                throw new ArgumentOutOfRangeException("委买价格小于0");
            this.bianhao = bianhao;
            this.jiage = jiage;
            this.shuliang = shuliang;
        }

        public int CompareTo(object obj)
        {
            weimai3Shuju qita = obj as weimai3Shuju;
            int result = this.jiage.CompareTo(qita.jiage) * -1;
            if (result == 0) { result = 1; }
            return result;
        }
    }
    internal class weimai4Shuju : IComparable //测试通过，委卖数据，接口从小到大排序
    {
        public uint bianhao { get; }
        public double jiage { get; set; }
        public uint shuliang { set; get; }//只有数量可以修改

        internal weimai4Shuju(uint bianhao, double jiage, uint shuliang)
        {
            if (jiage < 0)
                throw new ArgumentOutOfRangeException("委卖价格小于0");
            this.bianhao = bianhao;
            this.jiage = jiage;
            this.shuliang = shuliang;
        }

        public int CompareTo(object obj)
        {
            weimai4Shuju qita = obj as weimai4Shuju;
            int result = this.jiage.CompareTo(qita.jiage);
            if (result == 0) { result = 1; }
            return result;
        }
    }

}
