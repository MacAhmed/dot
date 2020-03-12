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

Action<IConfigContext> doConfig = (context) =>
{

    context.AddBar(new BarPluginConfig()
    {
        BarTitle = "workspacer.Bar",
        BarHeight = 18,
        FontSize = 10,
        DefaultWidgetForeground = Color.White,
        DefaultWidgetBackground = Color.Black,
        RightWidgets = () => new IBarWidget[] { new TimeWidget(200,"dd-MM-yyyy HH:mm:ss") },
    });

    context.AddFocusIndicator();
    var actionMenu = context.AddActionMenu();

    context.WorkspaceContainer.CreateWorkspaces("1. Browser", "2. Terminal", "3", "4", "5", "6");

    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Cisco AnyConnect Secure Mobility Client"));
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Skype for Business"));

};

return doConfig;
