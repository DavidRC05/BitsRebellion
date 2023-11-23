using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    string _name;

    

    public string getName()
    {
        return _name;
    }

    public void setName(string newName)
    {
        _name = newName;
    }
}
