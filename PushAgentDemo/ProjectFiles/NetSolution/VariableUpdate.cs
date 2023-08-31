#region Using directives
using System;
using FTOptix.Core;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.CODESYS;
using FTOptix.NetLogic;
using FTOptix.HMIProject;
using FTOptix.CommunicationDriver;
using FTOptix.OPCUAServer;
using FTOptix.NativeUI;
#endregion

public class VariableUpdate : BaseNetLogic
{
    public override void Start()
    {
        updateVariableValueTask = new PeriodicTask(UpdateVariableValue, 10, LogicObject);
        updateVariableValueTask.Start();
    }

    public override void Stop()
    {
        updateVariableValueTask.Dispose();
    }

    private void UpdateVariableValue()
    {
        var model = Project.Current.Find("Model");
        model.GetVariable("Variable5").Value = value;

        value = value < Int64.MaxValue ? value + 1 : 0;
    }

    private double GetRandomNumber()
    {
        Random random = new Random();
        return random.NextDouble() * 1500;
    }

    PeriodicTask updateVariableValueTask;
    Int64 value = 0;
}
