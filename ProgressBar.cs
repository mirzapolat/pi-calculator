using System;
using System.Timers;

/// <summary>
/// A simple Progress-Bar for Console Applications
/// </summary>
public class ProgressBar
{
    // Change this if you want
    private static int ProgressBarLenght = 60; // Min: 8
    private static int UpdateInterval = 100; // This is the Interval between each Update

    // Don't change these values
    private static System.Timers.Timer tmr = new System.Timers.Timer();
    private static double actual_percentage = 0f;
    private static bool go_on = false;

    private static void WriteProgressBar()
    {
        int amount_blocks = Convert.ToInt32((ProgressBarLenght - 2) * actual_percentage);
        string filling_inside = (string)(new String('=', amount_blocks)) + (string)(new String(' ', (ProgressBarLenght - 2) - amount_blocks));

        double percentage_number = actual_percentage * 100;
        int figures_before_comma;
        int figures_after_comma;

        if (percentage_number.ToString().Contains(','))
        {
            figures_before_comma = Convert.ToInt32(percentage_number.ToString().Split(',')[0]);
            figures_after_comma = Convert.ToInt32(percentage_number.ToString().Split(',')[1]);

            if (figures_after_comma.ToString().Length > 2)
            {
                figures_after_comma = Convert.ToInt32(figures_after_comma.ToString().Substring(0, 2));
            }
        }
        else
        {
            figures_before_comma = Convert.ToInt32(percentage_number);
            figures_after_comma = 0;
        }

        string written_percentage = figures_before_comma.ToString() + "." + figures_after_comma.ToString() + "%";

        int l = written_percentage.Length - 4;
        filling_inside = filling_inside.Remove(ProgressBarLenght / 2 - 2 - l, written_percentage.Length);
        filling_inside = filling_inside.Insert(ProgressBarLenght / 2 - 2 - l, written_percentage);

        Console.Write("[" + filling_inside + "] ");
    }

    /// <summary>
    /// Ilitializes the Progress-Bar
    /// </summary>
    public static void StartProgress()
    {
        Console.Write("[" + (string)(new String(' ', ProgressBarLenght / 2 - 3)) + "0.0%" + (string)(new String(' ', ProgressBarLenght / 2 - 3)) + "]");
        Console.CursorVisible = false;
        go_on = true;

        tmr = new System.Timers.Timer(UpdateInterval);
        tmr.Elapsed += TmrTick;
        tmr.Enabled = true;
    }

    /// <summary>
    /// Stops the Progress-Bar
    /// </summary>
    /// <param name="setfull">Determines whether the Progress-Bar shall set to 100.0% when stopping.</param>
    public static void StopPorgress(bool setfull)
    {
        go_on = false;
        tmr.Enabled = false;
        tmr.Dispose();

        if (setfull)
        {
            actual_percentage = 1f;
            Console.CursorLeft = 0;
            WriteProgressBar();
        }

        Console.CursorVisible = true;
        Console.Write("\n");
    }

    /// <summary>
    /// Updates the Progress
    /// </summary>
    /// <param name="percentage">Determines the percentage</param>
    public static void ReportProgress(double percentage)
    {
        actual_percentage = percentage;
    }

    private static void TmrTick(Object source, ElapsedEventArgs e)
    {
        if (go_on)
        {
            Console.CursorLeft = 0;
            WriteProgressBar();
        }
    }

}
