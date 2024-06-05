using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZbouraniSkoly2025
{
    public partial class Form1 : Form
    {


        // hlavni nastroje pro 
        Bitmap mobjMainBitmap;
        Graphics mobjBitmapGraphics;

        // grafika pro picturebox
        Graphics mobjPlatnoGraphics;

        // vytvoreni kulicky
        clsKulicka mobjBall;

        // vytvoreni plosiny
        clsPlosina mobjPlosina;

        // vytvoreni cihly
        clsCihla mobjCihla;

        // mackam klavesnici
        bool mbjOvladam;
        bool mbjCihlaNeni;


        // integeru indexujiciho cihlu co ma program znicit potom co se ji dotkla koule
        int mintIndexZnicCihly;

        // vytvoreni okna pri konci hry s moznostmi hrat znova nebo ukoncit program
        DialogResult mDrKonecHry;





        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            // vytvoreni grafiky v pictureboxu
            mobjPlatnoGraphics = pbPlatno.CreateGraphics();

            // vytvoreni hlavni grafiky
            mobjMainBitmap = new Bitmap(pbPlatno.Width, pbPlatno.Height);
            mobjBitmapGraphics = Graphics.FromImage(mobjMainBitmap);

            // nastaveni kulicky
            mobjBall = new clsKulicka(600, 350, 13, 3, 3, mobjBitmapGraphics);

            // nastaveni plosiny
            mobjPlosina = new clsPlosina((int)(mobjPlatnoGraphics.VisibleClipBounds.Width / 2), 500, 100, 10, 6, mobjBitmapGraphics);

            // nastaveni cihel
            mobjCihla = new clsCihla(12, 6, 40, 50, 80, 20, 10, 5, mobjBitmapGraphics);

            // nastaveni timeru
            tmrRedraw.Interval = 30;
            tmrRedraw.Enabled = true;
        }

        //
        // prekresleni obrazu pri ticku timeru
        //
        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            // vymazat platno a grafiku
            mobjBitmapGraphics.Clear(Color.White);

            // nakresli kulicku na bitmapu a jeji kolize
            mobjBall.DrawBall();
            mobjBall.MoveBall();
            mobjBall.KolizeBall();

            // nakresli plosinu a jeji kolize
            mobjPlosina.DrawPlosina();
            if (mbjOvladam == true)
                mobjPlosina.MovePlosina();
            mobjBall.KolizeBallAndPlosina(mobjPlosina.pintPlosinaX, mobjPlosina.pintPlosinaY, mobjPlosina.pintPlosinaWidth);

            // nakresli cihly
            mobjCihla.DrawCihla();
            TestKolizeBallCihla();

            // nakresleni na platno a zastaveni hry
            mobjPlatnoGraphics.DrawImage(mobjMainBitmap, 0, 0);
            if (mobjBall.tmrStop == true)
            {
                tmrRedraw.Enabled = false;
                if (mobjBall.tmrStop == true)
                {
                    StopGame();
                }
            }
        }
        // pohyb plosiny s kontrolou jestli se nedostala k hrane pictureboxu
        private void Form1_KeyDown(object sender, KeyEventArgs Klavesa)
        {
            try
            {
                switch (Klavesa.KeyCode)
                {
                    case Keys.Left:
                        mobjPlosina.PosunLeft();
                        mbjOvladam = true;
                        if (mobjPlosina.pintPlosinaX < 0)
                        {
                            mbjOvladam = false;
                        }
                        break;
                    case Keys.Right:
                        mobjPlosina.PosunRight();
                        mbjOvladam = true;
                        if (mobjPlosina.pintPlosinaX + mobjPlosina.pintPlosinaWidth > mobjPlatnoGraphics.VisibleClipBounds.Width)
                        {
                            mbjOvladam = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                mbjOvladam = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            mbjOvladam = false;
        }

        //
        // kolize cihel s kouli a smazani cihel kterych se dotkne z listu
        //
        private void TestKolizeBallCihla()
        {
            foreach (Rectangle rect in mobjCihla.listRect)
            {
                Rectangle lobjPrekryv;
                lobjPrekryv = Rectangle.Intersect(rect, mobjBall.mobjBallRect);
                if (lobjPrekryv.Width > 0 && lobjPrekryv.Height > 0)
                {
                    mobjBall.mintBallPosunY = mobjBall.mintBallPosunY * (-1);
                    mobjBall.mintBallPosunX = mobjBall.mintBallPosunX + mobjBall.mintRandomPosun;
                    mintIndexZnicCihly = mobjCihla.listRect.IndexOf(rect);
                    mbjCihlaNeni = true;
                }
            }
            if (mbjCihlaNeni == true)
            {
                
                    mobjCihla.listRect.RemoveAt(mintIndexZnicCihly);
                    mbjCihlaNeni = false;
                }
        }


        // ukonci hru, zavre program nebo otevre novou instanci
        public void StopGame()
        {
            mDrKonecHry = MessageBox.Show("Hrát znovu?","GAME OVER",MessageBoxButtons.YesNo);
            if (mDrKonecHry == DialogResult.Yes)
            {
                InitializeComponent();
                Application.Restart();
            }
            if (mDrKonecHry == DialogResult.No) 
            {
                this.Close();
            }
        }
    }
}
