#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.CommunicationDriver;
using FTOptix.EventLogger;
using FTOptix.Recipe;
#endregion

public class SimulateVariables : BaseNetLogic
{

    private PeriodicTask MyTask;
    private int iCounter;
    private double dCounter;
    private bool bRun;

    public override void Start()
    {
        MyTask = new PeriodicTask(Simulation, 250, LogicObject);
        iCounter = 0;
        dCounter = 0;
        MyTask.Start();
    }

    public void Simulation()
    {
        bRun = LogicObject.GetVariable("bRunSimulation").Value;
        if (bRun == true)
        {
            if (iCounter<=99)
            {
                iCounter =  iCounter + 1;
            }
            else
            {
                iCounter = 0;
            }
            dCounter = dCounter + 0.05;
            LogicObject.GetVariable("iRamp").Value = iCounter;
            LogicObject.GetVariable("iSin").Value = Math.Sin(dCounter) * 100;
            LogicObject.GetVariable("iCos").Value = Math.Cos(dCounter) * 50;
        }

    }

    public override void Stop()
    {
        if (MyTask != null)
        {
            MyTask.Dispose();
            MyTask = null;
        }
    }
}
