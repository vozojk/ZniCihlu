﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZbouraniSkoly2025
{
    internal class clsCihla
    {
        // list vsech cihel 
        public List<Rectangle> listRect = new List<Rectangle>();

        // kreslici platno
        Graphics mobjGrafika;

        Rectangle mobjCihlaRect;

        // souradnice cihly
        int mintCihlaX, mintCihlaY;
        int mintCihlaWidth, mintCihlaHeight;
        int mintCihlaRozestupX, mintCihlaRozestupY;

        

        // barva cihel
        Brush mobjCihlaBrush;

        //
        // konstruktor
        //
        public clsCihla(int intPocetCihelX, int intPocetCihelY, int intCihlaX, int intCihlaY, int intCihlaWidth, int intCihlaHeight, int intCihlaRozestupX, int intCihlaRozestupY, Graphics objGrafika) 
        {
            mintCihlaHeight = intCihlaHeight;
            mintCihlaWidth = intCihlaWidth;
            mintCihlaX = intCihlaX;
            mintCihlaY = intCihlaY;
            mobjGrafika = objGrafika;
            mobjCihlaBrush = new SolidBrush(Color.OrangeRed);
            mintCihlaRozestupX = intCihlaRozestupX;
            mintCihlaRozestupY = intCihlaRozestupY;
            mobjCihlaRect = new Rectangle(mintCihlaX, mintCihlaY, mintCihlaWidth, mintCihlaHeight);


            // vytvoreni vsech cihel do listu
            for (int x = 0; x < intPocetCihelX; x++)
            {
                
                mobjCihlaRect.X = x * (mintCihlaRozestupX + mintCihlaWidth) + mintCihlaX;
                for (int y = 0; y < intPocetCihelY; y++)
                {
                    mobjCihlaRect.Y = y * (mintCihlaRozestupY + mintCihlaHeight) + mintCihlaY ;
                    listRect.Add(mobjCihlaRect);
                }
            }

        }
        //
        // nakresleni cihel
        //
        public void DrawCihla()
        {
            foreach (Rectangle rect in listRect)
            {
                mobjGrafika.FillRectangle(mobjCihlaBrush, rect);
            }
        }
    }
}
