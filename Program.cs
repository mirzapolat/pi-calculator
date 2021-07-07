using System;
using System.Drawing;

/// <summary>
/// Calculates PI
/// </summary>
namespace pi_calculator
{
    class Program
    {
        private static bool create_bitmap = false;

        static void Main(string[] args)
        {
            long points;
            Bitmap bmp = new Bitmap(1, 1);
            int max_points_for_image = 30000;

            Console.Title = "PI CALUCATOR by mirza";

            // start
            Console.WriteLine();
            Console.WriteLine("------------------- PI CALUCATOR by mirza -----------------");
            Console.WriteLine();
            Console.WriteLine("Remember that there will be x Rows with x columns. \nSo get a cup of tea and make your choice...");

            Console.Write("Amount of random Points per Row/Column: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            points = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;

            if (points <= max_points_for_image)
            {
                Console.Write("Create Bitmap as .png? (Y/N): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string createbmp = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (createbmp == "y" || createbmp == "Y") create_bitmap = true;
                else create_bitmap = false;
            }

            if (create_bitmap)
                bmp = new Bitmap(Convert.ToInt32(points), Convert.ToInt32(points));
            Console.WriteLine();

            // Calculation starts here
            var progress = new ProgressBar();
            double points_in_circle = 0f;
            double points_outside_circle = 0f;
            double distance_between = 1f / (points - 1);

            ProgressBar.StartProgress();
            // Y-Achse
            for (long y = 0; y < points; y++)
            {
                // X-Achse
                for (long x = 0; x < points; x++)
                {
                    double newpoint_x = x * distance_between;
                    double newpoint_y = y * distance_between;
                    double range = Math.Pow(newpoint_x, 2) + Math.Pow(newpoint_y, 2);

                    if (range <= 1)
                    {
                        points_in_circle++;
                        if (create_bitmap)
                            bmp.SetPixel(Convert.ToInt32(x), Convert.ToInt32(y), Color.FromArgb(54, 56, 46)); // Grey
                    }
                    else
                    {
                        points_outside_circle++;
                        if (create_bitmap)
                            bmp.SetPixel(Convert.ToInt32(x), Convert.ToInt32(y), Color.FromArgb(91, 195, 235)); // Blue
                    }

                    ProgressBar.ReportProgress((double)(y * points + x) / (points * points));
                }
            }

            ProgressBar.StopPorgress(true);
            double estimate_pi = (points_in_circle * 4 / (points * points));

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PI:    " + estimate_pi.ToString());
            Console.WriteLine();
            Console.WriteLine("IN:    " + points_in_circle.ToString());
            Console.WriteLine("OUT:   " + points_outside_circle.ToString());
            Console.WriteLine("TOTAL: " + (points * points).ToString());
            Console.ForegroundColor = ConsoleColor.White;

            if (create_bitmap)
                bmp.Save("picture.png");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}