using System.Collections.Generic;

namespace driverClasses
{
    public delegate List<List<Button>> resetKeylayout();
    public delegate void saveKeylayout();
    public delegate void reconnect();
    public delegate void changeLayerDel(int index = -1);


    public enum connectioStatus
    {
        disconnected,
        connected,
        connecting
    }
}