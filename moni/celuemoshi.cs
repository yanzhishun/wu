using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moni
{
    
    interface celuemoshi//动态类名调用，不要修改类名
    {
        void huoquCelue(ref geti.yuqiJg yq);
    }
    class  duoDUAN : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(3, 11);//先设总时间

            yq.qishishijian =-5+ new Random().Next(0,4);//-5~-2
            yq.Yuanqigailv =0.5+0.2* new Random().NextDouble();//0.5~0.7
            yq.yuanqishijian=yq.qishishijian+ chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage=yq.Qishijiage*(1+0.01* new Random().Next(3,11));
        }
    }
    class kongDUAN : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(3, 11);//先设总时间

            yq.qishishijian =   new Random().Next(-2, 1);
            yq.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 - 0.01 * new Random().Next(3, 11));
        }
    }
    class guanwangDUAN : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(3, 11);//先设总时间

            yq.qishishijian = -5 + new Random().Next(0, 5+1);
            yq.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.001 * (new Random().Next(-10, 11)));
        }
    }
    class jiancangDUAN : celuemoshi
    {
        public  void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(3, 11);//先设总时间

            yq.qishishijian =  new Random().Next(-3, 0);
            yq.Yuanqigailv = 0.5 + 0.2 * new Random().NextDouble();//0.5~0.7
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.01 * new Random().Next(3, 11));
        }
    }
    class duoZHONG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(15, 61);//先设总时间

            yq.qishishijian = -37 + new Random().Next(0, 34);
            yq.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.01 * new Random().Next(15, 51));
        }
    }
    class kongZHONG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(15, 61);//先设总时间

            yq.qishishijian = -7 + new Random().Next(0, 4);
            yq.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 - 0.01 * new Random().Next(14, 26));
        }
    }
    class guanwangZHONG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(15, 61);//先设总时间

            yq.qishishijian = -37 + new Random().Next(0, 34);
            yq.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.001 * new Random().Next(-10, 11));
        }
    }
    class jiancangZHONG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(15, 61);//先设总时间

            yq.qishishijian = -7 + new Random().Next(0, 5);
            yq.Yuanqigailv = 0.6 + 0.3 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.01 * new Random().Next(15, 51));
        }
    }
    class duoCHANG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(120, 721);//先设总时间

            yq.qishishijian = -420 + new Random().Next(0, 410);
            yq.Yuanqigailv = 0.8 + 0.15 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.01 * new Random().Next(30, 201));
        }
    }
    class kongCHANG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(120, 721);//先设总时间

            yq.qishishijian = -15 + new Random().Next(0, 11);
            yq.Yuanqigailv = 0.8 + 0.15 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 - 0.01 * new Random().Next(20, 40));
        }
    }
    class guanwangCHANG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(120, 721);//先设总时间

            yq.qishishijian = -420 + new Random().Next(0, 410);
            yq.Yuanqigailv = 0.8 + 0.15 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.001 * new Random().Next(-10, 11));
        }
    }
    class jiancangCHANG : celuemoshi
    {
        public void huoquCelue(ref geti.yuqiJg yq)
        {
            int chiyoushijian = new Random().Next(120, 721);//先设总时间

            yq.qishishijian = -15 + new Random().Next(0, 14);
            yq.Yuanqigailv = 0.8 + 0.15 * new Random().NextDouble();
            yq.yuanqishijian = yq.qishishijian + chiyoushijian;
            if (yq.yuanqishijian < 0)
                yq.yuanqishijian = 0;
            yq.Yuanqijiage = yq.Qishijiage * (1 + 0.01 * new Random().Next(30, 201));
        }
    }

    
}
