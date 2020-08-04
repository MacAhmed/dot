#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using workspacer;
using workspacer.Bar;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;
using workspacer.Bar.Widgets;

using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;


public class BatteryWidget : BarWidgetBase
{
    public Color LowChargeColor { get; set; } = Color.Red;
    public bool HasBatteryWarning { get; set; } = true;
    public double LowChargeThreshold { get; set; } = 0.10;
    public int Interval { get; set; } = 5000;

    private System.Timers.Timer _timer;

    public override IBarWidgetPart[] GetParts()
    {
        PowerStatus pwr = SystemInformation.PowerStatus;
        float CurrentBatteryCharge = pwr.BatteryLifePercent;

        if (HasBatteryWarning)
        {
            if (CurrentBatteryCharge <= LowChargeThreshold)
            {
                return Parts(Part(CurrentBatteryCharge.ToString("#0%"), LowChargeColor));
            }
            else
            {
                return Parts(Part(CurrentBatteryCharge.ToString("#0%")));
            }
        }
        else
        {
            return Parts(CurrentBatteryCharge.ToString("#0%"));
        }
    }

    public override void Initialize()
    {
        _timer = new System.Timers.Timer(Interval);
        _timer.Elapsed += (s, e) => Context.MarkDirty();
        _timer.Enabled = true;
    }
}

Action<IConfigContext> doConfig = (context) =>
{

    context.AddBar(new BarPluginConfig()
    {
        BarTitle = "workspacer.Bar",
        BarHeight = 18,
        FontSize = 10,
        DefaultWidgetForeground = Color.White,
        DefaultWidgetBackground = Color.Black,
        RightWidgets = () => new IBarWidget[] { new TimeWidget(200,"dd-MM-yyyy HH:mm:ss"), new TextWidget("|"), new BatteryWidget(), new TextWidget("|") },
    });

    context.AddFocusIndicator();
    var actionMenu = context.AddActionMenu();

    context.WorkspaceContainer.CreateWorkspaces("1. Browser", "2. Terminal", "3", "4", "5", "6");

    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Cisco AnyConnect Secure Mobility Client"));
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Skype for Business"));

    /* Battery Indicator */
};

return doConfig;
