using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mikroklimat.Classes
{
    public class imgCrop
    {
        public System.Drawing.Bitmap bmap { get; set; }
        public imgCrop(ref System.Drawing.Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            scale_find(bmp, ref width, ref height);
            bmp = new_large_bitmap(bmp, width, height);
            bmp = new_small_bitmap(bmp);
        }
        private double scale(double a, double b)
        {
            return a / b;
        }

        private void scale_find(System.Drawing.Bitmap bm, ref int width, ref int height)
        {
            double sc_1 = 0, sc_2 = 0;

            if (bm.Width > 1200 || bm.Height > 900)
            {
                scale_function(ref sc_1, ref sc_2, bm.Width, bm.Height, 1200, 900);


                if (sc_1 >= 1 || sc_2 >= 1)
                {
                    if (sc_1 >= 1)
                        height = (int)Math.Round((double)900 / sc_1);

                    else if (sc_2 >= 1)
                        width = (int)Math.Round((double)1200 / sc_2);
                }

                else
                {
                    double coef_1 = bm.Width / 1200.0, coef_2 = bm.Height / 900.0; int coef = 1;

                    if (coef_1 != coef_2)
                    {
                        coef = coefficient(coef_1, coef_2);
                        scale_function(ref sc_1, ref sc_2, bm.Width, bm.Height, 1200, 900, coef);
                        change_size(ref width, ref height, sc_1, sc_2, 1200, 900, coef);

                    }

                }
            }

            else
            {
                if (bm.Width <= 832 || bm.Height <= 624)
                {
                    scale_function(ref sc_1, ref sc_2, bm.Width, bm.Height, 832, 624);
                    change_size(ref width, ref height, sc_1, sc_2, 832, 624);

                }
                else if ((bm.Width > 832 || bm.Height > 624) && (bm.Width <= 1200 || bm.Height <= 900))
                {
                    scale_function(ref sc_1, ref sc_2, bm.Width, bm.Height, 1200, 900);
                    change_size(ref width, ref height, sc_1, sc_2, 1200, 900);
                }
            }
        }

        private void change_size(ref int width, ref int height, double sc_1, double sc_2, int rat_width, int rat_height)
        {
            if (sc_1 > sc_2)
                height = (int)Math.Round((double)rat_height / sc_1);

            else if (sc_1 < sc_2)
                width = (int)Math.Round((double)rat_width / sc_2);
        }

        private void change_size(ref int width, ref int height, double sc_1, double sc_2, int rat_width, int rat_height, int coef)
        {
            if (sc_1 > sc_2)
                height = (int)Math.Round((double)(rat_height * coef) / sc_1);
            else if (sc_1 < sc_2)
                width = (int)Math.Round((double)(rat_width * coef) / sc_2);
        }

        private int coefficient(double coef_1, double coef_2)
        {
            if (coef_1 > coef_2)
                return (int)coef_1 + 1;
            else if (coef_1 < coef_2)
                return (int)coef_2 + 1;
            else
                return (int)coef_1;
        }

        private void scale_function(ref double sc_1, ref double sc_2, int width, int height, int rat_width, int rat_height)
        {
            sc_1 = scale(rat_width, width);
            sc_2 = scale(rat_height, height);
        }

        private void scale_function(ref double sc_1, ref double sc_2, int width, int height, int rat_width, int rat_height, int coef)
        {
            sc_1 = scale(rat_width * coef, width);
            sc_2 = scale(rat_height * coef, height);
        }
        private System.Drawing.Bitmap new_large_bitmap(System.Drawing.Bitmap bm, int width, int height)
        {
            return bm.Clone(new System.Drawing.Rectangle((bm.Width - width) / 2, (bm.Height - height) / 2, width, height), bm.PixelFormat);
        }


        public System.Drawing.Bitmap new_small_bitmap(System.Drawing.Bitmap bm)
        {
            return new System.Drawing.Bitmap(bm,320,240);
        }

    }
}