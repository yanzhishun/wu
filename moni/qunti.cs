using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace moni
{
    class qunti
    {
        static int getirenshu;
        geti[] getishuzu ;
        public geti[] Getishuzu { get { return getishuzu; } set { getishuzu = value; } }
        double gujia;
        double zuoshou;
        double zuidi;
        double zuigao;
        
        public qunti()
        {
            //数量从配置文件中读取
            Configuration config = ConfigurationManager.OpenExeConfiguration(@"moni.exe");
            string sl = config.AppSettings.Settings["shuliang"].Value;
            
            getirenshu = int.Parse(sl);
            getishuzu = new geti[getirenshu];
        }

        public void tiaokongshengcheng()
        {

            double gujia = 60.0;
            double zonggushu = 2.455 * 10000 * 10000;
            int renshu = getirenshu;
            //先实例测试，实际调控这些参数
            int duanxianRenshu = 3500;
            int zhongxianRenshu = 3500;
            int changxianRenshu = 3000;
            int duofanRenshu = 3;
            int kongfangRenshu = 1;
            int guanwangRenshu = 4;
            int jiancangRenshu = 1;
            int renshuZuot = 1;
            int renshuBuzuot = 1;
            double[] shizhifenbu = new double[8];
            int[,] shizhibiao = new int[8, 2];

            string text = System.IO.File.ReadAllText(@"E:\数据\shizhifenbu.txt");
            string[] sr = text.Split(',');
            for (int i = 0; i < 8; i++)
            {
                shizhifenbu[i] = double.Parse(sr[i]);
            }
            text = System.IO.File.ReadAllText(@"E:\数据\shizhibiao.txt");
              sr = text.Split(';');
            for(int i = 0; i < 8; i++)
            {
                string[] srr = sr[i].Split(',');
                shizhibiao[i, 0] =int.Parse( srr[0]);
                shizhibiao[i, 1] = int.Parse(srr[1]);
            }

            for (int i = 0; i < getirenshu; i++)
            {
                zhengtaicanshu zt= new zhengtaicanshu(gujia, zonggushu, renshu,
                    duanxianRenshu, zhongxianRenshu, changxianRenshu,
                    duofanRenshu, kongfangRenshu, guanwangRenshu, jiancangRenshu,
                    renshuZuot,renshuBuzuot,
                    shizhifenbu, shizhibiao);
                Getishuzu[i] = new zhengtaifenbuQunti(zt).huoqujieguo();
            }
        }
        
        public void yuqitiaozheng()
        {
            for(int i = 0; i < getirenshu; i++)
            {
                getishuzu[i].yuqitiaozheng();
            }
        }
    }
    
    public abstract class canshulei
    {
    }
    //参数类继承实例
    public class zhengtaicanshu : canshulei
    {
        public zhengtaicanshu(double gujia, double zonggushu, int renshu,
        int duanxianRenshu, int zhongxianRenshu, int changxianRenshu,
        int duofangRenshu, int kongfangRenshu, int guanwangRenshu, int jiancangRenshu,
        int renshuZuot, int renshuBuzuot,
        double[] shizhifenbu, int[,] shizhibiao)
        {
            this.gujia = gujia; this.zonggushu = zonggushu; this.renshu = renshu;
            this.duanxianRenshu = duanxianRenshu; this.zhongxianRenshu = zhongxianRenshu; this.changxianRenshu = changxianRenshu;
            this.duofangRenshu = duofangRenshu; this.kongfangRenshu = kongfangRenshu; this.guanwangRenshu = guanwangRenshu; this.jiancangRenshu = jiancangRenshu;
            this.renshuZuot = renshuZuot; this.renshuBuzuot = renshuBuzuot;
            this.shizhifenbugailv = shizhifenbu; this.shizhibiao = shizhibiao;
        }

        public double gujia { get; }
        public double zonggushu { get; }
        public int renshu { get; }
        public int duanxianRenshu { get; }
        public int zhongxianRenshu { get; }
        public int changxianRenshu { get; }
        public int duofangRenshu { get; }
        public int kongfangRenshu { get; }
        public int guanwangRenshu { get; }
        public int jiancangRenshu { get; }
        public int renshuZuot { get; }
        public int renshuBuzuot { get; }
        public double[] shizhifenbugailv { get; }
        public int[,] shizhibiao { get; }
    }
}
